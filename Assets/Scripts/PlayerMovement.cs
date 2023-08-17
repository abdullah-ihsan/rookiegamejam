//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float movementSpeed = 5f;

    [SerializeField] private FloatingJoystick _joystick;

    private Animator _animator;
    private int isWalkingHash;

    public static int score;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private GameObject[] piecesPrefab;

    public void setMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame

    // void Update()
    /*{
        if (transform.rotation.x != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.rotation.y, transform.rotation.z);
            transform.localPosition = new Vector3(transform.position.x, 0.06f, transform.position.z);
        }
    }*/

    private void OnEnable()
    {
        EnemyMovement.OnEnemyKilled += IncrementScore;
        CowScript.OnEnemyEaten += IncrementScore;
        EnemyMovement.OnEnemyKilled += StartMakingPieces;
    }

    void FixedUpdate()
    {
        bool forwardKey = Input.GetKey("w");
        bool isWalking = _animator.GetBool(isWalkingHash);

        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;
        _rb.velocity = new Vector3( horizontalInput* movementSpeed, _rb.velocity.y,  verticalInput* movementSpeed);

        
        if (horizontalInput != 0 || verticalInput != 0 && !isWalking)
        {
            _animator.SetBool(isWalkingHash, true);
            transform.rotation = Quaternion.LookRotation(_rb.velocity);
        }
        
        if (horizontalInput == 0 && verticalInput == 0 && isWalking)
        {
            _animator.SetBool(isWalkingHash, false);
        }
    }

    void IncrementScore()
    {
        //score++;
        scoreText.text = "Score: " + score.ToString(); 
    }

    void StartMakingPieces()
    {
        StartCoroutine(makePieces());
    }

    IEnumerator makePieces()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(piecesPrefab[Random.Range(0, piecesPrefab.Length)], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}


