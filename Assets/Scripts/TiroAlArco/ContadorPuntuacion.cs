using UnityEngine;

public class ContadorPuntuacion : MonoBehaviour
{
    private int puntuacion = 0;

    public void IncrementarPuntuacion()
    {
        puntuacion++;
    }

    public int GetPuntuacion()
    {
        return puntuacion;
    }
}