using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;

    public void Start()
    {
        startButton.onClick.AddListener(LoadGame);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("WreckingBalls");
    }
}
