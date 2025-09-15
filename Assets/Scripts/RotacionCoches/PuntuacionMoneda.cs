using UnityEngine;
using System.Collections.Generic;

public class PuntuacionMoneda : MonoBehaviour
{
    public GameObject[] modelosNumeros;

    public Transform puntoVisualizacion;

    public float espaciado = 1.0f;

    private int puntuacionActual = 0;
    private List<GameObject> objetosDigitoActuales = new List<GameObject>();

    private void Start()
    {
        ActualizarVisualizacionPuntuacion();
    }

    public void EstablecerPuntuacion(int nuevaPuntuacion)
    {
        puntuacionActual = nuevaPuntuacion;
        ActualizarVisualizacionPuntuacion();
    }

    public void SumarPuntos(int puntosAAgregar)
    {
        puntuacionActual += puntosAAgregar;
        ActualizarVisualizacionPuntuacion();
    }
    public void ActualizarVisualizacionPuntuacion()
    {
        foreach (GameObject digito in objetosDigitoActuales)
        {
            Destroy(digito);
        }
        objetosDigitoActuales.Clear();

        string cadenaPuntuacion = puntuacionActual.ToString();

        float anchoTotal = (cadenaPuntuacion.Length - 1) * espaciado;
        float desplazamientoInicio = anchoTotal / 2f;

        for (int i = 0; i < cadenaPuntuacion.Length; i++)
        {
            char caracterActual = cadenaPuntuacion[i];

            int valorDigito = (int)char.GetNumericValue(caracterActual);

            if (valorDigito >= 0 && valorDigito < modelosNumeros.Length && modelosNumeros[valorDigito] != null)
            {
                GameObject modeloAInstanciar = modelosNumeros[valorDigito];

                Vector3 posicionLocal = new Vector3(desplazamientoInicio - (i * espaciado), 0, 0);

                Vector3 posicionMundo = puntoVisualizacion.TransformPoint(posicionLocal);

                GameObject objetoInstanciado = Instantiate(modeloAInstanciar, posicionMundo, puntoVisualizacion.rotation);

                objetoInstanciado.transform.SetParent(puntoVisualizacion);

                objetosDigitoActuales.Add(objetoInstanciado);
            }
            else
            {
                Debug.LogWarning($"No se encontró un modelo para el dígito {valorDigito}.");
            }
        }
    }

    public int GetMonedasFinales()
    {
        return puntuacionActual;
    }
}