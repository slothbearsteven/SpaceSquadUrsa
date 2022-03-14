using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed { get; set; } = 20.0f;
    private float bounds = 22.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ProjectileMovement(float speed)
    { // causes the projectile to move forward, and be destroyed once it leaves the bounds of the game area
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.z <= -bounds - 10 || transform.position.z >= bounds - 10 || transform.position.x <= -bounds || transform.position.x >= bounds)
        {
            Destroy(gameObject);
        }
    }
}
