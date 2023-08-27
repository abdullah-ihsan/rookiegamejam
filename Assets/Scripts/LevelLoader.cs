using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private static bool transed = false;

    void Update()
    {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && !transed)
        {
            transed = true;
            LoadLevel();
        }
    }

    private void LoadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log("We here");
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }
}
