using UnityEngine;
using UnityEngine.InputSystem;

// Asegura que el GameObject siempre tenga un componente Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class CocheSalto : MonoBehaviour
{
    public InputActionProperty jumpAction;
    
    public float jumpForce = 7f;

    public Transform groundCheck;
    
    public float groundCheckRadius = 0.2f;
    
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        jumpAction.action.Enable(); 
        jumpAction.action.performed += PerformJump;
    }

    void OnDisable()
    {
        jumpAction.action.Disable(); 
        jumpAction.action.performed -= PerformJump;
    }

    void Update()
    {
        CheckIfGrounded();
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void PerformJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}