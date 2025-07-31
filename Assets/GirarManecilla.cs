using UnityEngine;

public class GirarManecilla : MonoBehaviour
{
    private float anguloObjetivo = 0f;
    private const float ROTACION_POR_SEGUNDO = 6f;
    public float velocidadAguja = 2f;
    private float duracionAnimacion;
    private float tiempoTranscurridoAnimacion = 0f;
    private bool estaGirando = true;
    private const float ANGULO_INICIAL = -90f;

    void Start()
    {
        if (ContadorArriba.Instance == null)
        {
            Debug.LogError("ContadorArriba.Instance no está disponible.");
            return;
        }

        anguloObjetivo = ContadorArriba.Instance.GetTiempoFinal() * ROTACION_POR_SEGUNDO;

        float anguloObjetivoFinal = anguloObjetivo + ANGULO_INICIAL;

        duracionAnimacion = Mathf.Abs(anguloObjetivoFinal - ANGULO_INICIAL) / (ROTACION_POR_SEGUNDO * velocidadAguja);

        if (anguloObjetivo == 0)
        {
            estaGirando = false;
        }

        transform.localRotation = Quaternion.Euler(ANGULO_INICIAL, transform.localEulerAngles.y, transform.localEulerAngles.z);

        ContadorArriba.Instance.PararContador();
    }

    void Update()
    {
        if (estaGirando)
        {
            tiempoTranscurridoAnimacion += Time.deltaTime;
            
            float progreso = tiempoTranscurridoAnimacion / duracionAnimacion;
            float t = Mathf.SmoothStep(0f, 1f, progreso);

            float anguloActual = Mathf.Lerp(ANGULO_INICIAL, anguloObjetivo + ANGULO_INICIAL, t);

            transform.localRotation = Quaternion.Euler(anguloActual, transform.localEulerAngles.y, transform.localEulerAngles.z);

            if (tiempoTranscurridoAnimacion >= duracionAnimacion)
            {
                estaGirando = false;
                transform.localRotation = Quaternion.Euler(anguloObjetivo + ANGULO_INICIAL, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
        }
    }
}