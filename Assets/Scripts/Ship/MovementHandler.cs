using System.Diagnostics;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public static readonly Stopwatch Stopwatch = new();

    public static bool IsWarping
    {
        get => _isWarping;
        set
        {
            _isWarping = value;
            InputReceiver.IsMovementBlocked = value;
            InputReceiver.IsRotationBlocked = value;
        }
    }

    public const float WarpTimer = 4f;

    [SerializeField]
    private float _forwardSpeed;

    [SerializeField]
    private float _forwardAcceleration;

    [SerializeField]
    private float _warpSpeed;

    [SerializeField]
    private float _warpAcceleration;

    [SerializeField]
    private Transform[] _movableObjects;

    [SerializeField]
    private Transform _shipOrigin;

    [SerializeField]
    private Transform _forwardOrigin;

    [SerializeField]
    private SpeedData[] _leverSpeed;

    private static bool _isWarping;

    private void ActivateWarp()
    {
        if (!InputReceiver.WarpIsPressed)
        {
            Stopwatch.Reset();

            IsWarping = false;
            return;
        }
        else if (!Stopwatch.IsRunning)
            Stopwatch.Start();

        if (Stopwatch.Elapsed.TotalSeconds <= WarpTimer)
            return;

        IsWarping = true;
    }

    private void Update()
    {
        ActivateWarp();

        float speed = GetSpeed();
        float acceleration = IsWarping ? _warpAcceleration : _forwardAcceleration;
        Vector3 moveDir = Vector3.zero;

        if (InputReceiver.WIsPressed || InputReceiver.WIsForced || IsWarping)
        {
            moveDir = speed * Time.deltaTime * _forwardOrigin.forward;
        } else if (InputReceiver.SIsPressed)
        {
            moveDir = _forwardSpeed * Time.deltaTime * -_forwardOrigin.forward;
        }

        if (IsWarping)
        {
            for (int i = 0; i < _movableObjects.Length; i++)
            {
                MoveInDirection(_movableObjects[i], -moveDir, acceleration);
            }
        }
        else
            MoveInDirection(_shipOrigin, moveDir, acceleration);
    }

    private void MoveInDirection(Transform transform, Vector3 direction, float acceleration)
    {
        Vector3 curPos = transform.position;
        Vector3 newPos = curPos + direction;

        transform.position = Vector3.Lerp(curPos, newPos, Time.deltaTime * acceleration);
    }

    /// <summary>
    /// Ésto tiene 3 prioridades/resultados:
    /// 1. Velocidad de Warping, si estás warpeando deberías ir a la velocidad de warpeo sin importar qué.
    /// 2. Va a devolver el nivel de velocidad más alto alcanzado por el potenciador.
    /// 3. Como último y default, velocidad normal de movimiento.
    /// </summary>
    /// <returns></returns>
    private float GetSpeed()
    {
        if (IsWarping)
            return _warpSpeed;

        for (int i = _leverSpeed.Length - 1; i >= 0; i--)
        {
            SpeedData data = _leverSpeed[i];

            if (InputReceiver.Potenciador >= data.LeverCap)
                return data.Speed;
        }

        return _forwardSpeed;
    }

    [System.Serializable]
    private struct SpeedData
    {
        public float Speed;
        public float LeverCap;
    }
}