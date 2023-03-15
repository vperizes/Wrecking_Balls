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

    private int score;
    public bool isGameActive;
    // Start is called before the first frame update
    
    
    public void Awake()
    {
        _gameManager = this;
        isGameActive = true;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
        SceneManager.LoadScene("Prototype 4");
    }

}
