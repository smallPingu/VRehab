using UnityEngine;
using System.Collections.Generic;

public class Contador : MonoBehaviour
{
    public GameObject[] numberModels;
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
        foreach (GameObject digit in currentDigits)
        {
            Destroy(digit);
        }
        currentDigits.Clear();

        char[] digits = currentValue.ToString().ToCharArray();
        float spacing = 1.0f;

        float totalWidth = (digits.Length - 1) * spacing;
        float startOffset = totalWidth / 2f;

        for (int i = 0; i < digits.Length; i++)
        {
            int digitValue = (int)char.GetNumericValue(digits[i]);

            Vector3 localPosition = new Vector3(startOffset - (i * spacing), 0, 0);

            Vector3 worldPosition = displayPoint.TransformPoint(localPosition);

            GameObject digitGO = Instantiate(numberModels[digitValue], worldPosition, displayPoint.rotation);

            digitGO.transform.SetParent(displayPoint);

            currentDigits.Add(digitGO);
        }
    }

    private void Start()
    {
        UpdateDisplay();
    }

    public int GetPuntuacion()
    {
        return currentValue;
    }
}