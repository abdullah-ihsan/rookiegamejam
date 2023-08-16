using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private FloatingJoystick _joystick;

    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;
    // Start is called before the first frame update
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        //_rb = GetComponent<Rigidbody>();
        _offset = transform.position - _player.position;

    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = _joystick.Horizontal;
        //float verticalInput = _joystick.Vertical;

        //_rb.velocity = new Vector3(horizontalInput * _movementSpeed, _rb.velocity.y, verticalInput * _movementSpeed);

        transform.position = new Vector3(_player.position.x + _offset.x, transform.position.y, _player.position.z + _offset.z);
    }
}
