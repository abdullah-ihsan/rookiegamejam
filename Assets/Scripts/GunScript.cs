using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
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
    }

    IEnumerator StartShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        //Debug.Log("Tried Shooting");
        /*Debug.Log("Shot");
        _animator.SetTrigger("isShot");*/
        /*GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");*/
        //rb.transform.LookAt(enemies[0].transform);
        if ((Physics.Raycast(_laserpoint.position, rb.transform.forward, out hit, range)) && hit.collider.gameObject.CompareTag("Enemy")) ;
        {
            _laserLine.SetPosition(0, _laserpoint.position);
            Debug.DrawRay(rb.transform.position, rb.transform.forward*hit.distance ,Color.red);
            _laserLine.SetPosition(1,hit.point);
            Debug.Log(hit.transform.name);
            Debug.Log("Shot");
            _animator.SetTrigger("isShot");
            StartCoroutine(ShootLaser());
        }
        //return true;
    }

    IEnumerator ShootLaser()
    {
        _laserLine.enabled = true;
        yield  return new WaitForSeconds(_laserduration);
        _laserLine.enabled = false;
    }
}
