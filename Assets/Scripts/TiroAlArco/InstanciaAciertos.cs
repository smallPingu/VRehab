using System.Collections;
using UnityEngine;

public class GeneradorDeParticulas : MonoBehaviour
{
    public GameObject prefabParticula;
    public int numeroDeParticulas = 10;
    public float tiempoDeEsperaInicial = 0.5f;
    public float decrementoDeEspera = 0.05f;
    public float tiempoDeEsperaMinimo = 0.1f;
    public float radioDeCirculo = 10f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
        StartCoroutine(GenerarParticulasConPausa());
    }

    IEnumerator GenerarParticulasConPausa()
    {
        if (prefabParticula == null)
        {
            Debug.LogError("El prefab de la partícula no está asignado. Por favor, asigna un GameObject al campo prefabParticula en el Inspector.");
            yield break;
        }

        float tiempoDeEsperaActual = tiempoDeEsperaInicial;

        for (int i = 0; i < numeroDeParticulas; i++)
        {
            Vector2 puntoAleatorioEnCirculo = Random.insideUnitCircle * radioDeCirculo;
            Vector3 offsetAleatorio = new Vector3(puntoAleatorioEnCirculo.x, 0, puntoAleatorioEnCirculo.y);
            
            GameObject nuevaParticula = Instantiate(prefabParticula, posicionInicial + offsetAleatorio, Quaternion.identity);
            nuevaParticula.transform.parent = this.transform;
            yield return new WaitForSeconds(tiempoDeEsperaActual);
            
            tiempoDeEsperaActual = Mathf.Max(tiempoDeEsperaMinimo, tiempoDeEsperaActual - decrementoDeEspera);
        }
    }
}
