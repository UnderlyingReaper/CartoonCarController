using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostForce;


    Rigidbody _rigidBody;


    void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!CarController.Instance.isBoosting ||  CarController.Instance.vertical <= 0) return;

        _rigidBody.AddForceAtPosition(_rigidBody.mass * boostForce * transform.forward, transform.position);

        Debug.Log(Vector3.Dot(_rigidBody.transform.forward, _rigidBody.velocity));
    }
}
