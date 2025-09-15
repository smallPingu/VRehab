using UnityEngine;

public class GuardarPuntuacion : MonoBehaviour
{
    public static GuardarPuntuacion Instance;

    private int puntuacionNegras;
    private int puntuacionBlancas;
    private int puntuacionCoches;
    private int dianasAcertadas;
    
    private float tiempoOvejas;
    private float tiempoCoches;
    private float tiempoArco;

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

    #region Juego de Ovejas

    public void GuardarPuntNegras(int punt)
    {
        puntuacionNegras = punt;
    }

    public void GuardarPuntBlancas(int punt)
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

    public void GuardarTiempoOvejas(float tiempo)
    {
        tiempoOvejas = tiempo;
    }

    public float GetTiempoOvejas()
    {
        return tiempoOvejas;
    }

    #endregion

    #region Juego de RotacionCoches

    public void GuardarPuntCoches(int punt)
    {
        puntuacionCoches = punt;
    }

    public int GetPuntCoches()
    {
        return puntuacionCoches;
    }

    public void GuardarTiempoCoches(float tiempo)
    {
        tiempoCoches = tiempo;
    }

    public float GetTiempoCoches()
    {
        return tiempoCoches;
    }

    #endregion

    #region Juego de TiroAlArco

    public void GuardarDianasAcertadas(int dianas)
    {
        dianasAcertadas = dianas;
    }

    public int GetDianasAcertadas()
    {
        return dianasAcertadas;
    }

    public void GuardarTiempoArco(float tiempo)
    {
        tiempoArco = tiempo;
    }

    public float GetTiempoArco()
    {
        return tiempoArco;
    }

    #endregion

    public void ResetearTodasLasPuntuaciones()
    {
        puntuacionNegras = 0;
        puntuacionBlancas = 0;
        puntuacionCoches = 0;
        dianasAcertadas = 0;

        tiempoOvejas = 0f;
        tiempoCoches = 0f;
        tiempoArco = 0f;

        Debug.Log("Todas las puntuaciones y tiempos han sido reseteados.");
    }
}