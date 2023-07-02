using UnityEngine;
public class GeneradorSistema : MonoBehaviour
{
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
    private Planeta[] _planetaPrefabs;

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
        float angleIncrement = 360f / planetas;
        
        star.SetParent(transform);
        star.position = Vector3.zero;

        for (int i = 0; i < planetas; i++)
        {
            float angle = i * angleIncrement;
            Transform planeta = Instantiate(_planetaPrefabs[Random.Range(0, _planetaPrefabs.Length)]).transform;

            radius += Random.Range(_minDistance, _maxDistance) + planeta.localScale.x;
            Vector3 planetPos = CalculatePosition(angle, radius);//(new(RandomNegative(radius), 0, RandomNegative(radius)));

            //planeta.SetParent(transform);
            planeta.position = planetPos;
        }
    }

    private Vector3 CalculatePosition(float angle, float distance)
    {
        float angleRadians = angle * Mathf.Deg2Rad;
        float x = distance * Mathf.Cos(angleRadians);
        float z = distance * Mathf.Sin(angleRadians);
        return transform.position + new Vector3(x, 0, z);
    }

    private float RandomNegative(float value)
    {
        if (!_randomizePlanetPosition)
            return value;

        return Random.value > 0.5f ? value : -value;
    }
}
