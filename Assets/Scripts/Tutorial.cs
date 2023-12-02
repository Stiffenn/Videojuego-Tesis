using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public enum Tutoriales
    {
        Warp, Scanner, Potenciador,
    }

    public Image WarpImage;
    public Image ScannerImage;
    public Slider Potenciador;
    public Slider Carga;
    public float CargaVelocidad = 1f;
    public float LerpMultiplier = 10f;

    public readonly Dictionary<Tutoriales, bool> TutorialesCompletados = new Dictionary<Tutoriales, bool>();

    void Next()
    {
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Awake()
    {
        Control.MessageReceived += OnMessageREceived;

        foreach (Tutoriales item in Enum.GetValues(typeof(Tutoriales)))
        {
            TutorialesCompletados[item] = false;
        }
    }

    private void OnMessageREceived(float potenciador, bool warpPressed, bool scannerPressed)
    {
        if(warpPressed)
        {
            WarpImage.color = Color.green;
            TutorialesCompletados[Tutoriales.Warp] = true;
        }

        if(scannerPressed)
        {
            ScannerImage.color = Color.green;
            TutorialesCompletados[Tutoriales.Scanner] = true;
        }

        if(potenciador > 200)
        {
            TutorialesCompletados[Tutoriales.Potenciador] = true;
        }

        Potenciador.value = Mathf.Lerp(Potenciador.value, potenciador, Time.deltaTime * LerpMultiplier);
    }

    void UpdateCarga()
    {
        if (TutorialesCompletados.Any(t => !t.Value))
            return;

        Carga.value -= Time.deltaTime * CargaVelocidad;

        if (Carga.value <= 0)
        {
            Carga.value = 100;
            Next();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCarga();
    }

    private void OnDestroy()
    {
        Control.MessageReceived -= OnMessageREceived;
    }
}
