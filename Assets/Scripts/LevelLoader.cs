using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public static bool transed = false;
    void Update()
    {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && !transed)
        {
            transed = true;
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }
}
