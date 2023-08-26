using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform enemy;
    [SerializeField] private float shurikenSpeed = 500f;
    private Vector3 finalposition;

    private void Awake()
    {
        enemy = this.transform;
    }
    // Update is called once per frame
    void Update()
    {

        if(enemy != null) {
            finalposition = enemy.position;
        }
        gameObject.transform.position = Vector3.MoveTowards(transform.position, finalposition, shurikenSpeed * Time.deltaTime);
        if(transform.position == finalposition)
        {
            Destroy(this.gameObject);
        }
    }
}
