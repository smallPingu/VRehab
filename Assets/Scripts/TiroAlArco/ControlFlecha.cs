using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFlecha : MonoBehaviour
{
    [SerializeField]
    private GameObject puntoMedioVisual, flechaLanza, spawnFlecha;

    [SerializeField]
    private float velocidadMaxima = 10f;

    public void PrepararFlecha()
    {
        puntoMedioVisual.SetActive(true);
    }

    public void SolarFlecha(float fuerza)
    {
        puntoMedioVisual.SetActive(false);

        GameObject flecha = Instantiate(flechaLanza);
        flecha.transform.position = spawnFlecha.transform.position;
        flecha.transform.rotation = puntoMedioVisual.transform.rotation;
        Rigidbody rb = flecha.GetComponent<Rigidbody>();
        rb.AddForce(puntoMedioVisual.transform.forward * fuerza * velocidadMaxima, ForceMode.Impulse);

    }
}