using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private float speed = 10;
    public float horizontalInput;
    public float verticalInput;
    public ParticleSystem explosionParticle;
    public AudioClip shootingSound;
    public AudioClip deathSound;
    public GameObject playerSprite;
    private AudioSource playerAudio;
    private float xbounds = 22.0f;
    private float zbounds = 11.0f;

    public static int energy { get; set; } = 5;
    public Text energyText;
    public GameObject projectilePrefab;

    private Vector3 offset = new Vector3(0, 0, 0.15f);
    // Start is called before the first frame update
    void Start()
    {
        energy = 5;
        playerAudio = GetComponent<AudioSource>();
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
        if (MainManager.gameActive)
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

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && MainManager.gameActive)
        {
            playerAudio.PlayOneShot(shootingSound, 0.5f);
            Instantiate(projectilePrefab, transform.position + offset, projectilePrefab.transform.rotation);
        }
    }

    void PlayerEnergyDecrease()
    {
        energy -= 1;
        if (energy <= 0)
        {

            energy = 0;
            energyText.text = "Energy: Critical Failure";
            StartCoroutine(DestructionCoroutine());

        }
    }

    IEnumerator DestructionCoroutine()
    {
        explosionParticle.Play();
        playerAudio.PlayOneShot(deathSound, 1.0f);
        playerSprite.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}

