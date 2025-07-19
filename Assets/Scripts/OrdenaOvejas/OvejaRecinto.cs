using UnityEngine;
using UnityEngine.Events;

public class OvejaRecinto : MonoBehaviour
{
    public UnityEvent onSheepTriggered;
    public string colorOveja;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(colorOveja))
        {
            Destroy(other.gameObject);
            onSheepTriggered.Invoke();
        }
    }
}
