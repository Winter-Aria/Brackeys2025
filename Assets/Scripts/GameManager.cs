using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign in Inspector
    public float scoreMultiplier = 100f; // Adjust to control score growth

    private float startTime;
    private int score;

    void Start()
    {
        //Initialise tasks
        startTime = Time.time;
        UpdateScoreDisplay();
    }

    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        float elapsedTime = Time.time - startTime;
        score = Mathf.FloorToInt(scoreMultiplier * Mathf.Pow(elapsedTime, 1.5f)); // Quadratic growth
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score;
    }
}

  
