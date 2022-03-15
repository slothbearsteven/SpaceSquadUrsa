using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{



    public GameObject projectilePrefab;

    public ParticleSystem explosionParticle;
    public AudioSource enemyAudio;

    public bool isAlive = true;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }



    public IEnumerator EnemyAttackRoutine(AudioClip shootingSound)
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




    public IEnumerator DestructionCoroutine(GameObject sprite, AudioClip deathSound)
    {
        //The explosion particles and sounds are played, and the sprite is set to inactive to allow the image of the ship being destroyed, while allowing enough time for said animation to play before the object is truly removed.
        explosionParticle.Play();
        enemyAudio.PlayOneShot(deathSound, 1.0f);
        sprite.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


}
