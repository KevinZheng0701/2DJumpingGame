using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public bool gameOverTriggered = false;
    public bool isPaused = true;
    public bool pointBoostedActivated = false;
    public int playerScore = 0;
    public int highScore;
    private float pointsDuration = 10;
    private float remainingTime = 0;
    private float activationTime;
    [SerializeField] GameObject asteroid;
    [SerializeField] PlayerScript playerScript;
    [SerializeField] SpawnerScript spawnerScript;
    [SerializeField] MusicScript musicScript;
    [SerializeField] UIScript uiScript;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        musicScript.PlaySoundTrack();
    }

    void Update()
    {
        if (pointBoostedActivated && !isPaused)
        {
            remainingTime = CalculateRemainingTime(activationTime, pointsDuration);
            if (remainingTime <= 0)
            {
                DeactivatePointsMultipler();
            }
        }
    }

    public void AddScore(int value)
    {
        playerScore += value;
        uiScript.textScore.text = playerScore.ToString();
    }

    public void ResetScore()
    {
        playerScore = 0;
        uiScript.textScore.text = playerScore.ToString();
    }

    public void UpdateCurrentScore()
    {
        uiScript.currentScore.text = uiScript.textScore.text;
    }

    public void UpdateHighScore()
    {
        if (playerScore > highScore)
        {
            highScore = playerScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            uiScript.highestScore.text = highScore.ToString();
        }
    }

    public void StartGame()
    {
        isPaused = false;
        uiScript.CloseMainMenu();
        playerScript.UnPausePlayer();
        spawnerScript.spawner = true;
    }

    public void RestartGame()
    {
        gameOverTriggered = false;
        isPaused = false;
        spawnerScript.spawner = true;
        playerScript.ResetPlayer();
        uiScript.CloseGameOverScreen();
        ResetScore();
    }

    public void GameOver()
    {
        gameOverTriggered = true;
        isPaused = true;
        DeactivatePointsMultipler();
        playerScript.PausePlayer();
        musicScript.PlayGameOverSound();
        uiScript.OpenGameOverScreen();
        UpdateCurrentScore();
        UpdateHighScore();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TogglePause()
    {
        spawnerScript.spawner = !spawnerScript.spawner;
        if (playerScript.playerStatus)
        {
            playerScript.PausePlayer();
            isPaused = true;
            uiScript.OpenPauseScreen();
        }
        else
        {
            playerScript.UnPausePlayer();
            isPaused = false;
            uiScript.ClosePauseScreen();
        }
        UpdateCurrentScore();
        UpdateHighScore();
    }

    public void ActivatePointsMultiplier()
    {
        activationTime = Time.time;
        pointBoostedActivated = true;
        uiScript.DisplayBoost("Double Points");
    }

    public void DeactivatePointsMultipler()
    {
        pointBoostedActivated = false;
        uiScript.HideBoost();
    }

    public float CalculateRemainingTime(float activationTime, float duration)
    {
        return activationTime + duration - Time.time;
    }
}
