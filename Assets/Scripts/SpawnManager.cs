using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemy;
    private float zSpawnMax = 9.0f;
    private float zPawnMin = 5.0f;
    private float xSpawnRange = 20.0f;
    public int enemyCount;
    public static int wave = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy(wave);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0) { wave++; SpawnEnemy(wave); }
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

    public int waveCount()
    {
        return wave;
    }
}
