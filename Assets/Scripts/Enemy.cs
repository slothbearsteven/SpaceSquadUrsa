using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject targetPlayer;
    public GameObject projectilePrefab;
    public int waveNumber;
    private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");

        StartCoroutine(EnemyAttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {//rotates the enemy towards the player

        Vector3 direction = targetPlayer.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);



    }


    IEnumerator EnemyAttackRoutine()
    {
        while (targetPlayer)
        {
            yield return new WaitForSeconds(1);
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Projectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }


}
