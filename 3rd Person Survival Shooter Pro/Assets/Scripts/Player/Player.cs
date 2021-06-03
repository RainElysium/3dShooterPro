using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private Camera _mainCamera;
    [SerializeField]
    private Vector3 _playerVelocity;
    [Header ("Controller Settings")]
    [SerializeField]
    private float _playerSpeed = 7.0f;
    [SerializeField]
    private float _jumpHeight = 10.0f;
    [SerializeField]
    private float _gravityValue = 1f;
    [Header ("Camera Settings")]
    [SerializeField]
    private float _mouseSens = 1;
    [SerializeField]
    private bool _isGrounded;
    private float _yVelocity;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (!_controller)
            Debug.LogError("Character Controller is NULL");

        _mainCamera = Camera.main;

        if (!_mainCamera)
            Debug.LogError("Main Camera is NULL");

        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        CalculateMovement();
        CameraMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }

    void CalculateMovement()
    {
        _isGrounded = _controller.isGrounded;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        _playerVelocity = direction * _playerSpeed;

        // Changes the height position of the player..
        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _yVelocity = _jumpHeight;
        }
        else
            _yVelocity -= _gravityValue;

        _playerVelocity.y = _yVelocity;

        _playerVelocity = transform.TransformDirection(_playerVelocity);

        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // look left and right
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _mouseSens;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        // look up and down

        Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * _mouseSens;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0f, 26f);
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

}
