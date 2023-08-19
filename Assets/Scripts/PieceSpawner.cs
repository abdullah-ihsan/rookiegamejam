using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] piecesPrefab;
    [SerializeField] private Transform _player;

    private void OnEnable()
    {
        EnemyMovement.OnEnemyKilled += StartMakingPieces;
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartMakingPieces()
    {
        StartCoroutine(makePieces());
    }

    IEnumerator makePieces()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(piecesPrefab[Random.Range(0, piecesPrefab.Length)], _player.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
