using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Allows us to interact with text
using UnityEngine.UI; //Allows us to interact with UI/buttons
using UnityEngine.SceneManagement; //allows us to call scenes

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public GameObject titleScreen;
    public GameObject player;
    public Button restartButton;
    private SpawnManager spawnManager;

    public bool isGameActive;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    public void Update()
    {
        
       
    }

    public void StartGame()
    {
        isGameActive = true;
        player.gameObject.SetActive(true);
        spawnManager.SpawnPowerUp();
        spawnManager.SpawnEnemyWave(spawnManager.waveCount);
        

        score = 0;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Prototype 4");
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

}
