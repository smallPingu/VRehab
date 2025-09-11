using UnityEngine;
using UnityEngine.Events;

public class BotonLogin : MonoBehaviour
{
    public GameObject boton;
    public UnityEvent onPresion;
    public UnityEvent onSuelta;
    GameObject presionar;
    AudioSource sonido;
    bool isPresionado;

    void Start()
    {
        sonido = GetComponent<AudioSource>();
        isPresionado = false;
    }


    private void OnTriggerEnter(Collider otro)
    {
        if (!isPresionado)
        {
            boton.transform.localPosition -= new Vector3(0, 0.03f, 0);
            presionar = otro.gameObject;
            onPresion.Invoke();
            sonido.Play();
            isPresionado = true;
        }
    }

    private void OnTriggerExit(Collider otro)
    {
        if (otro.gameObject == presionar)
        {
            boton.transform.localPosition += new Vector3(0, 0.03f, 0);
            onSuelta.Invoke();
            isPresionado = false;
        }
    }
}
