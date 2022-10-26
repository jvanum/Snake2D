using UnityEngine;
using TMPro;

public class ScoreController2 : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        RefreshUI();
    }


    public void IncreaseScore(int increment)
    {
        score += increment;
        RefreshUI();
    }

    public void DecreaseScore(int decrement)
    {
        if (score > 0)
        {
            score -= decrement;
        }
        else
        { 
            score = 0;
        }
      
        RefreshUI();
    }
    private void RefreshUI()
    {
        scoreText.text = "Score : " + score;
    }
}
