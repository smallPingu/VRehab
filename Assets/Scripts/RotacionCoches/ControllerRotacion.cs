using UnityEngine;

public class ControllerRotacion : MonoBehaviour
{
    public Transform controller;          // Transform del mando
    public enum Axis { Yaw, Pitch, Roll }
    public Axis axis = Axis.Roll;         // Default: wrist tilt
    public float angleThreshold = 10f;    // Degrees to ignore as noise
    public float speed = 3f;              // Units/second

    float neutralAngle;

    void Start()
    {
        if (controller) neutralAngle = GetAxisAngle();
    }

    void Update()
    {
        if (!controller) return;

        float angleDelta = Mathf.DeltaAngle(neutralAngle, GetAxisAngle());

        if (Mathf.Abs(angleDelta) <= angleThreshold) return;

        float dir = Mathf.Sign(angleDelta);              // -1 left, +1 right
        transform.Translate(Vector3.left * dir * speed * Time.deltaTime, Space.World);
    }

    float GetAxisAngle()
    {
        return axis switch
        {
            Axis.Pitch => controller.eulerAngles.x,
            Axis.Yaw   => controller.eulerAngles.y,
            _          => controller.eulerAngles.z,       // Roll
        };
    }

    // Call this from a UI button, input action, etc.
    public void Recenter() => neutralAngle = GetAxisAngle();
}
