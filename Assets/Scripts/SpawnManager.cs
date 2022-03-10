using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class SpawnManager : MonoBehaviour
{

    public GameObject enemy;

    private float zSpawnMax = 9.0f;
    private float zPawnMin = 5.0f;
    private float xSpawnRange = 20.0f;
    public int enemyCount { get; private set; }

    public static int wave { get; private set; } = 1;
    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        SpawnEnemy(wave);
    }

    // Update is called once per frame
    void Update()
    {
        WaveSet();

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
            MainManager.score += (wave * 100);
            wave++; SpawnEnemy(wave);
            if (PlayerController.energy < 5) { PlayerController.energy++; }
            else if (PlayerController.energy >= 5) { PlayerController.energy = 5; }
        }
    }


}
