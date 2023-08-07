using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemyAIPatrol : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Player;

    

    [SerializeField] private Transform destinationPoint;
    
    

    void Awake()
    {
        
    }
    void Start()
    {
        Debug.Log("Destination: " + destinationPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    
    
}
