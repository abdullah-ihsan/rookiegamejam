using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesScript : MonoBehaviour
{
    [SerializeField] private Transform _flowpoint;
    [SerializeField] private Transform _insideBowl;
    [SerializeField] private float flowSpeed = 20f;
    private bool atBowl = false;

    private void Awake()
    {
        _flowpoint = GameObject.FindGameObjectWithTag("Flowpoint").transform;
        _insideBowl = GameObject.FindGameObjectWithTag("InsideBowl").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!atBowl)
            gameObject.transform.position = Vector3.MoveTowards(transform.position, _flowpoint.position, flowSpeed * Time.deltaTime);
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, _insideBowl.position, flowSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flowpoint"))
        {
            atBowl = true;
        }
        if (other.gameObject.CompareTag("InsideBowl"))
        {
            Destroy(this.gameObject);
        }
    }
}
