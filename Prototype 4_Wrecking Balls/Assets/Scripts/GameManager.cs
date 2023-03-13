using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _gameManager; //singleton call set up
    public bool isGameActive;
    // Start is called before the first frame update
    
    
    public void Awake()
    {
        _gameManager = this;
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        isGameActive = false;
    }
}
