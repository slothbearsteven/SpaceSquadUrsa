using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{



    // Start is called before the first frame update
    void Start()
    {
        speed = 40.0f;

    }

    // Update is called once per frame
    void Update()
    {
        ProjectileMovement(speed);
    }
}
