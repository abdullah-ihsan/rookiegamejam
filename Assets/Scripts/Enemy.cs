using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GunScript _fire;
    [SerializeField] private float movementSpeed;

    [SerializeField] private float _maxhealth = 10;
    private float _currenthealth ;

    [SerializeField] private FloatingHealthBar _healthBar;

    private Rigidbody _rb;

    [SerializeField]private Transform _target;
    
    private NavMeshAgent _agent;

    [SerializeField] private LayerMask groundLayer, playerLayer;

    private bool isActive = true;
    
    private Vector3 _moveDirection;

    private Animator _animator;
    private int isPushingHash;
    [SerializeField] private EnemyHealth _enemyhealth;

    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        isPushingHash = Animator.StringToHash("isPushing");
        _agent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Bowl").transform;
        _healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _agent.speed = movementSpeed;
        _currenthealth = _maxhealth;
        //_enemyhealth.UpdateHealthbar(_maxhealth, _currenthealth);
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

    public void takeDamage(float damageAmount)
    {
        _currenthealth -= damageAmount;
        if (_currenthealth <= 0)
        {
            StartCoroutine(Die());
        }
        _healthBar.UpdateHealthBar(_currenthealth, _maxhealth);
        
    }

    IEnumerator Die()
    {
        _animator.SetTrigger("Death");
        _agent.isStopped = true;
        yield return new WaitForSeconds(3f);
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
        GameObject.Destroy(this.gameObject);
    }
}
