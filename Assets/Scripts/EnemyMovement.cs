using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Rigidbody _rb;

    [SerializeField]private Transform _target;
    
    private NavMeshAgent _agent;

    [SerializeField] private LayerMask groundLayer, playerLayer;

    private bool isActive = true;
    
    private Vector3 _moveDirection;

    private Animator _animator;
    private int isPushingHash;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        isPushingHash = Animator.StringToHash("isPushing");
        _agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _agent.speed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        /*if (_target)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
           _rb.transform.LookAt(_target);
            _moveDirection = direction;
        }*/
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            _agent.SetDestination(_target.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Bowl")
        {
            isActive = false;
            _animator.SetBool(isPushingHash,true);
            _rb.velocity = new Vector3(0,0,0);
            _agent.SetDestination(transform.position);
            _agent.isStopped = true;
        }
    }
}
