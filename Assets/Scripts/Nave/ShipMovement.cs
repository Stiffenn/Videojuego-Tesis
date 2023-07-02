using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Transform-based movement by Z world axis.
/// </summary>
public class ShipMovement : MonoBehaviour
{
    private Dictionary<int, float> _speed = new Dictionary<int, float>()
    {
        [1] = 1f,
        [2] = 1.25f,
        [3] = 1.5f,
        [4] = 1.75f,
        [5] = 2f
    };

    [Range(1.0f, 400.0f)]
    [Tooltip("Maximum speed of player ship.")]
    [SerializeField] private float maxSpeed = 200.0f;

    [Range(1.0f, 200.0f)]
    [Tooltip("How fast the ship's position is changed through time.\nThe less this value is, the smoother (or heavier) movement feels.")]
    [SerializeField] private float deltaMovementSpeed = 25.0f;

    [Range(1.0f, 2.0f)]
    [Tooltip("How fast the ship should lose its speed when using brake (S button).\nValue of 1 means it's the same as acceleration.")]
    [SerializeField] private float brakeForce = 1.5f;

    [Range(0.01f, 3.0f)]
    [Tooltip("How fast the ship will be losing its speed over time when not accelerating.\nBigger value -> ship will lose speed faster.")]
    [SerializeField] private float inertia = 2f;

    [SerializeField]
    private AnimationCurve _acceleratorCalculator;

    [SerializeField] private Ship ship;

    private float currentSpeed = 0f;

    private Transform thisTransform;

    private Vector3 targetPos;
    private Vector3 smoothPos;

    // Accessor for various game mechanics, such as collisions and speed displayer.
    public float CurrentSpeed
    {
        get => currentSpeed;
        internal set => currentSpeed = value;
    }

    private void Awake()
    {
        if (ship == null)
            ship = GetComponent<Ship>();
    }

    private void Start() => thisTransform = gameObject.transform;
    
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        targetPos = (thisTransform.position + (thisTransform.forward * (currentSpeed)) * Time.deltaTime);
        smoothPos = Vector3.Lerp(a: thisTransform.position, b: targetPos, t: 0.99f);

        // Moving ship by applying changes directly to its transform every frame. 
        thisTransform.position = smoothPos;

        // Smoothly changing the apply value over time on key presses.
        if (ship.ShipInput.WIsPressed)
            currentSpeed += (deltaMovementSpeed * Time.deltaTime) /** (Speed(ship.ShipInput.NivelPotenciador))*/;//_acceleratorCalculator.Evaluate(ship.ShipInput.NivelPotenciador);
        else if (ship.ShipInput.SIsPressed)
            currentSpeed -= (deltaMovementSpeed * brakeForce) * Time.deltaTime;

        // Mimic inertia force by passive speed decrease.
        else if (currentSpeed > 0.0001f)
            currentSpeed -= (deltaMovementSpeed * inertia) * Time.deltaTime;
        else if (currentSpeed < 0f)
            currentSpeed += (deltaMovementSpeed * inertia) * Time.deltaTime;

        // Setting speed limits.
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 6.0f, maxSpeed);
    }

    private float Speed(int val)
    {
        if(!_speed.TryGetValue(val, out float speed))
            return 1;
        
        return speed;
    }
}