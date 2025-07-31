using UnityEngine;
using UnityEngine.SceneManagement;

public class DestruirCoches : MonoBehaviour
{
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
                SceneManager.LoadScene(escenaObjetivo);
        }
    }
}