using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{


    //Inheritance and Polymorphism
    // Start is called before the first frame update
    void Start()
    {
        //moves the player projectile faster than enemy projectiles for balances sake
        speed = 40.0f;

    }

    // Update is called once per frame
    void Update()
    {
        ProjectileMovement(speed);
    }
}
