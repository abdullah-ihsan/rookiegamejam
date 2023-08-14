using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _button;
    private void Awake()
    {
        _button.onClick.AddListener(ReloadGame);
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
