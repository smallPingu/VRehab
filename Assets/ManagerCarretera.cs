using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerCarretera : MonoBehaviour
{
    // ¿Convertir en array de posibles carreteras a generar?
    public GameObject seccionCarretera;
    private int carreterasIniciales = 12;
    private Queue<GameObject> instanciasCreadas = new Queue<GameObject>();
    void Awake()
    {
        GameObject aux;
        for (int i = -carreterasIniciales; i < carreterasIniciales; i++)
        {
            aux = Instantiate(seccionCarretera, new Vector3(0, 0, i * 20), Quaternion.identity);
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
        
        GameObject objetoABorrar = instanciasCreadas.Dequeue();
        Destroy(objetoABorrar);
        
    }

}
