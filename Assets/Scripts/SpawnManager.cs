using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class SpawnManager : MonoBehaviour
{

    public List<GameObject> enemies;

    private GameObject enemy;
    // eventually will need to create an array or list to contain differemt prefabs of varying Enemy types
    private float zSpawnMax = 9.0f;
    private float zPawnMin = 5.0f;
    private float xSpawnRange = 20.0f;
    public int enemyCount { get; private set; }
    private int enemySpawned;
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

        // Creates an enemy according to the input of the method, which should always be the wave number. The enemies area spawned at the top of the game area to avoid spontaneous collision with where the player usually will be.
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            enemy = enemies[Random.Range(0, enemies.Count)];
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            float randomZ = Random.Range(zPawnMin, zSpawnMax);
            Vector3 spawnLocation = Vector3.zero;
            bool validPosition = false;
            int spawnAttempts = 0;
            int maxSpawnAttempts = wave * 20;

            while (!validPosition && spawnAttempts < maxSpawnAttempts)
            {
                spawnAttempts++;
                spawnLocation = new Vector3(randomX, 1, randomZ);
                validPosition = true;
                Collider[] colliders = Physics.OverlapSphere(spawnLocation, 1f);
                foreach (Collider col in colliders)
                {
                    // If this collider is tagged "Enemy"
                    if (col.tag == "Enemy")
                    {
                        // Then this position is not a valid spawn position
                        validPosition = false;
                    }
                }
            }
            if (validPosition)
            {
                Instantiate(enemy, spawnLocation, enemy.transform.rotation);
            }

        }


    }

    public void WaveSet()
    {// whenever the amount of enemies in a wave reaches 0, the wave number is increased, allowing more enemies to spawn.
     // Also adds score equal to 100 * the wave number and an energy to the player if their energy is lower than five, and ensures their energy cannot be above 5.
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
