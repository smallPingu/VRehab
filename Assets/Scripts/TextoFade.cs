using UnityEngine;
using TMPro;
using System.Collections;

public class TextoFade : MonoBehaviour
{
    public TextMeshPro texto;

    [Tooltip("Tiempo en segundos que el texto está totalmente visible.")]
    public float duracionVisible = 3.0f;

    [Tooltip("Tiempo en segundos que tarda el texto en desvanecerse.")]
    public float duracionDesvanecimiento = 1.0f;

    public void MostrarMensaje(string mensaje)
    {
        texto.text = mensaje;
        
        StopAllCoroutines();
        StartCoroutine(SecuenciaMostrarYDesvanecer());
    }

    private IEnumerator SecuenciaMostrarYDesvanecer()
    {
        texto.alpha = 1.0f;

        yield return new WaitForSeconds(duracionVisible);

        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < duracionDesvanecimiento)
        {
            float nuevoAlpha = Mathf.Lerp(1.0f, 0.0f, tiempoTranscurrido / duracionDesvanecimiento);
            texto.alpha = nuevoAlpha;
            
            tiempoTranscurrido += Time.deltaTime;
            
            yield return null;
        }

        texto.alpha = 0.0f;
    }
}