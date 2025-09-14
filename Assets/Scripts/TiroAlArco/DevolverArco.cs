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
    public AudioSource sonido;
    public GameObject prefabParticulasReset;

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

            if (prefabParticulasReset != null)
            {
                GameObject efectoInstanciado = Instantiate(prefabParticulasReset, datosIniciales.posicion, Quaternion.identity);

                ParticleSystem ps = efectoInstanciado.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    Destroy(efectoInstanciado, ps.main.duration + ps.main.startLifetime.constantMax);
                }
                else
                {
                    Destroy(efectoInstanciado, 3f); 
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        sonido.Play();
        ResetearTransform();
    }
}