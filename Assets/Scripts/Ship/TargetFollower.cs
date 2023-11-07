using UnityEngine;

/// <summary>
/// Makes camera seem following a little behind and above the ship to make player's flight more entertaining and responsive.
/// Based on the camera script from https://www.youtube.com/watch?v=f1jGPdT4Er. Great thanks to Bit Galaxis for showing and overwiew of it.
/// </summary>
public class TargetFollower : MonoBehaviour
{
    [Tooltip("Target to follow."), SerializeField]
    private Transform _target;

    [Tooltip("How fast camera changes its rotation according to player ship.")]
    [SerializeField, Range(0.0f, 250f)]
    private float _rotationSpeed = 3.0f;

    [Tooltip("Bigger value -> camera reaches player slower (Z world axis).")]
    [SerializeField, Range(0.0f, 250f)]
    private float _followSpeed = 0.10f;

    private Vector3 _velocity;
    private Transform _cachedTransform;

    private void Awake()
    {
        if (_target == null)
        {
            Debug.LogWarning($"Field '{nameof(_target)}' is not set in the inspector for {gameObject.name}. Script will be disabled");
            enabled = false;
        }
    }

    private void Start() => _cachedTransform = transform;

    void LateUpdate()
    {
        FollowTarget();
    }

    /// <summary>
    /// This is the best part.
    /// Calculating the angle allows our camera to change its velocity depending on the difference between Quaternions.
    /// Basically this works like a custom Lerp - camera speeds up trying to catch up to the ship and slows down in the end, 
    /// which effectively prevents jittery rotation patterns and makes camera ultra smooth (built-in Lerp and Slerp wouldn't work properly).
    /// </summary>
    private void FollowTarget()
    {
        float delta = Time.deltaTime;
        Quaternion newRotation = Quaternion.Lerp(_cachedTransform.rotation, _target.rotation, delta * _rotationSpeed);
        Vector3 newPos;

        if (_followSpeed <= 0)
            newPos = _target.position;
        else
            newPos = Vector3.Lerp(_cachedTransform.position, _target.position, delta * _followSpeed);

        _cachedTransform.SetPositionAndRotation(newPos, newRotation);
    }
}
