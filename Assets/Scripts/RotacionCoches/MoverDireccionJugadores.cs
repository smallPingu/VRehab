using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoverDireccionJugadores : MonoBehaviour
{
    private float velocidad;

    private Vector3 direccionDeMovimiento;

    private Vector3 centro;

    void Start()
    {
        GameObject manager = GameObject.Find("ManagerCarreteras");
        if (manager != null)
        {
            VelocidadCarretera scriptVeloc = manager.GetComponent<VelocidadCarretera>();
            if (scriptVeloc != null)
            {
                velocidad = scriptVeloc.GetVelocidad();
            }
            else
            {
                Debug.LogError("Script VelocidadCarretera No Encontrado");
            }
        }
        else
        {
            Debug.LogError("Manager No Encontrado");
        }
    }

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