using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;

    [SerializeField] private Button _button;

    private Rigidbody rb;

    // Update is called once per frame
    private void Awake(){
        _button.onClick.AddListener(Shoot);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
    }

    void Shoot()
    {
        RaycastHit hit;
        Debug.Log("Shot");
        if(Physics.Raycast(rb.transform.position, rb.transform.forward, out hit, range))
        {
            
            Debug.Log(hit.transform.name);
        }
        //return true;
    }
}
