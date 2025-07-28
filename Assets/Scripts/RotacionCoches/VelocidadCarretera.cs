using UnityEngine;

public class VelocidadCarretera : MonoBehaviour
{
    public float velocidad = 5f;

    public float GetVelocidad()
    {
        return velocidad;
    }

    public void SetVelocidad(float cambio)
    {
        velocidad = cambio;
    }
}
