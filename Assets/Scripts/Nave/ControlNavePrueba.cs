using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlNavePrueba : MonoBehaviour
{
    public float VelocidadAdelante = 1f;
    private float _velocidadActual;

    [SerializeField]
    private Rigidbody _rb;

    public float velocidadX = 25f, velocidadY = 7.5f;
    private float activoVelocidadX, activoVelocidadY;

    public float lookRateSpeed = 90f;
    private Vector2 screenCenter, mouseDistance;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();

        //mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f) ;

        activoVelocidadX = Input.GetAxisRaw("Vertical") * velocidadX;
        activoVelocidadY = Input.GetAxisRaw("Horizontal") * velocidadY;

        //transform.position += transform.forward * activoVelocidadX * Time.deltaTime;
        transform.position += transform.right * activoVelocidadY * Time.deltaTime;
    }

    void FixedUpdate()
    {
        ImpulsarNave();

    }

    void ImpulsarNave()
    {
        _rb.AddForce(transform.forward * Input.GetAxisRaw("Vertical") * VelocidadAdelante);
/*
        float input = Mathf.Min(Input.GetAxisRaw("Vertical") * VelocidadAdelante * Time.deltaTime, VelocidadAdelanteMax);

        transform.position += transform.forward * input;
        */
    }

    void MouseMovement()
    {
        Vector2 lookInput = Input.mousePosition;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.x) / screenCenter.y;

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, 0f, Space.Self);
    }
}
