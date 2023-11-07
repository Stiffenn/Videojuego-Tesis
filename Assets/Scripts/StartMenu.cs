using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private Transform _tutorial;

    private void Awake()
    {
        Control.MessageReceived += MessageReceived;
    }

    private void MessageReceived(float potenciador, bool warpPressed, bool scannerPressed)
    {
        if (potenciador >= 300 || warpPressed || scannerPressed)
            Next();

    }

    private void OnDestroy()
    {
        Control.MessageReceived -= MessageReceived;
    }

    public void Next()
    {
        _tutorial.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
