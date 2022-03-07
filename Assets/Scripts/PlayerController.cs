using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private float speed = 10;
    public float horizontalInput { get; set; }
    public float verticalInput { get; set; }
    private float xbounds = 22.0f;
    private float zbounds = 11.0f;

    public static int energy { get; set; } = 5;
    public Text energyText;
    public GameObject projectilePrefab;

    private Vector3 offset = new Vector3(0, 0, 0.15f);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerShoot();
        energyText.text = $"Energy: {energy}";

    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy Projectile"))
        {
            PlayerEnergyDecrease();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerEnergyDecrease();
        }
    }
    private void PlayerMovement()
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

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position + offset, projectilePrefab.transform.rotation);
        }
    }

    void PlayerEnergyDecrease()
    {
        energy -= 1;
        if (energy <= 0)
        {
            energyText.text = $"Energy: Critical Failure";
            Destroy(gameObject);
        }
    }

}

