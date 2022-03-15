using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    public AudioClip shootingSound;
    public AudioClip deathSound;
    public GameObject enemySprite;
    private Quaternion extraBulletsRightOffset = new Quaternion(0, 45, 0, 30);
    private Quaternion extraBulletsLeftOffset = new Quaternion(0, -45, 0, 30);
    // Start is called before the first frame update
    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        StartCoroutine(EnemyAttackRoutine(shootingSound));
        StartCoroutine(ExtraProjectilesRoutine());
    }


    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ExtraProjectilesRoutine()
    {
        while (MainManager.gameActive && isAlive)
        {
            yield return new WaitForSeconds(1);
            if (isAlive)
            {
                enemyAudio.PlayOneShot(shootingSound, 0.5f);
                Instantiate(projectilePrefab, transform.position, extraBulletsRightOffset);
                Instantiate(projectilePrefab, transform.position, extraBulletsLeftOffset);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
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
