using UnityEngine;
public abstract class Cuerpo : MonoBehaviour
{
    /// <summary>
    /// Referencia al Movement Blocker.
    /// </summary>
    public MovementBlocker Blocker { get; private set; }

    /// <summary>
    /// Referencia al material de éste <see cref="Cuerpo"/>.
    /// </summary>
    protected Material Material { get; private set; }

    [SerializeField]
    private float _tamañoMax = 600;

    [SerializeField]
    private float _tamañoMin = 200;

    [SerializeField]
    private float _tamañoOffset = 1f;

    /// <inheritdoc />
    protected virtual void Awake()
    {
        float tamaño = Random.Range(_tamañoMin, _tamañoMax);

        Material = GetComponent<MeshRenderer>().material;
        Blocker = GetComponent<MovementBlocker>();

        Blocker.DetectionRange = tamaño * _tamañoOffset;
        transform.localScale = new Vector3(tamaño, tamaño, tamaño);
    }
}
