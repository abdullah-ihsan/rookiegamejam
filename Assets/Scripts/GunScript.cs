using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    //[SerializeField] private EnemyMovement _enemy;
    public bool bulletHit = false;
    [SerializeField] private float damage = 0.1f;
    [SerializeField] private float range = 100f;

    [SerializeField] private Button _button;

    [SerializeField] private PlayerMovement _player;
    [SerializeField] private Transform _laserpoint;
    [SerializeField] private GameObject shurikenPrefab;

    [SerializeField] private bool isLaser = false;

    [SerializeField] private float _laserduration;

    [SerializeField] private float shootMovementSpeed = 5f;

    private float normalMovementSpeed;

    private EnemyMovement enemy;
    private Canvas enemyBar;
    private LineRenderer _laserLine;
    private ParticleSystem gunParticles; //

    private Rigidbody rb;
    private Animator _animator;
    [SerializeField]private float delay = 0.001f;
    float timer;

    // Update is called once per frame
    private void Awake(){
//        _button.onClick.AddListener(Shoot);
        _laserLine = GetComponent<LineRenderer>();
  //      gunParticles = GetComponentInChildren<ParticleSystem>();
//        gunParticles.Stop();
        _player = _laserLine.GetComponentInParent<PlayerMovement>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        normalMovementSpeed = _player.GetMovementSpeed();
        
        //StartCoroutine(StartShooting());
    }

    void FixedUpdate()
    {
        // timer += Time.deltaTime;
        // if (timer > delay)
        // {
        //     Shoot();
        //     timer -= delay;
        // }
        if (isLaser)
        {
            SetLaser();
        }
        
        Shoot();
    }

    IEnumerator StartShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        
        if ((Physics.Raycast(_laserpoint.position, rb.transform.forward, out hit, range)) && (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("UnderAttack")))
        {
            //gunParticles.Play();
            enemy = hit.transform.gameObject.GetComponent<EnemyMovement>();
            //Debug.DrawRay(rb.transform.position, rb.transform.forward*hit.distance ,Color.red);
            
            //Debug.Log(hit.transform.name + " shot");

            enemyBar = hit.transform.gameObject.GetComponentInChildren<Canvas>();
            enemyBar.enabled = true;
            //_laserLine.startColor = Color.red;
            //_laserLine.endColor = Color.red;
            //_laserLine.startWidth = 0.5f;
            //_laserLine.endWidth = 0.5f;


            timer += Time.deltaTime;
            if (timer > delay)
            {
                GameObject shuriken;
                shuriken = Instantiate(shurikenPrefab, _laserpoint.position, Quaternion.identity);
                shuriken.GetComponent<ShurikenScript>().enemy = hit.transform;
              
                timer -= delay;
            }

            _player.setMovementSpeed(shootMovementSpeed);
            _animator.SetBool("isShooting", true);
            //enemy.takeDamage(damage);
            //StartCoroutine(ThrowShurikens(hit.transform));


        }
        else
        {
            //            gunParticles.Stop();
            _animator.SetBool("isShooting", false) ;
            _player.setMovementSpeed(normalMovementSpeed);
            _laserLine.startWidth = 0.1f;
            _laserLine.endWidth = 0.1f;
            _laserLine.startColor = Color.black;
            _laserLine.endColor = Color.black;
            if(enemyBar != null && enemyBar.enabled)
                enemyBar.enabled = false;
        }
    }

    void SetLaser()
    {
        RaycastHit hit;
        Physics.Raycast(_laserpoint.position, rb.transform.forward, out hit, range);
        if (hit.transform == null)
        {
            Vector3 position = _laserpoint.transform.position + _laserpoint.forward * range;
            _laserLine.SetPosition(0, _laserpoint.position);
            _laserLine.SetPosition(1, position);
        }
        else
        {
            _laserLine.SetPosition(0, _laserpoint.position);
            _laserLine.SetPosition(1, hit.point);
            _laserLine.enabled = true;
        }
        //StartCoroutine(ShootLaser());
    }

    IEnumerator ShootLaser()
    {
        _laserLine.enabled = true;
        yield  return new WaitForSeconds(_laserduration);
        _laserLine.enabled = false;
    }

    //IEnumerator ThrowShurikens(Transform enemy)
    //{
    //    GameObject shuriken;
    //    shuriken = Instantiate(shurikenPrefab, _laserpoint.position, Quaternion.identity);
    //    shuriken.transform.position = Vector3.MoveTowards(shuriken.transform.position, enemy.transform.position, shurikenSpeed*Time.deltaTime);
    //    yield return new WaitForSeconds(0.2f);
    //}

}
