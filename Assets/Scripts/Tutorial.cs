using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public enum Tutoriales
    {
        Up, Down, Left, Right, Scanner, Potenciador,
    }

    public Image TutorialUp;
    public Image TutorialDown;
    public Image TutorialLeft;
    public Image TutorialRight;
    public Image Scanner;
    public Slider Potenciometro;
    public Slider Carga;
    public float PotenciadorVelocidad = 1f;
    public float CargaVelocidad = 1f;

    public readonly Dictionary<Tutoriales, bool> TutorialesCompletados = new Dictionary<Tutoriales, bool>();

    private bool _potenciadorApretado;

    void Next()
    {
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Awake()
    {
        Control.CameraMove += OnCameraMove;
        Control.MovimientoNave += MovimientoNave;
        Control.ScannerRefresh += OnScanner;

        foreach (Tutoriales item in Enum.GetValues(typeof(Tutoriales)))
        {
            TutorialesCompletados[item] = false;
        }
    }

    void UpdateCarga()
    {
        if (TutorialesCompletados.Any(t => !t.Value))
            return;

        Carga.value -= Time.deltaTime * CargaVelocidad;

        if (Carga.value <= 0)
        {
            Carga.value = 100;
            Next();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCarga();

        float speed = Time.deltaTime * PotenciadorVelocidad;

        if (_potenciadorApretado)
        {
            Potenciometro.value += speed;
            return;
        }

        Potenciometro.value -= speed;
    }

    private void OnDestroy()
    {
        Control.CameraMove -= OnCameraMove;
        Control.MovimientoNave -= MovimientoNave;
        Control.ScannerRefresh -= OnScanner;
    }

    private void OnScanner(bool isPressed)
    {
        Color scanner = isPressed ? Color.green : Color.red;

        if (isPressed)
            TutorialesCompletados[Tutoriales.Scanner] = isPressed;

        Scanner.color = scanner;
    }

    private void MovimientoNave(float potenciador)
    {
        bool isPressed = potenciador >= ShipInput.PotenciadorThreshold;

        _potenciadorApretado = isPressed;

        if(isPressed)
            TutorialesCompletados[Tutoriales.Potenciador] = isPressed;
    }

    private void OnCameraMove(float cameraX, float cameraY)
    {
        Color up = Color.red;
        Color down = Color.red;
        Color left = Color.red;
        Color right = Color.red;

        if (!IsDrifting(cameraX))
        {
            bool movingLeft = cameraX < 0;

            if (movingLeft)
                TutorialesCompletados[Tutoriales.Left] = true;
            else
                TutorialesCompletados[Tutoriales.Right] = true;

            left = movingLeft ? Color.green : Color.red;
            right = movingLeft ? Color.red : Color.green;
        }

        if (!IsDrifting(cameraY))
        {
            bool movingUp = cameraY < 0;

            if (movingUp)
                TutorialesCompletados[Tutoriales.Up] = true;
            else
                TutorialesCompletados[Tutoriales.Down] = true;

            up = movingUp ? Color.green : Color.red;
            down = movingUp ? Color.red : Color.green;
        }

        TutorialUp.color = up;
        TutorialDown.color = down;
        TutorialLeft.color = left;
        TutorialRight.color = right;
    }

    private bool IsDrifting(float value)
    {
        float absoluteValue = Mathf.Abs(value);

        return absoluteValue < ShipInput.DriftingThreshold;
    }
}
