using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int valorPuntuacion = 1;
    public GameObject efectoDesaparecer;
    public AudioClip sonidoAlRecoger;

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.gameObject.CompareTag("Coche"))
        {
            Object.FindFirstObjectByType<PuntuacionMoneda>().SumarPuntos(valorPuntuacion);

            if (efectoDesaparecer != null)
            {
                Instantiate(efectoDesaparecer, transform.position, Quaternion.identity);
            }

            if (sonidoAlRecoger != null)
            {
                AudioSource.PlayClipAtPoint(sonidoAlRecoger, transform.position);
            }

            Destroy(gameObject);
        }
    }
}