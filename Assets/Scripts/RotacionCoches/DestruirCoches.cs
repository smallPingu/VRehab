using UnityEngine;
using UnityEngine.SceneManagement;

public class DestruirCoches : MonoBehaviour
{
    public PuntuacionMoneda gestorPuntuacion;
    public string etiquetaDeObjetivo = "Coche";
    private int cocheDestruidos;

    public string escenaObjetivo = "MenuPrincipal";

    void Start()
    {
        cocheDestruidos = 0;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(etiquetaDeObjetivo))
        {
            Destroy(other.gameObject);
            cocheDestruidos++;
            if (cocheDestruidos > 1)
            {
                GuardarPuntuacion.Instance?.GuardarPuntCoches(gestorPuntuacion.GetMonedasFinales());
                SceneManager.LoadScene(escenaObjetivo);
            }
        }
    }
}