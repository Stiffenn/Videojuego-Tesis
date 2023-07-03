using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipScanner : MonoBehaviour
{
    private static LayerMask ScannerLayer;

    /// <summary>
    /// La distancia a la que se buscará objetos para escanear.
    /// </summary>
    [field: SerializeField, Range(0, 2500)]
    public int MaxRange { get; private set; }

    [SerializeField]
    private TMP_Text _text;

    [SerializeField]
    private float _chargeProgress = 1;

    [SerializeField]
    private float _scannerDuration = 5;

    private Planeta _lastPlanet;
    private float _scannerProgress;
    private float _scannerTimer;

    private void Awake()
    {
        Control.ScannerRefresh += OnScanner;

        ScannerLayer = ScannerLayer = LayerMask.GetMask("Escaneable");
    }

    private void OnDestroy()
    {
        Control.ScannerRefresh -= OnScanner;
    }

    private void OnScanner(bool isPressed)
    {
        if (!isPressed)
            return;

        if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, MaxRange, ScannerLayer))
            return;

        if (!hitInfo.collider.TryGetComponent(out Planeta planet))
            return;

        if (_lastPlanet != planet)
            _scannerProgress = 0;

        _lastPlanet = planet;
        _scannerProgress = Mathf.Clamp01(_scannerProgress + Time.deltaTime * _chargeProgress);

        if(_scannerProgress >= 1)
            _scannerTimer = Time.time + _scannerDuration;
    }

    private void Update()
    {
#if UNITY_EDITOR
        Debug.DrawLine(transform.position, transform.position + transform.forward * MaxRange, Color.cyan, 0.1f);
#endif
        bool scanComplete = _scannerProgress >= 1;

        if (scanComplete && _lastPlanet != null)
        {
            _text.text = $"{_lastPlanet.Name}";
        }

        if (scanComplete || _scannerTimer > Time.time)
            return;

        _text.text = $"{Mathf.RoundToInt(_scannerProgress * 100)}%";
    }
}
