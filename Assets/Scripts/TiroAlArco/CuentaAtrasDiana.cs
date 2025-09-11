using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CuentaAtrasDiana : MonoBehaviour
{
    public GameObject[] numberModels;
    public Transform displayPoint;
    public float countdownInterval = 1.0f;
    public int startNumber = 60;
    public string nextSceneName;

    public ContadorPuntuacion contador;
    public GeneradorDeDianas generador;

    private List<GameObject> currentDigits = new List<GameObject>();

    void Start()
    {
        if (contador == null || generador == null || displayPoint == null)
        {
            Debug.LogError("Comprueba inspector de cuentaAtras");
            return;
        }
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        for (int number = startNumber; number >= 0; number--)
        {
            ClearPreviousDigits();
            DisplayNumber(number);
            yield return new WaitForSeconds(countdownInterval);
        }

        generador.enabled = false;

        if (PuntosDiana.Instance != null)
        {
            GuardarPuntuacion.Instance.GuardarTiempoArco(startNumber);
            PuntosDiana.Instance.GuardarPuntuacionFinal(contador.GetPuntuacion());
        }

        SceneManager.LoadScene(nextSceneName);
    }

    void ClearPreviousDigits()
    {
        foreach (GameObject digit in currentDigits)
        {
            Destroy(digit);
        }
        currentDigits.Clear();
    }

    void DisplayNumber(int number)
    {
        char[] digits = number.ToString("D2").ToCharArray();
        float spacing = 1.0f;
        float totalWidth = (digits.Length - 1) * spacing;

        for (int i = 0; i < digits.Length; i++)
        {
            int digitValue = (int)char.GetNumericValue(digits[i]);
            if (digitValue < 0 || digitValue >= numberModels.Length) continue;

            Vector3 position = displayPoint.position + new Vector3((i * spacing) - totalWidth / 2f, 0, 0);
            GameObject digitGO = Instantiate(numberModels[digitValue], position, displayPoint.rotation, displayPoint);
            currentDigits.Add(digitGO);
        }
    }
}