using UnityEngine;

public class BallController : MonoBehaviour
{
    public float bounceForce = 10f; // Adjust this value to control bounce strength

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision was with an object tagged as "Paddle"
        // (You'll need to set up this tag later)
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Calculate the direction opposite to the collision normal
            // (the direction the ball came from relative to the surface it hit)
            Vector3 bounceDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);

            // Apply an impulse force to the ball to make it bounce
            // Use ForceMode.Impulse for an instant force, like a hit
            GetComponent<Rigidbody>().AddForce(bounceDirection.normalized * bounceForce, ForceMode.Impulse);

            // Optional: Add some upward force for more exaggerated bounce
            // GetComponent<Rigidbody>().AddForce(Vector3.up * bounceForce * 0.5f, ForceMode.Impulse);
        }
    }
}