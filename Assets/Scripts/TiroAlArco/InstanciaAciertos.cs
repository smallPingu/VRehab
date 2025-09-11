using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstanciaAciertos : MonoBehaviour
{
    public GameObject prefabParticula;
    private int numeroDeParticulas = 10;
    public float tiempoDeEsperaInicial = 0.5f;
    public float decrementoDeEspera = 0.05f;
    public float tiempoDeEsperaMinimo = 0.1f;
    public float radioDeCirculo = 10f;
    
    public GameObject[] numberModels;
    public Transform displayPoint;

    private Vector3 posicionInicial;
    private List<GameObject> currentDigits = new List<GameObject>();

    void Start()
    {
        if (displayPoint == null || numberModels.Length != 10)
        {
            Debug.LogError("Faltan numeros.");
            return;
        }

        if (PuntosDiana.Instance != null)
        {
            numeroDeParticulas = PuntosDiana.Instance.PuntuacionFinal;
            GuardarPuntuacion.Instance?.GuardarDianasAcertadas(PuntosDiana.Instance.PuntuacionFinal);
        }
        else
        {
            Debug.LogWarning("No se encontró una instancia de PuntosDiana.");
        }

        posicionInicial = transform.position;
        StartCoroutine(GenerarFlechasConPausa());
    }

    IEnumerator GenerarFlechasConPausa()
    {
        if (prefabParticula == null)
        {
            Debug.LogError("El prefab de la flecha no está asignado.");
            yield break;
        }

        float tiempoDeEsperaActual = tiempoDeEsperaInicial;

        DisplayNumber(0);

        for (int i = 0; i < numeroDeParticulas; i++)
        {
            Vector2 puntoAleatorioEnCirculo = Random.insideUnitCircle * radioDeCirculo;
            Vector3 offsetAleatorio = new Vector3(puntoAleatorioEnCirculo.x, 0, puntoAleatorioEnCirculo.y);
            
            GameObject nuevaParticula = Instantiate(prefabParticula, posicionInicial + offsetAleatorio, Quaternion.identity);
            nuevaParticula.transform.parent = this.transform;

            ClearPreviousDigits();
            DisplayNumber(i + 1);
            
            yield return new WaitForSeconds(tiempoDeEsperaActual);
            
            tiempoDeEsperaActual = Mathf.Max(tiempoDeEsperaMinimo, tiempoDeEsperaActual - decrementoDeEspera);
        }
    }
    
    void ClearPreviousDigits()
    {
        foreach (GameObject digit in currentDigits)
        {
            Destroy(digit);
        }
        currentDigits.Clear();
    }

    void DisplayNumber(int number)
    {
        char[] digits = number.ToString().ToCharArray();
        float spacing = 1.0f;
        float totalWidth = (digits.Length - 1) * spacing;

        for (int i = 0; i < digits.Length; i++)
        {
            int digitValue = (int)char.GetNumericValue(digits[i]);
            if (digitValue < 0 || digitValue >= numberModels.Length) continue;

            Vector3 position = displayPoint.position + new Vector3((i * spacing) - totalWidth / 2f, 0, 0);
            GameObject digitGO = Instantiate(numberModels[digitValue], position, displayPoint.rotation, displayPoint);
            currentDigits.Add(digitGO);
        }
    }
}