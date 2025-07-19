using UnityEngine;
using System.Collections.Generic;

public class ContadorBlancas : MonoBehaviour
{
    public GameObject[] numberModels; // Modelos 0 al 9
    public Transform displayPoint;
    public int currentValue = 0;

    private List<GameObject> currentDigits = new List<GameObject>();

    public void Increment()
    {
        currentValue++;
        UpdateDisplay();
    }

    public void ResetCounter()
    {
        currentValue = 0;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        // Limpiar los modelos anteriores
        foreach (GameObject digit in currentDigits)
        {
            Destroy(digit);
        }
        currentDigits.Clear();

        // Separar el número actual en dígitos
        char[] digits = currentValue.ToString().ToCharArray();
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

    private void Start()
    {
        UpdateDisplay(); // Mostrar el 0 al inicio
    }

    public int GetPuntuacion()
    {
        return currentValue;
    }
}
