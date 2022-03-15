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
    private int shieldDuration = 5;
    public GameObject shieldPowerup;
    private bool shieldActive;
    private int firepowerDuration = 5;
    private bool firepowerActive;

    private Vector3 offsetRight = new Vector3(1, 0, 0.15f);
    private Vector3 offsetLeft = new Vector3(-1, 0, 0.15f);

    private Vector3 firepowerOffsetRight = new Vector3(0.3f, 0, 0.15f);
    private Vector3 firepowerOffsetLeft = new Vector3(-0.3f, 0, 0.15f);
    private float xbounds = 20.0f;
    private float zbounds = 11.0f;

    //encapsulation
    public static int energy { get; set; } = 5;
    public Text energyText;
    public GameObject projectilePrefab;


    // Start is called before the first frame update
    void Start()
    {
        energy = 5;
        shieldActive = false;
        firepowerActive = false;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //abstraction
        PlayerMovement();
        PlayerShoot();
        energyText.text = $"Energy: {energy}";
        FirepowerUp();
        ShieldUp();
        OverdrivePowerup();
    }





    private void OnTriggerEnter(Collider other)
    {//If the player collides with an Enemy or the enemy's projectile, causes the other projectile to be destroyed and runs the player energy decrease method
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
    { //abstraction
      //while the game is Active, uses the input from the Horizontal and verticcal Axis to allow the user to move in any direction on the 2d plane while remaining in bounds
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
    {// When the user presses the space bar, a projectile is spawned in front of the player character
        if (Input.GetKeyDown(KeyCode.Space) && MainManager.gameActive)
        {
            playerAudio.PlayOneShot(shootingSound, 0.5f);
            Instantiate(projectilePrefab, transform.position + offsetRight, projectilePrefab.transform.rotation);
            Instantiate(projectilePrefab, transform.position + offsetLeft, projectilePrefab.transform.rotation);
            if (firepowerActive)
            {
                playerAudio.PlayOneShot(shootingSound, 0.5f);
                Instantiate(projectilePrefab, transform.position + firepowerOffsetRight, projectilePrefab.transform.rotation);
                Instantiate(projectilePrefab, transform.position + firepowerOffsetLeft, projectilePrefab.transform.rotation);
            }
        }
    }

    //Create life gamble mechanics - note that each should NOT allow the user to trigger their death

    // shield - spend one energy to become shielded for several seconds
    IEnumerator ShieldRoutine(int duration)
    {
        Debug.Log("Shield Start");
        shieldPowerup.SetActive(true);
        shieldActive = true;
        yield return new WaitForSeconds(duration);
        shieldPowerup.SetActive(false);
        shieldActive = false;
        Debug.Log("Shield End");
    }
    void ShieldUp()
    {
        if (Input.GetKeyDown(KeyCode.X) && energy > 2 && !shieldActive)
        {

            energy -= 2;
            StartCoroutine(ShieldRoutine(shieldDuration));
        }
    }
    //fire power - increases the amount of projectiles fired for a limited time for the cost of 2 energy
    IEnumerator FirepowerRoutine(int duration)
    {
        Debug.Log("Firepower Start");
        firepowerActive = true;
        yield return new WaitForSeconds(duration);
        firepowerActive = false;
        Debug.Log("Firepower end");
    }
    void FirepowerUp()
    {
        if (Input.GetKeyDown(KeyCode.Z) && energy > 1 && !firepowerActive)
        {
            energy--;
            StartCoroutine(FirepowerRoutine(firepowerDuration));
        }
    }

    //overdrive - spend all but one energy to activate both powerups effects for double the duration
    void OverdrivePowerup()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && energy > 3 || Input.GetKeyDown(KeyCode.RightShift) && energy > 3)
        {
            energy = 1;
            StartCoroutine(ShieldRoutine(shieldDuration * 2));
            StartCoroutine(FirepowerRoutine(firepowerDuration * 2));
        }
    }




    void PlayerEnergyDecrease()
    {// Removes an energy from the player, then if the player has zero or less energy, ensures the energy amount is set to zero for the ui, and then begins the destruction coroutine
        if (!shieldActive)
        {

            energy -= 1;
            if (energy <= 0)
            {

                energy = 0;
                StartCoroutine(DestructionCoroutine());

            }
        }
    }

    IEnumerator DestructionCoroutine()
    {
        //plays the explosion particles and sound while setting the player sprite to inactive, allowing the animation to play before the player object is destroyed in the scene
        explosionParticle.Play();
        playerAudio.PlayOneShot(deathSound, 1.0f);
        playerSprite.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}

