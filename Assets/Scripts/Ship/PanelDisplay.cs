using TMPro;
using UnityEngine;

public class PanelDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _warpDisplay;

    private void Update()
    {
        if (_warpDisplay == null)
            return;

        int warpTime = (int)(MovementHandler.WarpTimer - MovementHandler.Stopwatch.Elapsed.TotalSeconds);

        _warpDisplay.text = warpTime <= 0 ? "MODO WARP\nACTIVADO" : $"Warp en {warpTime}...";
    }
}