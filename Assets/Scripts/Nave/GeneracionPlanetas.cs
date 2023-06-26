using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GeneracionPlanetas : MonoBehaviour
{
    [SerializeField]
    private int _planetasMin = 5;

    [SerializeField]
    private int _planetasMax = 7;

    [SerializeField]
    private float _distanciaPlanetas = 3000;
    
    [SerializeField]
    private float _tamañoMax = 400;
    
    [SerializeField]
    private float _tamañoMin = 200;
    
    [SerializeField]
    private float _tamañoOffset = 2f;
    
    [SerializeField]
    private float _altura = 100;

    private float _nextPosX;
    private float _nextPosZ;

    // Start is called before the first frame update
    void Start()
    {
        SpawnearPlanetas(Random.Range(_planetasMin, _planetasMax));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnearPlanetas(int planetas)
    {
        for (int i = 0; i < planetas; i++)
        {
            GameObject planeta = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            float tamaño = Random.Range(_tamañoMin, _tamañoMax);
            float altura = Random.Range(-_altura, _altura);
            _nextPosX += Random.Range(-_distanciaPlanetas, _distanciaPlanetas) + tamaño * _tamañoOffset;
            _nextPosZ += Random.Range(-_distanciaPlanetas, _distanciaPlanetas) + tamaño * _tamañoOffset;

            Transform planetaTransform = planeta.transform;
            planetaTransform.position = new Vector3(_nextPosX, altura, _nextPosZ);
            planetaTransform.localScale = new Vector3(tamaño, tamaño, tamaño);
        }
    }
}
