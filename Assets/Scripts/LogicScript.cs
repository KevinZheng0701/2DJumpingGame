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

    public GameObject asteroid;

    public PlayerScript playerScript;
    public SpawnerScript spawnerScript;
    public MusicScript musicScript;
    public UIScript uiScript;

    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        spawnerScript = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerScript>();
        musicScript = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>();
        uiScript = GetComponent<UIScript>();
    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        musicScript.playSoundTrack();
    }
    void Update()
    {
        if (pointBoostedActivated && !isPaused)
        {
            remainingTime = calculateRemainingTime(activationTime, pointsDuration);
            if (remainingTime <= 0)
            {
                deactivatePointsMultipler();
            }
        }
    }
    public void addScore(int value)
    {
        playerScore += value;
        uiScript.textScore.text = playerScore.ToString();
    }
    private void resetScore()
    {
        playerScore = 0;
        uiScript.textScore.text = playerScore.ToString();
    }
    private void updateCurrentScore()
    {
        uiScript.currentScore.text = uiScript.textScore.text;
    }
    private void updateHighScore()
    {
        if (playerScore > highScore)
        {
            highScore = playerScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            uiScript.highestScore.text = highScore.ToString();
        }
    }
    private void startGame()
    {
        isPaused = false;
        uiScript.closeMainMenu();
        playerScript.unPausePlayer();
        spawnerScript.spawner = true;
    }
    private void restartGame()
    {
        gameOverTriggered = false;
        isPaused = false;
        spawnerScript.spawner = true;
        playerScript.resetPlayer();
        uiScript.closeGameOverScreen();
        resetScore();
    }
    public void gameOver()
    {   
        gameOverTriggered = true;
        isPaused = true;
        deactivatePointsMultipler();
        playerScript.pausePlayer();
        musicScript.playGameOverSound();
        uiScript.openGameOverScreen();
        updateCurrentScore();
        updateHighScore();
    }
    private void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void togglePause()
    {
        spawnerScript.spawner = !spawnerScript.spawner;
        if (playerScript.playerStatus)
        {
            playerScript.pausePlayer();
            isPaused = true;
            uiScript.openPauseScreen();
            }
        else
        {
            playerScript.unPausePlayer();
            isPaused = false;
            uiScript.closePauseScreen();
        }
        updateCurrentScore();
        updateHighScore();
    }
    public void activatePointsMultiplier()
    {
        activationTime = Time.time;
        pointBoostedActivated = true;
        uiScript.boostText.text = "Double Points";
        uiScript.displayBoost();
    }
    private void deactivatePointsMultipler()
    {
        pointBoostedActivated = false;
        uiScript.hideBoost();
    }
    public float calculateRemainingTime(float activationTime, float duration)
    {
        return activationTime + duration - Time.time;
    }
}
