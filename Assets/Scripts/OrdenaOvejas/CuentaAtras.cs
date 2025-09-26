using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CountdownManager : MonoBehaviour
{
    public GameObject[] numberModels;
    public Transform displayPoint;
    public float countdownInterval = 1.0f;

    public Contador contadorNegras;
    public Contador contadorBlancas;
    public string nextSceneName;
    private int startNumber;
    private float gameSpeed;
    private List<GameObject> currentDigits = new List<GameObject>();

    void Awake()
    {
        if (ParametrosInicial.instance != null)
        {
            startNumber = ParametrosInicial.instance.paramOrdenaOvejas.cuentaAtras;
            gameSpeed = ParametrosInicial.instance.paramOrdenaOvejas.velocidadJuego;
        }
        else
        {
            Debug.LogWarning("No se encontró ParametrosInicial.instance. Usando valores por defecto.");
            startNumber = 25;
            gameSpeed = 1f;
        }

        Time.timeScale = gameSpeed;
    }

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        for (int number = startNumber; number >= 0; number--)
        {
            ClearPreviousDigits();
            DisplayNumber(number);

            yield return new WaitForSecondsRealtime(countdownInterval);
        }

        Debug.Log("Countdown finished!");

        if (GuardarPuntuacion.Instance != null)
        {
            GuardarPuntuacion.Instance.GuardarPuntNegras(contadorNegras.GetPuntuacion());
            GuardarPuntuacion.Instance.GuardarPuntBlancas(contadorBlancas.GetPuntuacion());
            GuardarPuntuacion.Instance.GuardarTiempoOvejas(startNumber);
        }

        SceneManager.LoadScene(nextSceneName);
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
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
        char[] digits = number.ToString().ToCharArray();
        float spacing = 1.0f;
        float totalWidth = (digits.Length - 1) * spacing;

        for (int i = 0; i < digits.Length; i++)
        {
            int digitValue = (int)char.GetNumericValue(digits[i]);
            if (digitValue >= 0 && digitValue < numberModels.Length)
            {
                Vector3 position = displayPoint.position + new Vector3((i * spacing) - totalWidth / 2f, 0, 0);
                GameObject digitGO = Instantiate(numberModels[digitValue], position, displayPoint.rotation);
                currentDigits.Add(digitGO);
            }
        }
    }
}