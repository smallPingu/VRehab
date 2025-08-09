using UnityEngine;

public abstract class DianaMovimiento : MonoBehaviour
{
    protected Rigidbody rb;
    protected Vector3 siguientePosicion;

    [SerializeField]
    protected float umbralDeLlegada = 0.5f;

    [SerializeField]
    protected float velocidad = 1f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        if (GetComponent<Diana>().GetSalud() > 0)
        {
            Mover();
        }
    }

    protected abstract void Mover();
}