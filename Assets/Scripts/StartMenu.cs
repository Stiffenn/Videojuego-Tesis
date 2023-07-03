using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private Transform _tutorial;

    private void Awake()
    {
        Control.CameraMove += OnCameraMove;
        Control.MovimientoNave += MovimientoNave;
        Control.ScannerRefresh += OnScanner;
    }
    private void OnDestroy()
    {
        Control.CameraMove -= OnCameraMove;
        Control.MovimientoNave -= MovimientoNave;
        Control.ScannerRefresh -= OnScanner;
    }

    public void Next()
    {
        _tutorial.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnScanner(bool isPressed)
    {
        if (!isPressed)
            return;

        Next();
    }

    private void MovimientoNave(float potenciador)
    {
        if (potenciador < ShipInput.PotenciadorThreshold)
            return;

        Next();
    }

    private void OnCameraMove(float cameraX, float cameraY)
    {
        if (cameraX < ShipInput.DriftingThreshold || cameraY < ShipInput.DriftingThreshold)
            return;
        
        Next();
    }
}
