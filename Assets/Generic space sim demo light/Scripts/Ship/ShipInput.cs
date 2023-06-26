using UnityEngine;

/// <summary>
/// This class deals with player input.
/// </summary>
public class ShipInput : MonoBehaviour
{
    private const float PotenciadorThreshold = 50;
    private const float DriftingThreshold = 50;
    private const float CenterValue = 500;
    private const float DivideValue = 250;

    // Mouse position relative to screen.
    public float Pitch { get; private set; }
    public float Yaw { get; private set; }

    // Keyboard input info.
    public bool WIsPressed { get; private set; }
    public bool SIsPressed { get; private set; }
    public bool QIsPressed { get; private set; }
    public bool EIsPressed { get; private set; }
    public int NivelPotenciador { get; private set; }

    private bool _isPressingPotenciador;
    private float _potenciometro;

    void Update()
    {
        SetStickCommandsUsingMouse();
        HandleKeyboardInput();
    }

    void Awake()
    {
        Control.CameraMove += OnCenterMove;
        Control.MovimientoNave += OnMovimientoNave;
    }

    void OnDestroy()
    {
        Control.CameraMove -= OnCenterMove;
        Control.MovimientoNave -= OnMovimientoNave;
    }

    /// <summary>
    /// Imitate virtual joystick to rotate ship by mouse position on the screen.
    /// Huge thanks to brihernandez for source code of this method:
    /// https://github.com/brihernandez/ArcadeSpaceFlightExample/blob/master/Assets/ArcadeSpaceFlight/Code/Ship/ShipInput.cs
    /// </summary>
    private void SetStickCommandsUsingMouse()
    {
        // Make sure the values don't exceed limits.
        Pitch = Mathf.Clamp(Pitch, -1.0f, 1.0f);
        Yaw = Mathf.Clamp(Yaw, -1.0f, 1.0f);
    }

    private void HandleKeyboardInput()
    {
        WIsPressed = _isPressingPotenciador || Input.GetKey(KeyCode.W);
        SIsPressed = Input.GetKey(KeyCode.S);
        QIsPressed = Input.GetKey(KeyCode.Q);
        EIsPressed = Input.GetKey(KeyCode.E);

        NivelPotenciador = (int) Mathf.Min(_potenciometro / DivideValue, 1);
        Debug.Log(NivelPotenciador);
    }

    private void OnCenterMove(float cameraX, float cameraY)
    {
        Pitch = TranslateValue(cameraY);
        Yaw = TranslateValue(cameraX);
    }

    private float TranslateValue(float value)
    {
        float absoluteValue = Mathf.Abs(value);

        if (absoluteValue < DriftingThreshold)
        {
            return 0;
        }

        float translatedValue = (absoluteValue / CenterValue);
        bool isPositive = value > 0;

        return isPositive ? translatedValue : -translatedValue;
    }

    private void OnMovimientoNave(float potenciador)
    {
        _isPressingPotenciador = potenciador > PotenciadorThreshold;
        _potenciometro = potenciador;
    }
}
