using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectorDrone : Enemy
{
    public AudioClip shootingSound;
    public AudioClip deathSound;
    public GameObject targetPlayer;
    public GameObject enemySprite;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        enemyAudio = GetComponent<AudioSource>();
        StartCoroutine(EnemyAttackRoutine(shootingSound));
    }

    // Update is called once per frame
    void Update()
    {
        EnemyTargeting();

    }
    void EnemyTargeting()
    { //When the game is active, the enemy will rotate towards the players direction at all times
        if (MainManager.gameActive)
        {
            Vector3 direction = targetPlayer.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if the enemy is hit by the players projectile, or runs into the player, sets the alive status to false to avoid the user from getting extra points from hitting an invisible object in a short window of time
        if (other.gameObject.CompareTag("Player Projectile"))
        {
            if (isAlive)
            {
                MainManager.score += 20;
                isAlive = false;
                Destroy(other.gameObject);
                StartCoroutine(DestructionCoroutine(enemySprite, deathSound));
            }


        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (isAlive)
            {
                MainManager.score += 20;
                isAlive = false;
                StartCoroutine(DestructionCoroutine(enemySprite, deathSound));
            }

        }
    }
}
