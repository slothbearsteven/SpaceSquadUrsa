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
    {//rotates the enemy towards the player


        EnemyTargeting();


    }

    void EnemyTargeting()
    {
        if (MainManager.gameActive)
        {
            Vector3 direction = targetPlayer.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        }
    }

    IEnumerator EnemyAttackRoutine()
    {
        while (MainManager.gameActive && isAlive)
        {
            yield return new WaitForSeconds(1);
            enemyAudio.PlayOneShot(shootingSound, 0.5f);
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Projectile"))
        {
            isAlive = false;
            Destroy(other.gameObject);
            StartCoroutine(DestructionCoroutine());
        }
        if (other.gameObject.CompareTag("Player"))
        {
            isAlive = false;
            StartCoroutine(DestructionCoroutine());
        }
    }

    IEnumerator DestructionCoroutine()
    {
        explosionParticle.Play();
        enemyAudio.PlayOneShot(deathSound, 1.0f);
        enemySprite.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


}
