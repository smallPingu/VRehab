using UnityEngine;
using System.Collections.Generic;

public class GeneraOvejas : MonoBehaviour
{
    [Header("Configuración de Ovejas")]
    public List<GameObject> objetosAInstanciar;
    public float radioDelCirculo = 100f;
    public float alturaMinima = 50f;
    public float alturaMaxima = 150f;
    public bool rotacionAleatoria = true;
    public Vector3 ejeDeRotacion = Vector3.up;

    [Header("Generación Continua")]
    public bool generarContinuamente = true;
    public float intervaloDeGeneracion = 1.0f;

    [Header("Optimización")]
    public bool padreAlGenerador = true; // Emparentar los objetos instanciados a este GameObject para organización en Unity

    private float tiempoSiguienteGeneracion;
    //private List<GameObject> objetosGenerados = new List<GameObject>(); // Para seguimiento y limpieza si es necesario

    void Start()
    {
        tiempoSiguienteGeneracion = Time.time + intervaloDeGeneracion;
    }

    void Update()
    {
        if (generarContinuamente && Time.time >= tiempoSiguienteGeneracion)
        {
            GenerarUnObjeto();
            tiempoSiguienteGeneracion = Time.time + intervaloDeGeneracion;
        }
    }

    [ContextMenu("Generar Un Objeto Ahora")]
    public void GenerarUnObjeto()
    {
        if (objetosAInstanciar == null || objetosAInstanciar.Count == 0)
        {
            Debug.LogError("¡La lista de Objetos a Instanciar está vacía o no asignada! Por favor, asigna prefabs en el Inspector.");
            return;
        }

        // Elegir un prefab aleatorio de la lista
        GameObject prefabElegido = objetosAInstanciar[Random.Range(0, objetosAInstanciar.Count)];

        // 1. Obtener un ángulo aleatorio en radianes
        float angulo = Random.Range(0f, 2f * Mathf.PI);

        // 2. Calcular las posiciones X y Z en el círculo
        float x = Random.Range(1, transform.position.x + radioDelCirculo * Mathf.Cos(angulo));
        float z = Random.Range(1, transform.position.z + radioDelCirculo * Mathf.Sin(angulo));

        // 3. Obtener una posición Y (altura) aleatoria
        float y = Random.Range(alturaMinima, alturaMaxima);

        // 4. Combinar en una posición final
        Vector3 posicionAleatoria = new Vector3(x, y, z);

        // 5. Determinar la rotación
        Quaternion rotacionAleatoriaQ = Quaternion.identity * Quaternion.AngleAxis(90f, Vector3.left);
        if (rotacionAleatoria)
        {
            rotacionAleatoriaQ = Quaternion.AngleAxis(Random.Range(0f, 360f), ejeDeRotacion);
        }

        // 6. Instanciar el objeto
        GameObject nuevoObjeto = Instantiate(prefabElegido, posicionAleatoria, rotacionAleatoriaQ);

        // 7. Emparentar el objeto para organización
        if (padreAlGenerador)
        {
            nuevoObjeto.transform.SetParent(this.transform);
        }

        //objetosGenerados.Add(nuevoObjeto);
        Debug.Log($"Se generó un objeto '{prefabElegido.name}' en la posición {posicionAleatoria}.");
    }

    /*
    Si quisiera limpiar los objetos generados, usaría esta función
    
    public void LimpiarTodosLosObjetosGenerados()
    {
        foreach (GameObject obj in objetosGenerados)
        {
            if (obj != null)
            {
                DestroyImmediate(obj);
            }
        }
        objetosGenerados.Clear();
        Debug.Log("Se limpiaron todos los objetos generados previamente.");
    }
    */
}