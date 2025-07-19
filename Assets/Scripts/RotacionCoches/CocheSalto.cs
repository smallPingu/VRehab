using UnityEngine;
using UnityEngine.InputSystem;

public class CocheSalto : MonoBehaviour
{
    public InputActionProperty jumpAction; // Asignar acción (por ejemplo, 'primaryButton')
    public float jumpForce = 5f;
    private Rigidbody rb;

    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on this GameObject!");
        }
    }

    void Update()
    {
        if (jumpAction.action.WasPressedThisFrame() && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
