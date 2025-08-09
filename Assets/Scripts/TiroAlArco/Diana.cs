using UnityEngine;

public interface IHittable
{
    void GetHit();
}

public class Diana : MonoBehaviour, IHittable
{
    private Rigidbody rb;
    
    [SerializeField]
    private int salud = 1;

    [SerializeField]
    private AudioSource audioSource;

    private const string etiquetaDeFlecha = "Flecha";

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
        salud--;
        if (salud <= 0)
        {
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