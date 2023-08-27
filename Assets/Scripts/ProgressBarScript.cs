using UnityEngine;
using UnityEngine.UI;
public class ProgressBarScript : MonoBehaviour
{
    Image progressBar;
    private float maxLevelScore;
    private float currentScore;
    [SerializeField] SoupScript forGettingMaxScore;
    void Start()
    {
        progressBar = GetComponent<Image>();
        maxLevelScore = forGettingMaxScore.getMaxScore();
    }

    void Update()
    {
        currentScore = PlayerMovement.score;
        if (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount = currentScore / maxLevelScore;
        }
    }
}
