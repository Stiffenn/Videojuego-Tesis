using TMPro;
using UnityEngine;

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

        string objName = hitInfo.collider.gameObject.name;
        _text.text = $"{objName}";
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * MaxRange, Color.cyan, 0.1f);
    }
}
