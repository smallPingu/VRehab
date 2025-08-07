using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdherirFlechaSuperficie : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private SphereCollider colliderEsfera;

    [SerializeField]
    private GameObject flechaAdherir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        colliderEsfera.isTrigger = true;

        GameObject arrow = Instantiate(flechaAdherir);
        arrow.transform.position = transform.position;
        arrow.transform.forward = transform.forward;

        if (collision.collider.attachedRigidbody != null)
        {
            arrow.transform.parent = collision.collider.attachedRigidbody.transform;
        }

        collision.collider.GetComponent<IHittable>()?.GetHit();

        Destroy(gameObject);

    }
}