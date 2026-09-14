using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    private PlayerData _playerData;
    private float _pitch;
    private Rigidbody _rigidbody;

    private bool _isJumpPressed => Input.GetKeyDown(_jumpKey);
    private bool _isJumping;
    private bool _canJump => _isJumpPressed && !_isJumping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _isJumping = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_layerMask.Contains(collision.transform))
        {
            _isJumping = false;
        }
    }

    public void Move()
    {
        if (_isJumping) return;

        Vector3 input = ReadMovement();
        Vector3 direction = 
            transform.right * input.x + 
            transform.forward * input.z;

        Vector3 newVelocity = new Vector3(
            direction.x * _playerData.MoveSpeed,
            _rigidbody.velocity.y,
            direction.z * _playerData.MoveSpeed
            );

        _rigidbody.velocity = newVelocity;
    }

    public void Jump()
    {
        if (!_canJump) return;

        _isJumping = true;
        _rigidbody.AddForce(Vector3.up * _playerData.JumpPower);
    }

    public void Rotate()
    {
        Vector3 Input = ReadRotateInput() * _mouseSensitivity;
        transform.Rotate(0, Input.y, 0, Space.Self);

        _pitch = Mathf.Clamp(_pitch + Input.x, _minPitch, _maxPitch);

        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }
    private Vector3 ReadMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        return new Vector3(x, 0, z).normalized;
    }

    private  Vector3 ReadRotateInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        return new Vector3(-y, x, 0);
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, -transform.up);
    }

    public void SetPlayerData(PlayerData playerData)
    {
        _playerData = playerData;
    }
}