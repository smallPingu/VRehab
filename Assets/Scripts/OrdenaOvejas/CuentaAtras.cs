using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CountdownManager : MonoBehaviour
{
    public GameObject[] numberModels;
    public Transform displayPoint;
    public float countdownInterval = 1.0f;
    public int startNumber = 25;

    private List<GameObject> currentDigits = new List<GameObject>();

    public Contador contadorNegras;
    public Contador contadorBlancas;
    public string nextSceneName;

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
            yield return new WaitForSeconds(countdownInterval);
        }

        Debug.Log("Countdown finished!");

        // Store data
        if (GuardarPuntuacion.Instance != null)
        {
            GuardarPuntuacion.Instance.GuardarNegras(contadorNegras.GetPuntuacion());
            GuardarPuntuacion.Instance.GuardarBlancas(contadorBlancas.GetPuntuacion());
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
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

            Vector3 position = displayPoint.position + new Vector3((i * spacing) - totalWidth / 2f, 0, 0);
            GameObject digitGO = Instantiate(numberModels[digitValue], position, displayPoint.rotation);
            currentDigits.Add(digitGO);
        }
    }
}
