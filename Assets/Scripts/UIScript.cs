using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public bool scalingUp = true;

    public Text textScore;
    public Text currentScore;
    public Text highestScore;
    public Text boostText;

    public Button pauseButton;
    public Button playButton;
    public Button replayButton;

    public GameObject homeScreen;
    public GameObject tutorialScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    public LogicScript logic;

    private void Awake()
    {
        logic = GetComponent<LogicScript>();
    }
    void Start()
    {
        initializeUI();
    }
    public void initializeUI()
    {
        highestScore.text = logic.highScore.ToString();
        openMainMenu();
    }
    public void displayBoost()
    {
        boostText.gameObject.SetActive(true);
    }
    public void hideBoost()
    {
        boostText.gameObject.SetActive(false);
    }
    public void openGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }
    public void closeGameOverScreen()
    {
        gameOverScreen.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
    public void openTutorialcreen()
    {
        tutorialScreen.SetActive(true);
    }
    public void closeTutorialScreen()
    {
        tutorialScreen.SetActive(false);
    }
    public void openPauseScreen()
    {
        pauseScreen.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }
    public void closePauseScreen()
    {
        pauseScreen.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
    public void openMainMenu()
    {
        homeScreen.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        textScore.gameObject.SetActive(false);
    }
    public void closeMainMenu()
    {
        homeScreen.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        textScore.gameObject.SetActive(true);
    }
}