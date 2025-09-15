using UnityEngine;
using System.Collections.Generic;

public class MonedasFinal : MonoBehaviour
{
    private int valorAMostrar;

    public GameObject[] modelosNumeros;

    public Transform puntoVisualizacion;

    public float espaciado = 1.0f;
    
    void Start()
    {
        if (GuardarPuntuacion.Instance != null)
            valorAMostrar = GuardarPuntuacion.Instance.GetPuntCoches();
        else
            valorAMostrar = 0;
        MostrarValor();
    }

    public void MostrarValor()
    {
        string cadenaValor = valorAMostrar.ToString();

        float anchoTotal = (cadenaValor.Length - 1) * espaciado;
        float desplazamientoInicio = anchoTotal / 2f;

        for (int i = 0; i < cadenaValor.Length; i++)
        {
            char caracterActual = cadenaValor[i];

            int valorDigito = (int)char.GetNumericValue(caracterActual);

            if (valorDigito >= 0 && valorDigito < modelosNumeros.Length && modelosNumeros[valorDigito] != null)
            {
                GameObject modeloAInstanciar = modelosNumeros[valorDigito];
                
                Vector3 posicionLocal = new Vector3(desplazamientoInicio - (i * espaciado), 0, 0);

                Vector3 posicionMundo = puntoVisualizacion.TransformPoint(posicionLocal);

                GameObject objetoInstanciado = Instantiate(modeloAInstanciar, posicionMundo, puntoVisualizacion.rotation);
                
                objetoInstanciado.transform.SetParent(puntoVisualizacion);
            }
            else
            {
                Debug.LogWarning($"No se encontró un modelo para el dígito {valorDigito}.");
            }
        }
    }
}