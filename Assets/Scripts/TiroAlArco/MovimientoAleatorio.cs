using UnityEngine;

public class MovimientoAleatorio : DianaMovimiento
{
    private Vector3 posicionOrigen;

    [SerializeField]
    private float radioDeMovimiento = 2f;

    protected override void Awake()
    {
        base.Awake();
        posicionOrigen = transform.position;
        siguientePosicion = GetNuevaPosicionDeMovimiento();
    }

    private Vector3 GetNuevaPosicionDeMovimiento()
    {
        return posicionOrigen + (Vector3)Random.insideUnitCircle * radioDeMovimiento;
    }

    protected override void Mover()
    {
        if (Vector3.Distance(transform.position, siguientePosicion) < umbralDeLlegada)
        {
            siguientePosicion = GetNuevaPosicionDeMovimiento();
        }

        Vector3 direccion = siguientePosicion - transform.position;
        if (rb != null)
        {
            rb.MovePosition(transform.position + direccion.normalized * Time.fixedDeltaTime * velocidad);
        }
    }
}