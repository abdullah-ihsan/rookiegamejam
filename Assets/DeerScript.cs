using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeerScript : MonoBehaviour
{

    private GameObject[] enemies;
    private GameObject closest;

    private float distance;
    private float closetDistance = 10000f;

    [SerializeField] float deerSpeed = 20f;
    [SerializeField] float lastDuration = 10f;

    private NavMeshAgent _agent;

    public delegate void EnemyEaten();
    public static event EnemyEaten OnEnemyEaten;

    // Start is called before the first frame update
    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _agent = GetComponent<NavMeshAgent>();
        Destroy(this.gameObject, lastDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (closest == null || closest.tag != "Enemy")
        {
            closest = GetClosestEnemy();
        }
        else
        {
            attackNearest();
        }
            
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EatEnemy(collision.gameObject);
        }
    }


    private void EatEnemy(GameObject EnemyInContact)
    {
        //EnemyInContact = collision.gameObject;
        //EnemyInContact = collision.gameObject;
        EnemyInContact.tag = "EnemyAtBowl";
        EnemyInContact = GameObject.FindGameObjectWithTag("EnemyAtBowl");
        //_animator.SetTrigger("isEating");
        EnemyInContact.GetComponent<EnemyMovement>().setIsActive(false);
        EnemyInContact.GetComponent<NavMeshAgent>().enabled = false;
        EnemyInContact.GetComponent<BoxCollider>().enabled = false;
        EnemyInContact.GetComponent<Target>().enabled = false;
        transform.LookAt(EnemyInContact.transform);
        PlayerMovement.score++;
        if (OnEnemyEaten != null)
        {
            OnEnemyEaten();
        }
        //EnemyInContact.tag = "Untagged";

        Destroy(EnemyInContact.gameObject);

    }

    void attackNearest()
    {
        //Debug.Log(closest);
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, deerSpeed * Time.deltaTime);
        _agent.SetDestination(closest.transform.position); 
    }

    GameObject GetClosestEnemy()
    {
        closetDistance = 10000;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distance < closetDistance)
            {
                closest = enemies[i];
                closetDistance = distance;
            }
        }
        return closest;
        
    }
}
