using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public GameObject[] powerUpPrefabs;
    private GameManager gameManager;



    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._gameManager;
        SpawnEnemyWave(waveCount);
        SpawnPowerUp();


    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            enemyCount = FindObjectsOfType<Enemy>().Length;

            if (enemyCount == 0)
            {
                waveCount++;
                SpawnEnemyWave(waveCount);
                SpawnPowerUp();
            }
        }
        
    }


    public void SpawnPowerUp()
    {

        int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[powerUpIndex], GenerateSpawnPosition(), powerUpPrefabs[powerUpIndex].transform.rotation);

    }

    public void SpawnEnemyWave(int enemiesToSpawn)
    {

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);
        }
    }


    //This is a method that return or random position vector spawn our enemy
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

}
