using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SeccionTrigger : MonoBehaviour
{
    public GameObject seccionCarretera;

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.gameObject.CompareTag("TriggerCarretera"))
        {
            Instantiate(seccionCarretera);
        }
    }
}
