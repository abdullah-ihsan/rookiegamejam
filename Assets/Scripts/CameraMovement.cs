using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private FixedJoystick _joystick;

    [SerializeField] private float _movementSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;

        _rb.velocity = new Vector3(horizontalInput * _movementSpeed, _rb.velocity.y, verticalInput * _movementSpeed);
    }
}
