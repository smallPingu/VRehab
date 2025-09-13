using UnityEngine;

public struct TransformData
{
    public Vector3 posicion;
    public Quaternion rotacion;
    public Vector3 escala;
}

public class DevolverArco : MonoBehaviour
{
    public Transform objetoARecordar;

    private TransformData datosIniciales;
    AudioSource sonido;

    private bool isInicializado = false;

    void Awake()
    {
        if (objetoARecordar != null)
        {
            datosIniciales.posicion = objetoARecordar.position;
            datosIniciales.rotacion = objetoARecordar.rotation;
            datosIniciales.escala = objetoARecordar.localScale;

            isInicializado = true;
        }
    }

    public void ResetearTransform()
    {
        if (isInicializado)
        {
            objetoARecordar.position = datosIniciales.posicion;
            objetoARecordar.rotation = datosIniciales.rotacion;
            objetoARecordar.localScale = datosIniciales.escala;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        sonido.Play();
        ResetearTransform();
    }
}