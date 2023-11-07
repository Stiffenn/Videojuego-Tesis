using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Componente que bloquea el movimiento de la nave.
/// </summary>
public class MovementBlocker : MonoBehaviour
{
    /// <summary>
    /// Lista estática con todos los componentes de éste tipo.
    /// </summary>
    public static readonly List<MovementBlocker> Instances = new List<MovementBlocker>();

    /// <summary>
    /// Rango al que la nave tiene que estar para que éste componente la considere demasiado cerca.
    /// </summary>
    [field: SerializeField]
    public float DetectionRange { get; set; } = 1600;

    private Transform _savedTransform;

    /// <summary>
    /// Método utilizado para detectar si la nave está en rango de éste componente o no.
    /// </summary>
    public bool IsInRange(Transform target)
    {
        float distancia = Vector3.Distance(target.position, _savedTransform.position);

        // Si la distancia es mayor al rango de detección, no consideramos que la nave esté en rango.
        if(distancia > DetectionRange)
            return false;

        // Confirmamos que está en rango, así que devolvemos 'true'.
        return true;
    }

    private void Awake()
    {
        _savedTransform = transform;
        Instances.Add(this);
    }

    private void OnDestroy()
    {
        Instances.Remove(this);
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
#endif
}