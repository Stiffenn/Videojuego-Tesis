using UnityEngine;

/// <summary>
/// This class is responsible for rotating ship by mouse and E + Q buttons.
/// </summary>
public class NaveMovimiento : MonoBehaviour
{
    private const KeyCode Forward = KeyCode.W;
    private const KeyCode Left = KeyCode.A;
    private const KeyCode Backwards = KeyCode.S;
    private const KeyCode Right = KeyCode.D;

    public Vector3 Direction { get; private set; } = Vector3.zero;

    [SerializeField]
    private Vector4 _speed = Vector4.one;

    private Transform _cachedTransform;
    private float _accelerationRight;
    private float _accelerationForward;

    private void Update()
    {
        bool isMovingForward = Input.GetKey(Forward);
        bool isMovingLeft = Input.GetKey(Left);
        bool isMovingBackwards = Input.GetKey(Backwards);
        bool isMovingRight = Input.GetKey(Right);

        if(isMovingBackwards)
            _accelerationForward += _speed.z;
        else if(isMovingForward)
            _accelerationForward += _speed.x;
        
        if(isMovingLeft)
            _accelerationRight += _speed.y;
        
        if(isMovingRight)
            _accelerationRight += _speed.w;

        
        
        /*
        Vector3 newDirection = Vector3.zero;

        if(isMovingForward)
            newDirection += _cachedTransform.forward;
        
        if(isMovingDown)
            newDirection -= _cachedTransform.forward;
        
        if(isMovingRight)
            newDirection += _cachedTransform.right;
        
        if(isMovingLeft)
            newDirection -= _cachedTransform.right;

        // We update the direction in case other scripts access it.
        Direction = newDirection;

        Vector3 curPos = _cachedTransform.position;
        Vector3 newPos = curPos + newDirection;

        _cachedTransform.position = Vector3.Lerp(curPos, newPos, _speedDelay);
        */
    }

    private void Awake()
    {
        _cachedTransform = transform;
    }
}