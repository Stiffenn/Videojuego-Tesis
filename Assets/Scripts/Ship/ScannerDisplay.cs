using TMPro;
using UnityEngine;

public class ScannerDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scannerDisplay;

    private void Update()
    {
        if (_scannerDisplay == null)
            return;

        int warpTime = (int)(MovementHandler.WarpTimer - MovementHandler.Stopwatch.Elapsed.TotalSeconds);

        _scannerDisplay.text = warpTime <= 0 ? "MODO WARP\nACTIVADO" : $"Warp en {warpTime}...";
    }
}