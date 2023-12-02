using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private GameObject _blackHole;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Image _ui;

    [SerializeField]
    private float _forwardOffset;

    [SerializeField]
    private float _upOffset;

    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    private float _fovSpeed = 1f;

    [SerializeField]
    private float _fovMultiplier = 1f;

    [SerializeField]
    private float _fovDistance = 80f;

    [SerializeField]
    private float _uiDistance = 35f;

    [SerializeField]
    private float _fadeSpeed = 2.5f;

    private Transform _holeTransform;
    private Transform _cameraTransform;
    private float _defaultFov;
    private float _endTimer;
    private float _fade;

    public static bool EndSequenceActive;

    private void Awake()
    {
        _defaultFov = _camera.fieldOfView;
        _cameraTransform = _camera.transform;
        _holeTransform = _blackHole.transform;
        EndSequenceActive = false;
    }

    private void Update()
    {
        if (EndSequenceActive)
        {
            _holeTransform.LookAt(_cameraTransform.position);   
            _holeTransform.position = Vector3.MoveTowards(_holeTransform.position, _cameraTransform.position, Time.deltaTime * _speed);

            float distance = Vector3.Distance(_holeTransform.position, _cameraTransform.position);
            float newFov = Mathf.Min(distance, _defaultFov) * _fovMultiplier;

            bool ending = distance <= _uiDistance;

            Color oldColor = _ui.color;
            float delta = Time.deltaTime * _fadeSpeed;
            _fade = Mathf.Clamp01(_fade + (ending ? delta : -delta));

            _ui.color = new Color(oldColor.r, oldColor.g, oldColor.b, _fade);
            //_ui.SetActive(ending);
            Debug.Log(distance);

            if (ending)
            {
                _endTimer += Time.deltaTime;

                if(_endTimer >= 5)
                    SceneManager.LoadScene(0);

            }
            else 
                _endTimer = 0;

            if (distance > _fovDistance)
                newFov = _defaultFov;

            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, newFov, Time.deltaTime * _fovSpeed);
            return;
        }

        //if (Scanner.ScannedSoFar < Scanner.MaxScannables)
        if (Scanner.ScannedSoFar < 4)
            return;

        BeginEndSequence();
    }

    private void BeginEndSequence()
    {
        EndSequenceActive = true;

        _blackHole.SetActive(true);

        Vector3 up = _cameraTransform.up * _upOffset;
        Vector3 forward = _cameraTransform.forward * _forwardOffset;

        _holeTransform.position = _cameraTransform.position + up + forward;
    }
}
