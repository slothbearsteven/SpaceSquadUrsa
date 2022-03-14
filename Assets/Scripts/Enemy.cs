using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public AudioClip shootingSound;
    public AudioClip deathSound;
    public GameObject targetPlayer;
    public GameObject projectilePrefab;
    public GameObject enemySprite;
    public ParticleSystem explosionParticle;
    private AudioSource enemyAudio;

    private bool isAlive = true;
    private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        enemyAudio = GetComponent<AudioSource>();
        StartCoroutine(EnemyAttackRoutine());
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

    IEnumerator EnemyAttackRoutine()
    {
        //When the enemy is alive and the game is active, the enemy will shoot a projectile every 1 second
        while (MainManager.gameActive && isAlive)
        {
            yield return new WaitForSeconds(1);
            if (isAlive)
            {
                enemyAudio.PlayOneShot(shootingSound, 0.5f);
                Instantiate(projectilePrefab, transform.position, transform.rotation);
            }
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
                StartCoroutine(DestructionCoroutine());
            }


        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (isAlive)
            {
                MainManager.score += 20;
                isAlive = false;
                StartCoroutine(DestructionCoroutine());
            }

        }
    }

    IEnumerator DestructionCoroutine()
    {
        //The explosion particles and sounds are played, and the sprite is set to inactive to allow the image of the ship being destroyed, while allowing enough time for said animation to play before the object is truly removed.
        explosionParticle.Play();
        enemyAudio.PlayOneShot(deathSound, 1.0f);
        enemySprite.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


}
