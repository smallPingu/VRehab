using UnityEngine;
using System.Collections.Generic;

public class GeneradorDeDianas : MonoBehaviour
{
    [SerializeField]
    private GameObject[] dianaPrefabs;

    [SerializeField]
    private int cantidadDeDianas = 5;

    [SerializeField]
    private float radioDeGeneracion = 5f;

    private Vector3 puntoCentral;

    private void Start()
    {
        puntoCentral = transform.position;

        if (dianaPrefabs.Length == 0)
        {
            Debug.LogError("Prefabs no asignados");
            return;
        }

        List<int> weightedPrefabIndices = new List<int>();
        for (int i = 0; i < dianaPrefabs.Length; i++)
        {
            int weight = dianaPrefabs.Length - i;
            for (int j = 0; j < weight; j++)
            {
                weightedPrefabIndices.Add(i);
            }
        }

        for (int i = 0; i < cantidadDeDianas; i++)
        {
            int indicePonderado = weightedPrefabIndices[Random.Range(0, weightedPrefabIndices.Count)];
            GameObject prefabElegido = dianaPrefabs[indicePonderado];

            Vector2 posicionAleatoriaEnCirculo = Random.insideUnitCircle.normalized * radioDeGeneracion;
            Vector3 posicionDeInstanciacion = new Vector3(
                puntoCentral.x + posicionAleatoriaEnCirculo.x,
                puntoCentral.y,
                puntoCentral.z + posicionAleatoriaEnCirculo.y
            );

            // Rotación para mirar al jugador
            Quaternion rotacionDeInstanciacion = Quaternion.LookRotation(puntoCentral - posicionDeInstanciacion);

            Instantiate(prefabElegido, posicionDeInstanciacion, rotacionDeInstanciacion);
        }
    }
}
