using UnityEngine;
public class Control : MonoBehaviour
{
    public delegate void OnMessageReceived(float potenciador, bool warpPressed, bool scannerPressed); 
    public static event OnMessageReceived MessageReceived;
    
    public delegate void OnMovimientoNave(float potenciador);
    public static event OnMovimientoNave MovimientoNave;

    public delegate void OnScannerRefresh(bool isPressed);
    public static event OnScannerRefresh ScannerRefresh;

    public delegate void OnWarp(bool isPressed);
    public static event OnWarp Warp;

    private const string Separator = "|";

    // Start is called before the first frame update
    void OnMessageArrived(string msg) //msg es el valor String que toma del Arduino
    {
        Debug.Log($"Arduino: {msg}");
        string[] array = msg.Split(Separator); // array es igual a msg
        int indice = 0;
        //Debug.Log("Fallo");

        if (!float.TryParse(array[indice++], out float potenciador)) //TryParse convierte el String a float y aplica el valor a Potenciador
        {
            Debug.LogError("No se pudo traducir Potenciador: " + array[indice]);
            return;
        }

        potenciador = 1023 - potenciador; //Inversión de potenciómetro

        bool isWarpPressed = array[indice++] == "0"; //Estado del Swich Joystick
        bool isScannerPressed = array[indice++] == "0"; //Estado del Swich Joystick

        MovimientoNave?.Invoke(potenciador);
        ScannerRefresh?.Invoke(isScannerPressed);
        MessageReceived?.Invoke(potenciador, isWarpPressed, isScannerPressed);
        Warp?.Invoke(isWarpPressed);
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
