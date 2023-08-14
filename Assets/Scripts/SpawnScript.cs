using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] spawnPoints;
    private int no_enemies;
    private GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
    }

    private void OnEnable()
    {
        EnemyMovement.OnEnemyKilled += SpawnEnemy;
    }
    // Update is called once per frame
    void SpawnEnemy()
    {
        Debug.Log(PlayerMovement.score + " Score/5: " + ((float)PlayerMovement.score / 5f));
        no_enemies = Mathf.CeilToInt(PlayerMovement.score/5f);
        enemies  = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log("No of enemies: " + (enemies.Length-1));
        Debug.Log("No of enemies to spawn:" + no_enemies);
        if (enemies.Length-1 == 0)
        {
            for (int i = 0; i < no_enemies; i++)
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
            }
        }
    }
}
