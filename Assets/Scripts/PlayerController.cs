using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 targetPos;
    private Vector3 smoothPos;
    public float movimientoAngulo = 100f;
    [SerializeField] private float deltaMovementSpeed = 25.0f;
    [SerializeField] private float inertia = 0.1f;


    // Suscribirse al evento MovimientoNave para controlar el movimiento del jugador
    void Awake()
    {
        Control.MovimientoNave += Movimiento_Nave;
        // Control.CameraMove += Camera_Move;
    }

    private void Movimiento_Nave(float potenciador)
    {
        /*
        float Pitch = (cameraY - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        float Yaw = (cameraX - (Screen.width * 0.5f)) / (Screen.width * 0.5f);
        float currentSpeed = 10f;

        Pitch = Mathf.Clamp(Yaw, -1.0f, 1.0f);
        Yaw =  -Mathf.Clamp(Pitch, -1.0f, 1.0f);

        targetPos = (transform.position + (transform.forward * (currentSpeed)) * Time.deltaTime);
        smoothPos = Vector3.Lerp(a: transform.position, b: targetPos, t: 0.99f);

        transform.position = smoothPos;

        if (potenciador >= 50)
        {
            // Mover al jugador en el eje Z en función del valor del potenciador
            currentSpeed += potenciador *Time.deltaTime;
            //transform.position += new Vector3(0f, 0f, potenciador / 102);
        } else if (currentSpeed > 0.0001f)
            currentSpeed -= (deltaMovementSpeed * inertia) * Time.deltaTime;
        else if (currentSpeed < 0f)
            currentSpeed += (deltaMovementSpeed * inertia) * Time.deltaTime;



        if (cameraX > 600)
        {
            //transform.position += new Vector3(cameraX / 102, 0f, 0f);
            transform.Rotate(xAngle: 0f,
                     yAngle:  -(Yaw * movimientoAngulo) * Time.deltaTime,
                     zAngle: 0f);
        }
        if (cameraX < 400)
        {
            //transform.position -= new Vector3(10f, 0f, 0f);
            transform.Rotate(xAngle: 0f,
            yAngle:  -(Yaw * movimientoAngulo) * Time.deltaTime,
            zAngle: 0f);
        }
        if (cameraY > 600)
        {
            //transform.position += new Vector3(0f, cameraY / 102, 0f);
            transform.Rotate(xAngle: -(Pitch * movimientoAngulo) * Time.deltaTime,
                     yAngle: 0f,
                     zAngle: 0f);
        }
        if (cameraY < 400)
        {
            //transform.position -= new Vector3(0f, 10f, 0f);
            transform.Rotate(xAngle: (Pitch * movimientoAngulo) * Time.deltaTime,
                     yAngle: 0f,
                      zAngle: 0f);
        }*/

    }

}

/*        if (cameraX > 600)
        {
            //transform.position += new Vector3(cameraX / 102, 0f, 0f);
            transform.Rotate(xAngle: (Pitch * movimientoAngulo) * Time.deltaTime,
                     yAngle: (Yaw * movimientoAngulo) * Time.deltaTime,
                     zAngle: 0f);
        }
        if (cameraX < 400)
        {
            //transform.position -= new Vector3(10f, 0f, 0f);
            transform.Rotate(xAngle: -(Pitch * movimientoAngulo) * Time.deltaTime,
            yAngle: -(Yaw * movimientoAngulo) * Time.deltaTime,
            zAngle: 0f);
        }
        if (cameraY > 600)
        {
            //transform.position += new Vector3(0f, cameraY / 102, 0f);
            transform.Rotate(xAngle: (Pitch * movimientoAngulo) * Time.deltaTime,
                     yAngle: (Yaw * movimientoAngulo) * Time.deltaTime,
                     zAngle: 0f);
        }
        if (cameraY < 400)
        {
            //transform.position -= new Vector3(0f, 10f, 0f);
            transform.Rotate(xAngle: -(Pitch * movimientoAngulo) * Time.deltaTime,
                     yAngle: -(Yaw * movimientoAngulo) * Time.deltaTime,
                      zAngle: 0f);
        } */