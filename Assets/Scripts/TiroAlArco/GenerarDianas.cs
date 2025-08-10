using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneradorDeDianas : MonoBehaviour
{
    [SerializeField]
    private GameObject[] dianaPrefabs;

    [SerializeField]
    private int cantidadDeDianas = 5;

    [SerializeField]
    private float radioDeGeneracion = 5f;

    [SerializeField]
    private float retrasoEntreRondas = 3f;

    private Vector3 puntoCentral;
    private List<GameObject> dianasInstanciadas = new List<GameObject>();
    private int dianasVivas;

    private void Start()
    {
        puntoCentral = transform.position;
        GenerarRonda();
    }

    private void GenerarRonda()
    {
        if (dianaPrefabs.Length == 0)
        {
            Debug.LogError("Prefabs no asignados");
            return;
        }
        
        dianasVivas = cantidadDeDianas;
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

            Quaternion rotacionDeInstanciacion = Quaternion.LookRotation(puntoCentral - posicionDeInstanciacion);

            GameObject dianaInstanciada = Instantiate(prefabElegido, posicionDeInstanciacion, rotacionDeInstanciacion);
            
            dianaInstanciada.GetComponent<Diana>().OnDianaMuerte.AddListener(NotificarMuerteDeDiana);

            dianasInstanciadas.Add(dianaInstanciada);
        }
    }
    
    public void NotificarMuerteDeDiana()
    {
        dianasVivas--;
        if (dianasVivas <= 0)
        {
            StartCoroutine(CicloDeReinicio());
        }
    }

    private IEnumerator CicloDeReinicio()
    {
        yield return new WaitForSeconds(retrasoEntreRondas);
        foreach (GameObject diana in dianasInstanciadas)
        {
            if (diana != null)
            {
                Destroy(diana);
            }
        }
        dianasInstanciadas.Clear();
        GenerarRonda();
    }
}