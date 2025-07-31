using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField]private float _jumpForce = 5.0f;

    [SerializeField] private float _damping = 0.1f;

    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _jumpInput;

    private Rigidbody _rigidbody;

    private Vector2 _moveDirection;
    private float _currentVelocity;

    private float _sphereRadius = 0.1f; 



    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerInputReading();
        Debug.DrawLine(this.transform.position, Vector3.down * 0.15f);
    }


    private void FixedUpdate()
    {
        AddMovement();
    }

    private void PlayerInputReading()
    {
        _moveDirection = _moveInput.action.ReadValue<Vector2>();
        if (_jumpInput.action.triggered && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
    private void AddMovement()
    {
        _currentVelocity = Mathf.Lerp(_currentVelocity, _moveDirection.x * _moveSpeed * Time.fixedDeltaTime, Time.fixedDeltaTime * _damping);
        _rigidbody.linearVelocity = new Vector3(_currentVelocity, 0.0f);
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down * 0.1f);
        RaycastHit hitInfo;
        if (Physics.SphereCast(ray, _sphereRadius, out hitInfo) && hitInfo.collider.CompareTag("Ground") )
        {
            return true;
        }
        return false;

    }


}
