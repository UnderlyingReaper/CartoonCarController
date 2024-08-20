using UnityEngine;

public class Acceleration : MonoBehaviour
{
    [SerializeField] float rayRange;
    [SerializeField] float maxTorque;
    [SerializeField] AnimationCurve powerCurve;


    
    Rigidbody _rigidBody;


    void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!CarController.Instance.isOnGround) return;

        // Get direction of acceleration
        Vector3 accelDir = transform.forward * Mathf.Sign(CarController.Instance.vertical);

        // Calculate car speed
        float carSpeed = Vector3.Dot(_rigidBody.transform.forward, _rigidBody.velocity);

        // Get % of car speed relative to top speed
        float normalizeSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / CarController.Instance.topSpeed);

        // Calculate available Torque to apply
        float availableTorque = powerCurve.Evaluate(normalizeSpeed) * maxTorque * Mathf.Abs(CarController.Instance.vertical);

        // Calculate force to apply
        Vector3 force = accelDir * _rigidBody.mass * availableTorque;

        // Apply force
        _rigidBody.AddForceAtPosition(force, transform.position);


        // Debug
        Debug.DrawRay(transform.position, force/100, Color.blue, 0.1f);
    }
}
