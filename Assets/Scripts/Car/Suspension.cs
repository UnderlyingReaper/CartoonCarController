using UnityEngine;

public class Suspension : MonoBehaviour
{
    [SerializeField] float raycastRange;
    [SerializeField] float springLength;
    [SerializeField] float springStrength;
    [SerializeField] float springDamping;

    Rigidbody _rigidBody;


    void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody>();
    }

    void FixedUpdate()
    {   
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastRange))
        {
            Vector3 springDir = transform.up;

            // Get world velocity
            Vector3 tireWorldVel = _rigidBody.GetPointVelocity(transform.position);

            float offset = springLength - hit.distance;

            // Get velocity along the y-axis
            float velocity = Vector3.Dot(springDir, tireWorldVel);

            // Calculate force with damping;
            float force = (offset * springStrength) - (velocity * springDamping);

            // Apply force
            _rigidBody.AddForceAtPosition(springDir * force, transform.position);

            // Debug
            Debug.DrawRay(transform.position, transform.up * force/1000, Color.green, 0.01f); // Divide by 1000 to show in kN
            Debug.DrawRay(transform.position, -transform.up * raycastRange, Color.red, 0.01f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position - new Vector3(0, springLength/2, 0), new Vector3(0.1f, springLength, 0.1f));
    }
}
