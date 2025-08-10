using UnityEngine;

public class MovimientoRecto : DianaMovimiento
{
    private Vector3 posicionOrigen;
    private Vector3 posicionFinal;

    [SerializeField]
    private float distanciaDeMovimiento = 2f;

    protected override void Awake()
    {
        base.Awake();
        posicionOrigen = transform.position;
        posicionFinal = posicionOrigen + transform.right * distanciaDeMovimiento;
        siguientePosicion = posicionFinal;
    }

    protected override void Mover()
    {
        if (Vector3.Distance(transform.position, siguientePosicion) < umbralDeLlegada)
        {
            if (siguientePosicion == posicionFinal)
            {
                siguientePosicion = posicionOrigen;
            }
            else
            {
                siguientePosicion = posicionFinal;
            }
        }

        Vector3 direccion = siguientePosicion - transform.position;
        if (rb != null)
        {
            rb.MovePosition(transform.position + direccion.normalized * Time.fixedDeltaTime * velocidad);
        }
    }
}