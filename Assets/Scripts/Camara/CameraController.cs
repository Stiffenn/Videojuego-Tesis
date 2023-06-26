using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Transform cam;
    public Transform PlayerShip;  // Referencia al transform del jugador

    public Vector3 offset;   // Desplazamiento de la c�mara respecto al jugador

    // Suscribirse al evento CameraMove para controlar la rotaci�n de la c�mara
    void Awake()
    {
        //Control.CameraMove += Camera_Move;
    }

    // Cancelar la suscripci�n al evento CameraMove al desactivar el script
    private void OnDisable()
    {
        Control.CameraMove -= Camera_Move;
    }
    private void Start()
    {
        //cam = Camera.main.transform;
    }
    // M�todo para controlar la rotaci�n de la c�mara
    private void Camera_Move(float cameraX, float cameraY)
    {
        if (PlayerShip != null)
        {
            // Actualiza la posici�n de la c�mara para seguir al jugador
            transform.position = PlayerShip.position + offset;
        }
    }

}


/*if (cameraX > 600)
{
    cam.Rotate(cameraX * Time.deltaTime, 0, 0);
}
if (cameraX < 400)
{
    cam.Rotate(-(cameraX / 2), 0, 0);
}
if (cameraY > 600)
{
    cam.Rotate(0, cameraY * Time.deltaTime, 0);
}
if (cameraY < 400)
{
    cam.Rotate(0, (-(cameraY * Time.deltaTime)) * Time.deltaTime, 0);
} */