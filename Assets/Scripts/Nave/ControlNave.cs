using UnityEngine;
public class ControlNave : MonoBehaviour
{
    float movimiento;
    // Start is called before the first frame update
    void OnMessageArrived(string msg)
    {
        Debug.Log("Message arrived: " + msg);
        var array = msg.Split("|");
        movimiento = float.Parse(array[0]);

        if (movimiento > 550)
        {
            transform.position += new Vector3(1f, 0f, 0);
        }
        if (movimiento < 550)
        {
            transform.position -= new Vector3(1f, 0f, 0);
        }
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
