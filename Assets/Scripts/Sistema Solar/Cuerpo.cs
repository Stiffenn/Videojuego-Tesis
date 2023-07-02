using UnityEngine;
public abstract class Cuerpo : MonoBehaviour
{
    /// <summary>
    /// Referencia al Movement Blocker.
    /// </summary>
    public MovementBlocker Blocker { get; private set; }

    /// <summary>
    /// Referencia al material de �ste <see cref="Cuerpo"/>.
    /// </summary>
    protected Material Material { get; private set; }

    [SerializeField]
    private float _tama�oMax = 600;

    [SerializeField]
    private float _tama�oMin = 200;

    [SerializeField]
    private float _tama�oOffset = 1f;

    /// <inheritdoc />
    protected virtual void Awake()
    {
        float tama�o = Random.Range(_tama�oMin, _tama�oMax);

        Material = GetComponent<MeshRenderer>().material;
        Blocker = GetComponent<MovementBlocker>();

        Blocker.DetectionRange = tama�o * _tama�oOffset;
        transform.localScale = new Vector3(tama�o, tama�o, tama�o);
    }
}
