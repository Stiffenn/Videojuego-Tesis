using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public float Speed = 5f;  // Velocidad de movimiento del jugador

    void Start()
    {
       
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, 0.0f, vertical) * Time.deltaTime * Speed);
    }
}
