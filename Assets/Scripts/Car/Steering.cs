using System;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [Header("Traction & Grip")]
    [SerializeField] float rayRange; 
    [SerializeField] AnimationCurve gripFactor;
    [SerializeField] float mass;


    [Header("Wheel Sterring")]
    [SerializeField] bool doesTurn;
    [SerializeField] float maxTurnAngle;
    [SerializeField] AnimationCurve turnAngleModifier;

    Rigidbody _rigidBody;
    float _carSpeed;
    float _normalizeSpeed;



    void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        if(!doesTurn) return;

        transform.localRotation = Quaternion.Euler(new Vector3(0, maxTurnAngle * CarController.Instance.horizontal * turnAngleModifier.Evaluate(_normalizeSpeed), 0));
    }

    void FixedUpdate()
    {
        if(!CarController.Instance.isOnGround) return;

        Vector3 steeringDir = transform.right;

        // Get world velocity
        Vector3 tiredWorlVel = _rigidBody.GetPointVelocity(transform.position);

        // Speed of the car
        _carSpeed = Vector3.Dot(_rigidBody.transform.forward, _rigidBody.velocity);
        _normalizeSpeed = Mathf.Clamp01(Math.Abs(_carSpeed) / CarController.Instance.topSpeed); // % of the speed of the car

        // Get velocity of the car in the forward direction
        float steeringVel = Vector3.Dot(steeringDir, tiredWorlVel);

        // Calculate the change in velocity
        float desiredVelChange = -steeringVel * gripFactor.Evaluate(_normalizeSpeed);

        // Calculate the Acceleration
        float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

        // Apply Acceleration For Grip
        _rigidBody.AddForceAtPosition(steeringDir * mass * desiredAccel, transform.position);


        // Debug
        Debug.DrawRay(transform.position, (steeringDir * mass * desiredAccel)/1000, Color.magenta, 1);
        Debug.DrawRay(transform.position, tiredWorlVel/100, Color.yellow, 1);
    }
}
