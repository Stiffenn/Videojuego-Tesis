using UnityEngine;

public class Planeta : Cuerpo
{
    private static int _nextNameIndex = 0;

    public string Name { get; private set; } = string.Empty;

    [SerializeField]
    private string[] _randomNames;

    [SerializeField]
    private string _key;

    [SerializeField]
    private Color[] _colors;

    [SerializeField]
    private float _rotateSpeed;

    private void Update()
    {
        transform.RotateAround(GeneradorSistema.Center, Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    /// <inheritdoc />
    protected override void Awake()
    {
        base.Awake();

        Material.SetColor(_key, _colors[Random.Range(0, _colors.Length)]);

        if (_nextNameIndex >= _randomNames.Length)
            _nextNameIndex = 0;

        Name = _randomNames[_nextNameIndex++];
    }
}
