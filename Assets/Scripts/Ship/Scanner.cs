using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public static readonly List<Transform> Scannables = new();

    [SerializeField]
    private TMP_Text _scannerText;

    [SerializeField]
    private float _maxDistance;

    [SerializeField]
    private float _dotThresholdMultiplier = 0.93f;

    [SerializeField]
    private float _scanSpeed = 15f;

    [SerializeField]
    private Transform _camera;

    private readonly Dictionary<Transform, float> _progress = new();

    private void Update()
    {
        const float maxScanValue = 100f;

        foreach (Transform item in Scannables)
        {
            float distance = Vector3.Distance(_camera.position, item.position);

            if (distance > _maxDistance)
                continue;

            Vector3 cameraForward = _camera.TransformDirection(Vector3.forward);
            Vector3 direction = item.position - _camera.position;

            float dot = Vector3.Dot(cameraForward, direction);

            if (dot < distance * _dotThresholdMultiplier)
                continue;

            if (!item.TryGetComponent(out Planet planet))
            {
                _scannerText.text = "---";
                //_scannerText.text = $"dot: ({Mathf.RoundToInt(dot)} < {Mathf.RoundToInt(distance * _dotThresholdMultiplier)}) distance: {Mathf.RoundToInt(distance)}";
                continue;
            }

            if(!_progress.TryGetValue(item, out float value))
            {
                _progress.Add(item, value = 0);
            }

            int percentage = (int)Math.Round(value / maxScanValue * 100);
            
            if (InputReceiver.ScannerIsPressed && value < maxScanValue)
                _progress[item] = value + Time.deltaTime * _scanSpeed;

            if (value >= maxScanValue)
            {
                _scannerText.text = $"{planet.shapeSettings.Name}";
                return;
            }

            _scannerText.text = $"??? ({percentage}%)";
            return;
        }
        
        _scannerText.text = string.Empty;
    }
}