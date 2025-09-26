using UnityEngine;

public class VelocidadCarretera : MonoBehaviour
{
    public float velocidad = 5f;

    void Start()
    {
        if (ParametrosInicial.instance != null)
        {
            float velocidadConfigurada = ParametrosInicial.instance.paramRotacionCoches.velocidadJuego;

            velocidad *= velocidadConfigurada;

            Debug.Log("Velocidad de la carretera configurada a: " + velocidad);
        }
        else
        {
            Debug.LogWarning("No se encontró ParametrosInicial.instance. Usando velocidad por defecto: " + velocidad);
        }
    }

    public float GetVelocidad()
    {
        return velocidad;
    }

    public void SetVelocidad(float cambio)
    {
        velocidad = cambio;
    }
}