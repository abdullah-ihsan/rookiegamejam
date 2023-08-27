using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text TotalScoreDisplay;
    [SerializeField] private Button _button;

    private void Start()
    {
        TotalScoreDisplay.text = "Total Score: " + PlayerMovement.TotalScore;
    }

    private void Awake()
    {
        _button.onClick.AddListener(ReloadGame);
    }
    
    private void ReloadGame()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
