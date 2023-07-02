using UnityEngine;

public class Planeta : Cuerpo
{
    [SerializeField]
    private string _key;

    [SerializeField]
    private Color[] _colors;

    /// <inheritdoc />
    protected override void Awake()
    {
        base.Awake();

        Material.SetColor(_key, _colors[Random.Range(0, _colors.Length)]);
    }
}
