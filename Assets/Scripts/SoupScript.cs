using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoupScript : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private int scoreToReach = 50;
   
    private float scaleIncrement;
    private int lastScore = 0;
    private Vector3 _scale;
    // Start is called before the first frame update
    void Start()
    {
        scaleIncrement = 1f / (float)scoreToReach;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.score > lastScore)
        {
            _scale = transform.localScale;
            _scale += new Vector3(scaleIncrement, scaleIncrement, scaleIncrement);
            if (_scale.x < 1 && _scale.y < 1 && _scale.z < 1)
            {
                transform.localScale = _scale;
            }
            lastScore = PlayerMovement.score;
        }
        if(lastScore == scoreToReach)
        {
            Debug.Log("Why are we here?");
            LoadLevel();
        }
    }

    private void LoadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }
}
