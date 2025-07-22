using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerCarretera : MonoBehaviour
{
    public GameObject seccionCarretera;
    private int carreterasIniciales = 12;
    private Queue<GameObject> instanciasCreadas = new Queue<GameObject>();
    public float gradosGeneracion;

    void Awake()
    {
        float radianesGeneracion = gradosGeneracion * Mathf.Deg2Rad;
        GameObject aux;
        Vector3 direccionCero = new Vector3(0,0,0);

        for (int i = -carreterasIniciales; i < carreterasIniciales; i++)
        {
            aux = Instantiate(seccionCarretera, new Vector3((20f) * i * Mathf.Cos(radianesGeneracion), 0, (20f) * i * Mathf.Sin(radianesGeneracion) - 0.01f), Quaternion.Euler(0, -(gradosGeneracion - 90), 0));
            MoverDireccionJugadores mover = aux.GetComponent<MoverDireccionJugadores>();
            
            if (i < 0)
            {
                mover.InicializarDireccion(true); 

                if (i == -1)
                {
                    direccionCero = mover.GetDireccion();
                }
            }
            else if (i == 0)
            {
                mover.SetDireccion(direccionCero); 
            }
            else 
            {
                mover.InicializarDireccion(false);
            }

            AniadirCarreteraLista(aux);
        }
    }

    public void AniadirCarreteraLista(GameObject nuevaInstancia)
    {
        if (nuevaInstancia != null)
        {
            instanciasCreadas.Enqueue(nuevaInstancia);
        }
    }

    public void BorrarPrimeraCarretera()
    {
        if (instanciasCreadas.Count > 0)
        {
            GameObject objetoABorrar = instanciasCreadas.Dequeue();
            Destroy(objetoABorrar);
        }
    }
}