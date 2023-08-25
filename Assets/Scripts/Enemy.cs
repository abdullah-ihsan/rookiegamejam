using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] private float _maxhealth = 10;
    private float _currenthealth ;

    [SerializeField] private FloatingHealthBar _healthBar;

    private Rigidbody _rb;

    [SerializeField]private Transform _target;
    [SerializeField] private Transform _laserpoint;

    [SerializeField] private float suckSpeed = 30f;
    [SerializeField] private float suckScale = 0.3f;
    private Vector3 _scale;
    
    private NavMeshAgent _agent;

    [SerializeField] private LayerMask groundLayer, playerLayer;

    private bool isActive = true;
    
    private Vector3 _moveDirection;
    private Renderer _rd;

    private Animator _animator;
    private int isPushingHash;
    [SerializeField] private EnemyHealth _enemyhealth;

    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    public delegate void EnemyGotToBowl();
    public static event EnemyGotToBowl OnEnemyGotToBowl;


    private bool isDead = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        isPushingHash = Animator.StringToHash("isPushing");
        _agent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Bowl").transform;
        _healthBar = GetComponentInChildren<FloatingHealthBar>();
        _laserpoint = GameObject.FindGameObjectWithTag("Laserpoint").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        _agent.speed = movementSpeed;
        _currenthealth = _maxhealth;
        //_enemyhealth.UpdateHealthbar(_maxhealth, _currenthealth);
        //GameObject.DontDestroyOnLoad(_target.gameObject);
    }


    void FixedUpdate()
    {
        if (isActive)
        {
            _agent.SetDestination(_target.position);
           
        }
       
    }

    public bool getIsDead()
    {
        return isDead;
    }

    public void setIsDead(bool set)
    {
        isDead = set;
    }

    public void setIsActive(bool set)
    {
        isActive = set;
    }

    public bool getIsActive()
    {
        return isActive;
    }

    private void Update()
    {
        if (isDead)
        {
            _scale = transform.localScale;
            gameObject.transform.position = Vector3.MoveTowards(transform.position, _laserpoint.position, suckSpeed *Time.deltaTime);
            _scale -= new Vector3(suckScale, suckScale, suckScale);
            //_scale.x -= suckScale;
            //_scale.y -= suckScale;
            //_scale.z -= suckScale;
            if(_scale.x > 0 || _scale.y > 0 || _scale.z > 0) 
                transform.localScale = _scale;
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
            this.gameObject.tag = "EnemyAtBowl";
            GameObject[] enemiesatbowl = GameObject.FindGameObjectsWithTag("EnemyAtBowl");
            this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            this.gameObject.GetComponent<NavMeshObstacle>().enabled = true;
            this.gameObject.GetComponent<Target>().enabled = false;
            if(enemiesatbowl.Length >= 3)
            {
                StartCoroutine(GameOver());
            }
            if(OnEnemyGotToBowl != null)
            {
                OnEnemyGotToBowl();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isDead)
        {
            GameObject.Destroy(this.gameObject);
            
        }
      
    }

    public void takeDamage(float damageAmount)
    {
        _currenthealth -= damageAmount;
        if (_currenthealth <= 0)
        {
            InitiateDeath();
        }
        _healthBar.UpdateHealthBar(_currenthealth, _maxhealth);
        
    }
    private void InitiateDeath()
    {
        PlayerMovement.score++;
        isDead = true;
        isActive = false;
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.providesContacts = false;
        _agent.isStopped = true;
        _agent.enabled = false;
        gameObject.GetComponent<Target>().enabled = false;
        //StartCoroutine(Die());
        this.gameObject.tag = "Untagged";
        _animator.SetTrigger("Death");
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }

        //if (OnEnemyKilled != null)
        //{
        //    OnEnemyKilled();
        //}

    }
    IEnumerator Die()
    {
        _animator.SetTrigger("Death");
        _agent.isStopped = true;
        yield return new WaitForSeconds(3f);
        if (gameObject != null)
        {
            GameObject.Destroy(this.gameObject);
        }
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
    }


    IEnumerator GameOver()
    {
        Debug.Log("Game Over!");
        _target.gameObject.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(3f);
        //PlayerMovement.score = 0;
        SceneManager.LoadScene("GameOver");
    }

    
}
