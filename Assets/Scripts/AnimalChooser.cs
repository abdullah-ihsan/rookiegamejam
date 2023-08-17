using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnimalChooser : MonoBehaviour
{
    [SerializeField] private Button _cowButton;
    [SerializeField] private float offset;
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
        Debug.Log("Cow Found: " + GameObject.FindGameObjectWithTag("Cow"));
        if (GameObject.FindGameObjectWithTag("Cow") == null)
        {
            spawnpoint = player.position + player.forward * offset;
            Instantiate(_cowPrefab, spawnpoint, Quaternion.identity);
        }
    }
}
