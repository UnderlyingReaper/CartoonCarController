using UnityEngine;

public class Brakes : MonoBehaviour
{
    [SerializeField] float mass;
    [SerializeField] float brakeForce;


    Rigidbody _rigidBody;


    void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!CarController.Instance.isHandBraking || !CarController.Instance.isOnGround) return;

        // Calculate car speed
        float carSpeed = Vector3.Dot(_rigidBody.transform.forward, _rigidBody.velocity);

        // Get direction in which to apply force
        Vector3 forceDir = -transform.forward * Mathf.Sign(carSpeed);

        // Apply force
        _rigidBody.AddForceAtPosition(mass * brakeForce * forceDir, transform.position);
        
        
        // Debug
        Debug.DrawRay(transform.position, (mass * brakeForce * forceDir)/100, Color.white, 0.01f);
    }
}