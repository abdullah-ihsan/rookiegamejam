using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] spawnPoints;
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
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoints[Random.Range(0,spawnPoints.Length)].transform.position, Quaternion.identity);
    }
}
