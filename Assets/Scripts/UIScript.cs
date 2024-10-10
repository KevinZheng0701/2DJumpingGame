using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public bool scalingUp = true;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI highestScore;
    public TextMeshProUGUI boostText;
    public Button playButton;
    public Button replayButton;
    [SerializeField] Button pauseButton;
    [SerializeField] GameObject homeScreen;
    [SerializeField] GameObject tutorialScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] LogicScript logic;

    void Start()
    {
        InitializeUI();
    }

    public void InitializeUI()
    {
        highestScore.text = logic.highScore.ToString();
        OpenMainMenu();
    }

    public void DisplayBoost(string text)
    {
        boostText.text = text;
        boostText.gameObject.SetActive(true);
    }

    public void HideBoost()
    {
        boostText.gameObject.SetActive(false);
    }

    public void OpenGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void CloseGameOverScreen()
    {
        gameOverScreen.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    public void OpenTutorialcreen()
    {
        tutorialScreen.SetActive(true);
    }

    public void CloseTutorialScreen()
    {
        tutorialScreen.SetActive(false);
    }

    public void OpenPauseScreen()
    {
        pauseScreen.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void ClosePauseScreen()
    {
        pauseScreen.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    public void OpenMainMenu()
    {
        homeScreen.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        textScore.gameObject.SetActive(false);
    }

    public void CloseMainMenu()
    {
        homeScreen.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        textScore.gameObject.SetActive(true);
    }
}