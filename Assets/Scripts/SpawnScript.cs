using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] spawnPoints;
    private float temp;
    private int no_enemies = 1;
    private float _multiplier = 5;
    //private GameObject[] enemies;
    // Start is called before the first frame update
    void Awake()
    {
        no_enemies = 1;
        PlayerMovement.score = 0;
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
//        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, new Quaternion(1,180,1,1));
       DontDestroyOnLoad(this.gameObject);
        temp = no_enemies;
        SpawnEnemy();
    }

    private void OnEnable()
    {
        EnemyMovement.OnEnemyKilled += SpawnEnemy;
        EnemyMovement.OnEnemyGotToBowl += SpawnEnemy;
        CowScript.OnEnemyEaten += SpawnEnemy;
        DeerScript.OnEnemyEaten += SpawnEnemy;
    }
    // Update is called once per frame
    void SpawnEnemy()
    {
        //Debug.Log("Spawn Called");
        if (PlayerMovement.score % 20 == 0 && PlayerMovement.score != 0) { _multiplier += 5; temp = 1; }
        //no_enemies = Mathf.CeilToInt(PlayerMovement.score/_multiplier);
        if((Mathf.CeilToInt(PlayerMovement.score / _multiplier)) > temp)
        {
            temp++;
            no_enemies++;
        }
        //if (PlayerMovement.score == 0) no_enemies = 1;
        //Debug.Log(GameObject.FindGameObjectWithTag("Enemy") == null);
        //Debug.Log("Score: " + PlayerMovement.score);
        //Debug.Log("No of enemies: " + no_enemies);
        //Debug.Log("Temp: " + temp);
        //Debug.Log("Multiplier: " + _multiplier);
        //Debug.Log("Bool: " + GameObject.FindGameObjectWithTag("Enemy") == null);
       
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            for (int i = 0; i < no_enemies; i++)
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, new Quaternion(1, 180, 1, 1));
            }
        }
    }
}
