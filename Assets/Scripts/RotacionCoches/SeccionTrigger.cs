using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SeccionTrigger : MonoBehaviour
{
    public GameObject seccionCarretera;
    private ManagerCarretera manager;

    void Start()
    {
        manager = FindFirstObjectByType<ManagerCarretera>(); 
    }

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.gameObject.CompareTag("TriggerCarretera"))
        {
            GameObject aux;
            aux = Instantiate(seccionCarretera, new Vector3(0, 0, 215), Quaternion.identity);

            manager.BorrarPrimeraCarretera(); //Borramos sección de carretera no visible
            manager.AniadirCarreteraLista(aux);
        }
    }
}
