using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    public static GameManager _gameManager; //singleton call set up

    [SerializeField] TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public GameObject pauseMenu;

    private int score;
    public bool isGameActive;
    private bool isGamePaused;
    
    
    public void Awake()
    {
        _gameManager = this;
        isGameActive = true;
        score = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
    }

    public void AddScore(int num)
    {
        score += num;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    private void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

}
