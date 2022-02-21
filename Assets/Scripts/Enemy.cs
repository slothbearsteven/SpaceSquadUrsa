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

    }

    // Update is called once per frame
    void Update()
    {//rotates the enemy towards the player
        EnemyAttack();
        Vector3 direction = targetPlayer.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);


    }

    void EnemyAttack()
    {

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }








}
