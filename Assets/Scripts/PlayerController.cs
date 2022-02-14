using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10;
    public float horizontalInput;
    public float verticalInput;
    private float xbounds = 22.0f;
    private float zbounds = 11.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
        if (transform.position.x <= -xbounds)
        {
            transform.position = new Vector3(-xbounds, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= xbounds)
        {
            transform.position = new Vector3(xbounds, transform.position.y, transform.position.z);
        }
        if (transform.position.z <= -zbounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zbounds);
        }
        if (transform.position.z >= zbounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zbounds);
        }

    }
}
