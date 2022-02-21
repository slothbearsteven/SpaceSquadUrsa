using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform targetPlayer;
    public GameObject projectilePrefab;
    private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyAttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {//rotates the enemy towards the player
        Vector3 direction = targetPlayer.position - transform.position;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Projectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }


}
