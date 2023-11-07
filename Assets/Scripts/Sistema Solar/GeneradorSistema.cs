using System.Collections.Generic;
using System.ComponentModel;
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
    private Planet _planetTemplate;

    [SerializeField]
    private GameObject _estrellaPrefab;

    [Space(5), Header("-------- Planet Pool")]

    [SerializeField]
    private List<PlanetData> _rocosos = new();

    [SerializeField]
    private List<PlanetData> _gaseosos = new();

    [SerializeField]
    private List<PlanetData> _calientes = new();

    [SerializeField]
    private List<PlanetData> _aquaticos = new();

    // Start is called before the first frame update
    void Start()
    {
        // Terminar después
        SpawnearPlanetas(Random.Range(_planetasMin, _planetasMax));
    }

    public void SpawnearPlanetas(int planetas)
    {
        Transform star = Instantiate(_estrellaPrefab).transform;
        float radius = star.localScale.x * 2.5f;
        
        star.SetParent(transform);
        star.localPosition = Vector3.zero;

        Center = star.position;
        Scanner.Scannables.Add(star);

        for (int i = 0; i < planetas; i++)
        {
            // Select which planet to spawn.
            List<PlanetData> pool = GetPlanetPool(GetCategory(i));
            PlanetData chosenData = pool[Random.Range(0, pool.Count)];

            // Spawn the planet.
            Planet planet = Instantiate(_planetTemplate);
            Transform planetTransform = planet.transform;

            // Generate the planet with the chosen settings.
            planet.colourSettings = chosenData.ColorSettings;
            planet.shapeSettings = chosenData.ShapeSettings;
            planet.GeneratePlanet();

            // Offset the planet's distance by it's size.
            radius += planet.shapeSettings.planetRadius * 5f;

            // Place the planet in the world.
            Vector3 planetPos = new(RandomNegative(radius), 0, RandomNegative(radius));

            planetTransform.SetParent(transform);
            planetTransform.localPosition = planetPos;
            planetTransform.RotateAround(Center, Vector3.up, Random.Range(0, 360));

            planet.HasSpawned = true;
            Scanner.Scannables.Add(planetTransform);
        }
    }

    public List<PlanetData> GetPlanetPool(PlanetCategory category)
    {
        return category switch
        {
            PlanetCategory.Rocoso => _rocosos,
            PlanetCategory.Aquático => _aquaticos,
            PlanetCategory.Gaseoso => _gaseosos,
            PlanetCategory.Caliente => _calientes,
            _ => throw new InvalidEnumArgumentException($"Could not find planet pool: '{category}'"),
        };
    }

    private PlanetCategory GetCategory(int index)
    {
        switch (index)
        {
            case 0:
                return PlanetCategory.Caliente;
            case 1:
            case 2:
                return PlanetCategory.Rocoso;
            case 3:
            case 4:
                return PlanetCategory.Aquático;
            default:
                return PlanetCategory.Gaseoso;
        }
    }

    private float RandomNegative(float value)
    {
        if (!_randomizePlanetPosition)
            return value;

        return Random.value > 0.5f ? value : -value;
    }

    [System.Serializable]
    public struct PlanetData
    {
        public ColorSettings ColorSettings;
        public ShapeSettings ShapeSettings;
    }
}
