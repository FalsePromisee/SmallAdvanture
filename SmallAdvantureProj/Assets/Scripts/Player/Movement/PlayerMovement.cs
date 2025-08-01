
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _dampingSpeed = 0.1f;

    private Rigidbody _rigidbody;

    
    private InputAction _moveInput;
    private Inputs _inputsAction;


    private Vector2 _moveDirection;
    private float _currentVelocity;

    private float _raycastDistance = 1.35f; 
    [SerializeField] private float _extraFallGravity = 2.0f; 



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputsAction = new Inputs();
    }

    private void Update()
    {
        PlayerInputReading();
    }


    private void FixedUpdate()
    {
        AddMovement();


        if (_rigidbody.linearVelocity.y < 0f)
        {
            _rigidbody.linearVelocity += Vector3.down * _extraFallGravity * Time.fixedDeltaTime;            
        }
        
    }

    private void OnEnable()
    {
        _moveInput = _inputsAction.WalkControl.Move;        
        _inputsAction.WalkControl.Enable();
    }

    private void OnDisable()
    {
        _inputsAction.WalkControl.Disable();
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);        
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _raycastDistance))
        {
            return true;
        }
        return false;

    }


    private void PlayerInputReading()
    {
        _moveDirection = _inputsAction.WalkControl.Move.ReadValue<Vector2>();
        if (_inputsAction.WalkControl.Jump.triggered && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void AddMovement()
    {
        _currentVelocity = Mathf.Lerp(_currentVelocity, _moveDirection.x * _moveSpeed * Time.fixedDeltaTime, Time.fixedDeltaTime * _dampingSpeed);
        _rigidbody.linearVelocity = new Vector3(_currentVelocity, 0.0f);
    }

}
