using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] piecesPrefab;
    private Vector3 point;

    private void OnEnable()
    {
        //EnemyMovement.OnEnemyKilled += StartMakingPieces(;
    }

    private void Awake()
    {
        point = GameObject.FindGameObjectWithTag("Player").transform.position;
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

    public void StartMakingPieces(Vector3 point)
    {
        this.point = point;
        StartCoroutine(makePieces());
    }

    IEnumerator makePieces()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(piecesPrefab[Random.Range(0, piecesPrefab.Length)], point, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
