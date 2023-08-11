using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    //[SerializeField] private EnemyMovement _enemy;
    public bool bulletHit = false;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;

    [SerializeField] private Button _button;

    [SerializeField] private Transform _laserpoint;

    [SerializeField] private float _laserduration;

    private LineRenderer _laserLine;

    private Rigidbody rb;
    private Animator _animator;
    [SerializeField]private float delay = 3;
    float timer;

    // Update is called once per frame
    private void Awake(){
        _button.onClick.AddListener(Shoot);
        _laserLine = GetComponent<LineRenderer>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        StartCoroutine(StartShooting());
    }

    void FixedUpdate()
    {
        // timer += Time.deltaTime;
        // if (timer > delay)
        // {
        //     Shoot();
        //     timer -= delay;
        // }
        SetLaser();
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
        //Physics.Raycast(_laserpoint.position, rb.transform.forward, out hit, range);
        //Debug.Log(hit.transform.name + "testing");
        if ((Physics.Raycast(_laserpoint.position, rb.transform.forward, out hit, range)) && hit.collider.gameObject.CompareTag("Enemy"))
        {
           
            Debug.DrawRay(rb.transform.position, rb.transform.forward*hit.distance ,Color.red);
            
            Debug.Log(hit.transform.name + " shot");
            
            //lower the health of enemy
            /*if (hit.transform.name == "Potato")
                _enemy.gotShot();*/


            _animator.SetTrigger("isShot");
           
        }
        StartCoroutine(ShootLaser());
    }

    void SetLaser()
    {
        RaycastHit hit;
        Physics.Raycast(_laserpoint.position, rb.transform.forward, out hit, range);
        _laserLine.SetPosition(0, _laserpoint.position);
        _laserLine.SetPosition(1,hit.point);
        _laserLine.enabled = true;
        //StartCoroutine(ShootLaser());
    }

    IEnumerator ShootLaser()
    {
        _laserLine.enabled = true;
        yield  return new WaitForSeconds(_laserduration);
        _laserLine.enabled = false;
    }
}
