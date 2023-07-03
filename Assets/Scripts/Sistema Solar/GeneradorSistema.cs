using UnityEngine;
public class GeneradorSistema : MonoBehaviour
{
    public static Vector3 Center { get; private set; } = Vector3.zero;

    [SerializeField]
    private bool _randomizePlanetPosition;

    [SerializeField]
    private int _planetasMin = 5;

    [SerializeField]
    private int _planetasMax = 7;

    [SerializeField]
    private float _minDistance = 300;

    [SerializeField]
    private float _maxDistance = 500;

    [SerializeField]
    private float _altura = 100;

    [SerializeField]
    private Planeta _planetaMagma;

    [SerializeField]
    private Planeta _planetaOceano;

    [SerializeField]
    private float _distanciaMagma;

    [SerializeField]
    private GameObject _estrellaPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Terminar después
        SpawnearPlanetas(Random.Range(_planetasMin, _planetasMax));
    }

    public void SpawnearPlanetas(int planetas)
    {
        Transform star = Instantiate(_estrellaPrefab).transform;
        float radius = star.localScale.x * 1.75f;
        
        star.SetParent(transform);
        star.localPosition = Vector3.zero;

        Center = star.position;

        for (int i = 0; i < planetas; i++)
        {
            radius += Random.Range(_minDistance, _maxDistance);

            Transform planeta = Instantiate(radius <= _distanciaMagma ? _planetaMagma : _planetaOceano).transform;

            radius += planeta.localScale.x;
            Vector3 planetPos = new(RandomNegative(radius), 0, RandomNegative(radius));

            planeta.SetParent(transform);
            planeta.localPosition = planetPos;

            planeta.RotateAround(Center, Vector3.up, Random.Range(0, 360));
        }
    }

    private float RandomNegative(float value)
    {
        if (!_randomizePlanetPosition)
            return value;

        return Random.value > 0.5f ? value : -value;
    }
}
