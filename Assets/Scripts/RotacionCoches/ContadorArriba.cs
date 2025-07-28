using UnityEngine;
using System.Collections.Generic;

public class ContadorArriba : MonoBehaviour
{
    public GameObject[] modelosNumeros;

    public GameObject modeloDosPuntos;

    public Transform puntoVisualizacion;

    public float espaciado = 1.0f;

    private float tiempoTranscurrido = 0f;
    private int ultimoSegundoMostrado = -1;
    private List<GameObject> objetosMostradosActuales = new List<GameObject>();

    public void ReiniciarTemporizador()
    {
        tiempoTranscurrido = 0f;
        ultimoSegundoMostrado = -1;
        ActualizarVisualizacion();
    }

    public float ObtenerTiempoTranscurrido()
    {
        return tiempoTranscurrido;
    }

    private void Update()
    {
        tiempoTranscurrido += Time.deltaTime;

        int segundoActual = Mathf.FloorToInt(tiempoTranscurrido);

        if (segundoActual != ultimoSegundoMostrado)
        {
            ultimoSegundoMostrado = segundoActual;
            ActualizarVisualizacion();
        }
    }

    private void Start()
    {
        ActualizarVisualizacion();
    }

    private void ActualizarVisualizacion()
    {
        foreach (GameObject obj in objetosMostradosActuales)
        {
            Destroy(obj);
        }
        objetosMostradosActuales.Clear();

        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60f);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60f);

        string cadenaMinutos = minutos.ToString("00");
        string cadenaSegundos = segundos.ToString("00");

        string cadenaTiempo = cadenaMinutos;
        if (modeloDosPuntos != null)
        {
            cadenaTiempo += ":";
        }
        cadenaTiempo += cadenaSegundos;

        float anchoTotal = (cadenaTiempo.Length - 1) * espaciado;
        float desplazamientoInicio = anchoTotal / 2f;

        for (int i = 0; i < cadenaTiempo.Length; i++)
        {
            char caracterAMostrar = cadenaTiempo[i];
            GameObject modeloAInstanciar = null;

            if (char.IsDigit(caracterAMostrar))
            {
                int valorDigito = (int)char.GetNumericValue(caracterAMostrar);
                if (valorDigito >= 0 && valorDigito < modelosNumeros.Length && modelosNumeros[valorDigito] != null)
                {
                    modeloAInstanciar = modelosNumeros[valorDigito];
                }
                else
                {
                    Debug.LogWarning($"TimerDisplay: Falta el modelo de número para el dígito {valorDigito} en el índice {valorDigito}.");
                    continue;
                }
            }
            else if (caracterAMostrar == ':' && modeloDosPuntos != null)
            {
                modeloAInstanciar = modeloDosPuntos;
            }
            else if (caracterAMostrar == ':' && modeloDosPuntos == null)
            {
                Debug.LogWarning("TimerDisplay: El modelo de dos puntos no está asignado, se omite la visualización de los dos puntos.");
                continue;
            }
            else
            {
                Debug.LogWarning($"TimerDisplay: Carácter inesperado '{caracterAMostrar}' en la cadena de tiempo.");
                continue;
            }

            if (modeloAInstanciar != null)
            {
                Vector3 posicionLocal = new Vector3(desplazamientoInicio - (i * espaciado), 0, 0);
                Vector3 posicionMundo = puntoVisualizacion.TransformPoint(posicionLocal);

                GameObject objetoInstanciado = Instantiate(modeloAInstanciar, posicionMundo, puntoVisualizacion.rotation);
                objetoInstanciado.transform.SetParent(puntoVisualizacion);

                if (caracterAMostrar == ':' && modeloDosPuntos != null)
                {
                    objetoInstanciado.transform.localRotation *= Quaternion.Euler(-90, 0, 0);
                }

                objetosMostradosActuales.Add(objetoInstanciado);
            }
        }
    }
}
