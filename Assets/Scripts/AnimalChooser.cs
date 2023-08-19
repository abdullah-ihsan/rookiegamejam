using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnimalChooser : MonoBehaviour
{
    [SerializeField] private Button _cowButton;
    [SerializeField] private Button _deerButton;
    [SerializeField] private float offset;
    [SerializeField] GameObject _cowPrefab;
    [SerializeField] GameObject _deerPrefab;

    [SerializeField] private float cowCooldown = 40f;
    [SerializeField] private float deerCooldown = 20f;



    private GameObject _cow;
    private GameObject _deer;

    private bool cowExists = false;
    private bool deerExists = false;

    private Vector3 spawnpoint;
    private Transform player;

    private Color cowColor;
    private Color deerColor;
    private void Awake()
    {
        _cowButton.onClick.AddListener(AddCow);
        _deerButton.onClick.AddListener(AddDeer);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cowColor = _cowButton.image.color;
        deerColor = _deerButton.image.color;
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (cowExists)
        {
            cowColor.a = 0.2f;
            _cowButton.image.color = cowColor;
        }
        else
        {
            cowColor.a = 0.8f;
            _cowButton.image.color = cowColor;
        }
        if (deerExists)
        {
            deerColor.a = 0.2f;
            _deerButton.image.color = deerColor;
        }
        else
        {
            deerColor.a = 0.8f;
            _deerButton.image.color = deerColor;
        }


    }

    void AddCow()
    {
        if (!cowExists)
        {
            spawnpoint = player.position + player.forward * offset;
            _cow = Instantiate(_cowPrefab, spawnpoint, Quaternion.identity);
            StartCoroutine(CowCooldown());
        }
    }

    void AddDeer()
    {
        //Debug.Log("Cow Found: " + GameObject.FindGameObjectWithTag("Cow"));
        if (!deerExists)
        {
            spawnpoint = player.position + player.forward * offset;
            _deer = Instantiate(_deerPrefab, spawnpoint, Quaternion.identity);
            StartCoroutine(DeerCooldown());
        }
    }

    IEnumerator CowCooldown()
    {
        cowExists = true;
        yield return new WaitForSeconds(cowCooldown);
        Destroy(_cow);
        cowExists = false;
    }

    IEnumerator DeerCooldown()
    {
        deerExists = true;
        yield return new WaitForSeconds(deerCooldown);
        Destroy(_deer);
        deerExists = false;
    }
}
