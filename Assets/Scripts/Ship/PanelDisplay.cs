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

        if (!InputReceiver.WarpIsPressed)
        {
            _warpDisplay.text = string.Empty;
            return;
        }

        _warpDisplay.text = warpTime <= 0 ? "MOTOR WARP\nACTIVADO" : $"Warp en {warpTime}...";
    }
}