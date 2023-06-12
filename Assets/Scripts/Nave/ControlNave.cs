using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlNave : MonoBehaviour
{
    public float velocidadX = 25f, velocidadY = 7.5f;
    private float activoVelocidadX, activoVelocidadY;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.x) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f) ;

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, 0f, Space.Self);

        activoVelocidadX = Input.GetAxisRaw("Vertical") * velocidadX;
        activoVelocidadY = Input.GetAxisRaw("Horizontal") * velocidadY;

        transform.position += transform.forward * activoVelocidadX * Time.deltaTime;
        transform.position += transform.right * activoVelocidadY * Time.deltaTime;
    }
}
