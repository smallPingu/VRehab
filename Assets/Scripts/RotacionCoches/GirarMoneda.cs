using UnityEngine;

public class GirarMoneda : MonoBehaviour
{
    public float velocidadMaxima = 90f;

    public float duracionCiclo = 4f;

    public AnimationCurve curvaDeVelocidad;

    private float temporizadorCiclo = 0f;

    void Update()
    {
        temporizadorCiclo += Time.deltaTime;
        if (temporizadorCiclo > duracionCiclo)
        {
            temporizadorCiclo -= duracionCiclo;
        }

        float progresoCiclo = temporizadorCiclo / duracionCiclo;

        float multiplicadorVelocidad = curvaDeVelocidad.Evaluate(progresoCiclo);

        float velocidadActual = velocidadMaxima * multiplicadorVelocidad;
        transform.Rotate(Vector3.forward, velocidadActual * Time.deltaTime);
    }
}