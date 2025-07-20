using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoverDireccionJugadores : MonoBehaviour
{
    public float velocidad = 5f;

    private Vector3 direccionDeMovimiento;

    private Vector3 centro;

    public Vector3 GetDireccion()
    {
        return direccionDeMovimiento;
    }

    public void SetDireccion(Vector3 dir)
    {
        direccionDeMovimiento = dir;
    }

    public void InicializarDireccion(bool alejar)
    {
        centro = new Vector3(0f, 0f, 0f);

        Vector3 vectorHaciaJugadores = centro - transform.position;

        if (alejar)
            direccionDeMovimiento = -vectorHaciaJugadores.normalized;
        else
            direccionDeMovimiento = vectorHaciaJugadores.normalized;
    }

    void Update()
    {
        transform.position += direccionDeMovimiento * velocidad * Time.deltaTime;
    }
}