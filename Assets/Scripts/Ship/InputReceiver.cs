using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class deals with player input.
/// </summary>
public class InputReceiver : MonoBehaviour
{

    public static bool IsMovementBlocked { get; set; }
    public static bool IsRotationBlocked { get; set; }

    public const float PotenciadorThreshold = 65;
    public const float DriftingThreshold = 50;
    private const float CenterValue = 500;
    private const float DivideValue = 80;

    // Mouse position relative to screen.
    public static float Pitch { get; private set; }
    public static float Yaw { get; private set; }

    // Keyboard input info.
    public static bool WarpIsPressed { get; private set; }
    public static bool ScannerIsPressed { get; private set; }
    public static bool WIsPressed { get; private set; }
    public static bool SIsPressed { get; private set; }
    public static bool QIsPressed { get; private set; }
    public static bool EIsPressed { get; private set; }
    public static int NivelPotenciador { get; private set; }
    public static float Potenciador { get; private set; }
    public static bool WIsForced { get; set; }

    [SerializeField]
    private float _forcedPotenciadorSpeed = 25.0f;

    [SerializeField]
    private float _backwardsForcedPotenciadorMultiplier = 2.5f;

    private bool _isPressingPotenciador;
    private float _potenciometro;
    private bool _ignorePalanca;
    private Stopwatch _warpStopwatch = new Stopwatch();
    private Stopwatch _scannerStopwatch = new Stopwatch();

    void Update()
    {
        HandleKeyboardInput();
        ClampRotation();
        RestartKey();
    }

    void Awake()
    {
        //Control.CameraMove += OnCenterMove;
        Control.MovimientoNave += OnMovimientoNave;
        Control.ScannerRefresh += OnScanner;
        Control.Warp += OnWarp;
    }

    private void OnWarp(bool isPressed)
    {
        if (isPressed)
        {
            _warpStopwatch.Start();
            return;
        }

        _warpStopwatch.Reset();
    }

    private void OnScanner(bool isPressed)
    {
        if (isPressed)
        {
            _scannerStopwatch.Start();
            return;
        }

        _scannerStopwatch.Reset();
    }

    void OnDestroy()
    {
        //Control.CameraMove -= OnCenterMove;
        Control.ScannerRefresh -= OnScanner;
        Control.MovimientoNave -= OnMovimientoNave;
        Control.Warp -= OnWarp;
    }

    /// <summary>
    /// Imitate virtual joystick to rotate ship by mouse position on the screen.
    /// Huge thanks to brihernandez for source code of this method:
    /// https://github.com/brihernandez/ArcadeSpaceFlightExample/blob/master/Assets/ArcadeSpaceFlight/Code/Ship/ShipInput.cs
    /// </summary>
    private void ClampRotation()
    {
        const float screenLimit = 0.5f;
        Vector3 mousePos = Input.mousePosition;

        Pitch = (mousePos.y - (Screen.height * screenLimit)) / (Screen.height * screenLimit);
        Yaw = (mousePos.x - (Screen.width * screenLimit)) / (Screen.width * screenLimit);

        // Make sure the values don't exceed limits.

        // Arriba / Abajo
        Pitch = IsMovementBlocked ? 0 : Mathf.Clamp(Pitch, -1.0f, 1.0f);

        // Izquierda / Derecha
        Yaw = IsMovementBlocked ? 0 : -Mathf.Clamp(Yaw, -1.0f, 1.0f);
    }

    private void HandleKeyboardInput()
    {
        bool isWPressed = WIsForced || (Input.GetKey(KeyCode.W) && !IsMovementBlocked);
        bool isSPressed = Input.GetKey(KeyCode.S) && !IsMovementBlocked;

        _ignorePalanca = (isWPressed || isSPressed) && !IsMovementBlocked;

        if (_ignorePalanca)
        {
            float value = isWPressed ? _forcedPotenciadorSpeed : -_forcedPotenciadorSpeed * _backwardsForcedPotenciadorMultiplier;

            _potenciometro += value * Time.deltaTime;
        }

        ScannerIsPressed = Input.GetKey(KeyCode.LeftControl) || _scannerStopwatch.Elapsed.Seconds > 0.1;
        WarpIsPressed = (Input.GetKey(KeyCode.Space) || _warpStopwatch.Elapsed.Seconds > 0.1f) && !WIsForced;
        WIsPressed = (_isPressingPotenciador || isWPressed);
        SIsPressed = Input.GetKey(KeyCode.S);
        QIsPressed = Input.GetKey(KeyCode.Q) && !IsRotationBlocked;
        EIsPressed = Input.GetKey(KeyCode.E) && !IsRotationBlocked;

        NivelPotenciador = (int)Mathf.Max(_potenciometro / DivideValue, 0);
    }

    private void RestartKey()
    {
        if (!Input.GetKeyDown(KeyCode.R))
            return;

        SceneManager.LoadScene(0);
    }

    private void OnCenterMove(float cameraX, float cameraY)
    {
        Pitch = TranslateValue(cameraY, true);
        Yaw = TranslateValue(cameraX);
    }

    private float TranslateValue(float value, bool inverted = false)
    {
        float absoluteValue = Mathf.Abs(value);

        if (absoluteValue < DriftingThreshold)
        {
            return 0;
        }

        float translatedValue = (absoluteValue / CenterValue);
        bool isPositive = value > 0;

        if (inverted)
            isPositive = !isPositive;

        return isPositive ? translatedValue : -translatedValue;
    }

    private void OnMovimientoNave(float potenciador)
    {
        if (_ignorePalanca)
            return;

        _isPressingPotenciador = potenciador > PotenciadorThreshold;
        _potenciometro = potenciador;
    }
}
