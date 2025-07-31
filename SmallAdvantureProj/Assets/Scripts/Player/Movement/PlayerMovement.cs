using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private float _damping = 0.1f;

    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _jumpInput;

    private Rigidbody _rigidbody;

    private Vector2 _moveDirection;
    private float _currentVelocity;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerInputReading();
    }


    private void FixedUpdate()
    {
        AddMovement();
    }

    private void PlayerInputReading()
    {
        _moveDirection = _moveInput.action.ReadValue<Vector2>();
        if (_jumpInput.action.triggered)
        {
            // Handle jump logic here
            Debug.Log("Jump triggered");
        }
    }
    private void AddMovement()
    {
        _currentVelocity = Mathf.Lerp(_currentVelocity, _moveDirection.x * _moveSpeed * Time.fixedDeltaTime, Time.fixedDeltaTime * _damping);
        _rigidbody.linearVelocity = new Vector3(_currentVelocity, 0.0f);
    }



}
