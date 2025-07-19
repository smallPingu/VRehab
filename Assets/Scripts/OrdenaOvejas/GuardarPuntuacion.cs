using UnityEngine;
public class GuardarPuntuacion : MonoBehaviour
{
    public static GuardarPuntuacion Instance;

    private int puntuacionNegras;
    private int puntuacionBlancas;

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

    public void GuardarNegras(int punt)
    {
        puntuacionNegras = punt;
    }

    public void GuardarBlancas(int punt)
    {
        puntuacionBlancas = punt;
    }

    public int GetPuntNegras()
    {
        return puntuacionNegras;
    }

    public int GetPuntBlancas()
    {
        return puntuacionBlancas;
    }
}