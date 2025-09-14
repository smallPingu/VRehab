using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SeccionTrigger : MonoBehaviour
{
    public List<GameObject> seccionCarretera;
    private ManagerCarretera manager;
    public GameObject monedaPrefab;

    void Start()
    {
        manager = FindFirstObjectByType<ManagerCarretera>();
    }

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.gameObject.CompareTag("TriggerCarretera"))
        {
            GameObject aux;
            int dist = manager.GetCarrInic();
            float radianesGeneracion = manager.GetGradosGen() * Mathf.Deg2Rad;
            aux = Instantiate(seccionCarretera[Random.Range(0, seccionCarretera.Count)], new Vector3((18f) * dist * Mathf.Cos(radianesGeneracion), 0, (18f) * dist * Mathf.Sin(radianesGeneracion) - 0.01f), Quaternion.Euler(0, -(manager.GetGradosGen() - 90), 0));

            GenerarMonedas(aux);

            // Para que las nuevas carreteras se puedan mover
            MoverDireccionJugadores mover = aux.GetComponent<MoverDireccionJugadores>();

            mover.SetDireccion(new Vector3(0, 0, 0));
            mover.InicializarDireccion(false);

            manager.BorrarPrimeraCarretera(); //Borramos sección de carretera no visible
            manager.AniadirCarreteraLista(aux);
        }
    }
    
    void GenerarMonedas(GameObject seccionPadre)
    {
        foreach (Transform puntoAparicion in seccionPadre.transform)
        {
            if (puntoAparicion.CompareTag("PuntoMoneda"))
            {
                GameObject monedaGenerada = Instantiate(monedaPrefab, puntoAparicion.position, Quaternion.Euler(-90, 90, 0));
                
                monedaGenerada.transform.SetParent(seccionPadre.transform);
            }
        }
    }
}
