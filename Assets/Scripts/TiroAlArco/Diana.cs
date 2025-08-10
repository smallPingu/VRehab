using UnityEngine;
using UnityEngine.Events; // Requerido para UnityEvent

public interface IHittable
{
    void GetHit();
}

public class Diana : MonoBehaviour, IHittable
{
    public UnityEvent OnDianaMuerte;
    
    [Header("Configuración")]
    [SerializeField] private int salud = 1;
    [SerializeField] private AudioSource audioSource;
    
    private Rigidbody rb;
    private const string etiquetaDeFlecha = "Flecha";
    private bool muerto = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource != null && rb != null && rb.isKinematic && !collision.gameObject.CompareTag(etiquetaDeFlecha))
        {
            audioSource.Play();
        }
    }

    public void GetHit()
    {
        if (muerto) return;

        salud--;

        if (salud <= 0)
        {
            muerto = true;

            OnDianaMuerte.Invoke();

            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    public int GetSalud()
    {
        return salud;
    }
}