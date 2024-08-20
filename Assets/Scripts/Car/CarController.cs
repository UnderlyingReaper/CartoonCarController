using UnityEngine;

public class CarController : MonoBehaviour
{
    public float topSpeed;

    [Header("groundCheck")]
    [SerializeField] float rayRange;
    public bool isOnGround;

    [Header("Inputs")]
    public float horizontal;
    public float vertical;
    public bool isHandBraking;
    public bool isBoosting;

    public static CarController Instance { get; private set;}


    PlayerInput _playerInput;


    void Awake()
    {
        if(Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        _playerInput = new PlayerInput();
        
    }

    void OnEnable() => _playerInput.Car.Enable();
    void OnDisable() => _playerInput.Car.Disable();

    void Update()
    {
        if(Physics.Raycast(transform.position, -transform.up, rayRange)) isOnGround = true;
        else isOnGround = false;

        Vector2 values = _playerInput.Car.Movement.ReadValue<Vector2>();
        vertical = values.y;
        horizontal = values.x;

        isHandBraking = _playerInput.Car.Handbrake.IsPressed();
        isBoosting = _playerInput.Car.Boost.IsPressed();
    }
}
