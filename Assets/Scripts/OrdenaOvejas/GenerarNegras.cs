using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerarNegras : MonoBehaviour
{
    public GameObject ovejaNegra;

    [Header("Generación")]
    public float intervaloInicial = 1f;
    public float intervaloActual;
    public float intervaloMax = 0.1f;

    [Header("Dígitos 3D (0-9)")]
    public GameObject[] digitos3D; // Prefabs de 0 a 9
    public Vector3 offsetDigitos = new Vector3(0, 2f, 0); // Posición sobre el generador

    private List<GameObject> digitosInstanciados = new List<GameObject>();

    void Start()
    {
        intervaloActual = intervaloInicial;
        int numGener = GuardarPuntuacion.Instance.GetPuntNegras();
        Debug.Log("Puntuacion negras: " + numGener);

        StartCoroutine(Generar(numGener));
    }

    IEnumerator Generar(int num)
    {
        for (int i = 0; i < num; i++)
        {
            MostrarNumero(i);

            Vector3 randomOffset = new Vector3(
                Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)
            );

            Instantiate(
                ovejaNegra,
                transform.position + transform.TransformDirection(randomOffset),
                Random.rotation
            );

            yield return new WaitForSecondsRealtime(intervaloActual);

            if (intervaloActual > intervaloMax)
                intervaloActual -= (intervaloActual - intervaloMax) * 0.15f;
            else if (intervaloActual < intervaloMax)
                intervaloActual = intervaloMax;
        }

        // Mostrar el valor final también
        MostrarNumero(num);
    }

    void MostrarNumero(int numero)
    {
        foreach (GameObject obj in digitosInstanciados)
            Destroy(obj);
        digitosInstanciados.Clear();

        char[] digitosChar = numero.ToString().ToCharArray();
        float espaciado = 0.6f;

        Vector3 inicio = transform.position + offsetDigitos
                         - new Vector3((digitosChar.Length - 1) * espaciado / 2f, 0, 0);

        for (int i = 0; i < digitosChar.Length; i++)
        {
            int digito = digitosChar[i] - '0';
            if (digito < 0 || digito > 9) continue;

            Vector3 posicion = inicio + new Vector3(i * espaciado, 0, 0);
            GameObject obj = Instantiate(digitos3D[digito], posicion, Quaternion.Euler(0, 180, 0));
            //obj.transform.SetParent(transform);
            digitosInstanciados.Add(obj);
        }
    }
}
