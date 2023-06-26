using UnityEngine;
public class Control : MonoBehaviour
{
    public delegate void OnMessageReceived(float potenciador, float cameraX, float cameraY, bool isPressed); 
    public static event OnMessageReceived MessageReceived;
    
    public delegate void OnMovimientoNave(float potenciador);
    public static event OnMovimientoNave MovimientoNave;

    public delegate void OnCameraMove(float cameraX, float cameraY);
    public static event OnCameraMove CameraMove;

    private const string Separator = "|";
    private const float CenterValue = 500;

    // Start is called before the first frame update
    void OnMessageArrived(string msg) //msg es el valor String que toma del Arduino
    {
        string[] array = msg.Split(Separator); // array es igual a msg
        int indice = 0;
        //Debug.Log("Fallo");
        if (!float.TryParse(array[indice++], out float potenciador)) //TryParse convierte el String a float y aplica el valor a Potenciador
        {
            Debug.LogError("No se pudo traducir Potenciador: " + array[indice]);
            return;
        }

        if(!float.TryParse(array[indice++], out float cameraX)) //TryParse convierte el String a float y aplica el valor a cameraX
        {
            Debug.LogError("No se pudo traducir CameraX: " + array[indice]);
            return;
        }

        if(!float.TryParse(array[indice++], out float cameraY)) //TryParse convierte el String a float y aplica el valor a cameraY
        {
            Debug.LogError("No se pudo traducir CameraY: " + array[indice]);
            return;
        }

        bool isPressed = array[indice++] != "0"; //Estado del Swich Joystick

        // Les restamos el valor central para que el valor vaya de -500 a 500.
        cameraX -= CenterValue;
        cameraY -= CenterValue;

        MessageReceived?.Invoke(potenciador, cameraX, cameraY, isPressed);
        CameraMove?.Invoke(cameraX, cameraY);
        MovimientoNave?.Invoke(potenciador);
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
