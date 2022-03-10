using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{


    //Inheritance example
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProjectileMovement(speed);
    }
}

