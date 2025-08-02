using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionFlecha : MonoBehaviour
{
    //[SerializeField]
    private Rigidbody rb;

    void Awake()
    {
        // Podriamos buscar la componente al ser del mismo objeto
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.forward = Vector3.Slerp(transform.forward, rb.linearVelocity.normalized, Time.fixedDeltaTime);
    }

}