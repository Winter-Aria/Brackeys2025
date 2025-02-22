using TMPro;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal.Internal;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float scoreMultiplier = 100f; 

    private float startTime;
    private int score;
    public ShakeData shakeData;
    public ParticleSystem particles;
    public CanvasGroup GameOverScreen;
    public TextMeshProUGUI gameOverText;
    [SerializeField] private float gameOverLerpSpeed;

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
        score = Mathf.FloorToInt(scoreMultiplier * Mathf.Pow(elapsedTime, 1.5f));
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score;
    }
    public void QuitToMain()
    {
        SceneManager.LoadScene("MainMenu");
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
}

  
