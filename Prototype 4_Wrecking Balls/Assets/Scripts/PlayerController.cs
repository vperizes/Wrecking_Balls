using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Gameobject var
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    public GameObject extraPowerupIndicator;
    public GameObject smashPowerupIndicator;
    public GameObject projectilePrefab; //used to assign rocket prefab
    private GameObject tmpRocket; //used to spawn rocket prefab
    public PowerUpType currentPowerUp = PowerUpType.None; //this is calling the power up type from the powerup script and setting it to none
    private GameManager gameManager;

    //Rigidbody var
    private Rigidbody playerRb;
    private Rigidbody enemyRb;


    //bools
    public bool hasPowerup = false;
    public bool hasExtraPowerup = false;
    public bool hasSmashPowerup = false;

    public float speed;
    private int yBound = -3;
    private float forwardInput;
    private float powerupStrength = 15.0f;
    private Vector3 powerupIndicatorOffset;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        gameManager = GameManager._gameManager;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        //Sets power up position to player position

        powerupIndicatorOffset = new Vector3(0, 0.5f, 0);
        powerupIndicator.transform.position = transform.position - powerupIndicatorOffset;
        extraPowerupIndicator.transform.position = transform.position - powerupIndicatorOffset;
        smashPowerupIndicator.transform.position = transform.position - powerupIndicatorOffset;

        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }

        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space))
        {
            Smash();
        }

        //Defines game over if player off platform

        if (transform.position.y < yBound)
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }
       
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            hasExtraPowerup = false;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType; //specificaly gets the powerup script and its enum variable powerUpType
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);


            if (StartCoroutine(PowerupCountdownRoutine()) != null)
            {
                StopCoroutine(PowerupCountdownRoutine());
            }

        }

        if (other.CompareTag("RocketPowerup"))
        {
            hasExtraPowerup = true;
            hasPowerup = false;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType; //specificaly gets the powerup script and its enum variable powerUpType
            extraPowerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);

            if (StartCoroutine(PowerupCountdownRoutine()) != null)
            {
                StopCoroutine(PowerupCountdownRoutine());
            }

        }

        if (other.CompareTag("SmashPowerup"))
        {
            hasSmashPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            smashPowerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }

    }

    //Sets count down timer independant of update method for the power up
    IEnumerator PowerupCountdownRoutine()
    {
        if (currentPowerUp == PowerUpType.Pushback)
        {
            yield return new WaitForSeconds(6);
            hasPowerup = false;
            currentPowerUp = PowerUpType.None;
            powerupIndicator.gameObject.SetActive(false);

        }
        else if (currentPowerUp == PowerUpType.Rockets)
        {
            yield return new WaitForSeconds(6);
            hasExtraPowerup = false;
            currentPowerUp = PowerUpType.None;
            extraPowerupIndicator.gameObject.SetActive(false);
        }
        else if (currentPowerUp == PowerUpType.Smash)
        {
            yield return new WaitForSeconds(6);
            hasSmashPowerup = false;
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        //applies a propelling force to enemy if player collides while powerup is active
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }

    }

    void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(projectilePrefab, transform.position + Vector3.forward, Quaternion.identity);
            tmpRocket.GetComponent<ProjectileBehaviour>().Fire(enemy.transform);
        }
    }

    void Smash()
    {
        playerRb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
    }
}



