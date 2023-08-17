using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalChooser : MonoBehaviour
{
    [SerializeField] private Button _cowButton;
    [SerializeField] private Vector3 offset;
    [SerializeField] GameObject _cowPrefab;
    private Vector3 spawnpoint;
    private Transform player;
    private void Awake()
    {
        _cowButton.onClick.AddListener(AddCow);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddCow()
    {
        spawnpoint = player.position + offset;
        Instantiate(_cowPrefab, spawnpoint, Quaternion.identity);
    }
}
