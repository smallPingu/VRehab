using UnityEngine;

public class PuntosDiana : MonoBehaviour
{
    public static PuntosDiana Instance { get; private set; }

    public int PuntuacionFinal { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void GuardarPuntuacionFinal(int puntuacion)
    {
        PuntuacionFinal = puntuacion;
    }
}