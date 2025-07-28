using UnityEngine;

public class DestruirCoches : MonoBehaviour
{
    public string etiquetaDeObjetivo = "Coche";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(etiquetaDeObjetivo))
        {
            Destroy(other.gameObject);
        }
    }
}