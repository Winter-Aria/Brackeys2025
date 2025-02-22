using FirstGearGames.SmoothCameraShaker;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign in Inspector
    public float scoreMultiplier = 100f; // Adjust to control score growth

    private float startTime;
    private int score;
    public CanvasGroup GameOverScreen;
    public float gameOverLerpSpeed;
    public ShakeData shakeData;
    public ParticleSystem particles;
    public TextMeshProUGUI gameOverText;
    

	private void OnEnable()
	{
		EventManager.Instance.questSystemEvents.questUncompleted += QuestUncompleted;
	}

	private void OnDisable()
	{
		EventManager.Instance.questSystemEvents.questUncompleted -= QuestUncompleted;
	}

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

	private void QuestUncompleted()
    {
        Debug.Log("Game lost");
        if (shakeData != null)
        {
            CameraShakerHandler.Shake(shakeData);
        }
        particles.Play();
        StartCoroutine(FadeInGameOverScreen());
    }
    private IEnumerator FadeInGameOverScreen()
    {
        float elapsedTime = 0f;
        while (GameOverScreen.alpha < 1f)
        {
            elapsedTime += Time.deltaTime * gameOverLerpSpeed;
            GameOverScreen.alpha = Mathf.Lerp(0f, 1f, elapsedTime);
            GameOverScreen.interactable = true;
            gameOverText.text = scoreText.text;
            yield return null;
        }
    }
    public void QuitToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

  
