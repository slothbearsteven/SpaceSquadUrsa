using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class SpawnManager : MonoBehaviour
{

    public GameObject enemy;
    public static bool gameActive;
    public GameObject GameOverText;
    private float zSpawnMax = 9.0f;
    private float zPawnMin = 5.0f;
    private float xSpawnRange = 20.0f;
    public int enemyCount { get; private set; }
    public static int wave { get; private set; } = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
        SpawnEnemy(wave);
    }

    // Update is called once per frame
    void Update()
    {
        WaveSet();
        GameOver();
    }

    void SpawnEnemy(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            float randomZ = Random.Range(zPawnMin, zSpawnMax);
            Vector3 spawnLocation = new Vector3(randomX, 1, randomZ);
            Instantiate(enemy, spawnLocation, enemy.transform.rotation);
        }


    }

    public void WaveSet()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            wave++; SpawnEnemy(wave);
            if (PlayerController.energy < 5) { PlayerController.energy++; }
            else if (PlayerController.energy >= 5) { PlayerController.energy = 5; }
        }
    }

    public void GameOver()
    {
        if (PlayerController.energy <= 0)
        {
            gameActive = false;
            GameOverText.SetActive(true);
        }



    }
}
