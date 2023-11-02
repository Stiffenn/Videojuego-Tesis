using System.Drawing.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class deals with player input.
/// </summary>
public class ShipInput : MonoBehaviour
{
    public static bool IsInputBlocked { get; set; }

    public const float PotenciadorThreshold = 65;
    public const float DriftingThreshold = 50;
    private const float CenterValue = 500;
    private const float DivideValue = 80;

    // Mouse position relative to screen.
    public float Pitch { get; private set; }
    public float Yaw { get; private set; }

    // Keyboard input info.
    public bool WIsPressed { get; private set; }
    public bool SIsPressed { get; private set; }
    public bool QIsPressed { get; private set; }
    public bool EIsPressed { get; private set; }
    public int NivelPotenciador { get; private set; }

    [SerializeField]
    private float _forcedPotenciadorSpeed = 25.0f;

    [SerializeField]
    private float _backwardsForcedPotenciadorMultiplier = 2.5f;

    private bool _isPressingPotenciador;
    private float _potenciometro;
    private bool _ignorePalanca;

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
    }

    void OnDestroy()
    {
        //Control.CameraMove -= OnCenterMove;
        Control.MovimientoNave -= OnMovimientoNave;
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
        Pitch = Mathf.Clamp(Pitch, -1.0f, 1.0f);

        // Izquierda / Derecha
        Yaw = -Mathf.Clamp(Yaw, -1.0f, 1.0f);
    }

    private void HandleKeyboardInput()
    {
        bool isWPressed = Input.GetKey(KeyCode.W);
        bool isSPressed = Input.GetKey(KeyCode.S);

        _ignorePalanca = isWPressed || isSPressed;

        if (_ignorePalanca)
        {
            float value = isWPressed ? _forcedPotenciadorSpeed : -_forcedPotenciadorSpeed * _backwardsForcedPotenciadorMultiplier;

            _potenciometro += value * Time.deltaTime;
        }

        WIsPressed = _isPressingPotenciador || isWPressed;
        SIsPressed = Input.GetKey(KeyCode.S);
        QIsPressed = Input.GetKey(KeyCode.Q);
        EIsPressed = Input.GetKey(KeyCode.E);

        NivelPotenciador = (int) Mathf.Max(_potenciometro / DivideValue, 0);
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
