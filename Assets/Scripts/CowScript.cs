using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static EnemyMovement;

public class CowScript : MonoBehaviour
{
    private GameObject EnemyInContact;

    private Animator _animator;

    public delegate void EnemyEaten();
    public static event EnemyEaten OnEnemyEaten;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EatEnemy(other.gameObject);

        }
    }

    private void EatEnemy(GameObject EnemyInContact)
    {
        //EnemyInContact = collision.gameObject;
        //EnemyInContact = collision.gameObject;
        EnemyInContact.tag = "EnemyAtBowl";
        EnemyInContact = GameObject.FindGameObjectWithTag("EnemyAtBowl");
        _animator.SetTrigger("isEating");
        EnemyInContact.GetComponent<EnemyMovement>().setIsActive(false);
        EnemyInContact.GetComponent<NavMeshAgent>().enabled = false;
        EnemyInContact.GetComponent<BoxCollider>().enabled = false;
        EnemyInContact.GetComponent<Target>().enabled = false;
        transform.LookAt(EnemyInContact.transform);
        if (OnEnemyEaten != null)
        {
            OnEnemyEaten();
        }
        //EnemyInContact.tag = "Untagged";
     
        Destroy(EnemyInContact.gameObject);

    }


}
