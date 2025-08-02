using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFlecha : MonoBehaviour
{
    [SerializeField]
    private GameObject puntoMedioVisual;

    public void PrepararFlecha()
    {
        puntoMedioVisual.SetActive(true);
    }

    public void SolatFlecha(float fuerza)
    {
        puntoMedioVisual.SetActive(false);
        Debug.Log($"La fuerza de la flecha es {fuerza}");
    }
}