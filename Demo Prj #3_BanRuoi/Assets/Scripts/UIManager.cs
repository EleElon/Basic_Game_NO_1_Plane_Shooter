using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;

    public Text bulletsText;

    public Text healthText;

    public GameObject gameOverPanel;

    public GameObject gamePausePanel;

    public void SetScoreText(string txt)
    {
        if (scoreText)
            scoreText.text = txt;
    }

    public void SetBulletsText(string txt)
    {
        if (bulletsText)
            bulletsText.text = txt;
    }

    public void SetHealthText(string txt)
    {
        if (healthText)
            healthText.text = txt;
    }

    public void ShowGameOverPanel(bool state)
    {
        if (gameOverPanel)
            gameOverPanel.SetActive(state);
    }
    public void ShowGamePausePanel(bool state)
    {
        if (gamePausePanel)
            gamePausePanel.SetActive(state);
    }

    public bool IsGamePause()
    {
        return gamePausePanel.activeInHierarchy;
    }

    public void Pause()
    {
        gamePausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        gamePausePanel.SetActive(false);
        Time.timeScale = 1;
    }

}
