using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private GameObject player;
    private Rigidbody enemyRb;
    private GameManager gameManager;


    public float speed;
    private float forceMultiplier = 5.0f;
    public int points;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.isGameActive == true)
        {
            //look direction is returning a vector that represents the distance between the player and enemy. This tells the enemy where to move
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;

            enemyRb.AddForce(lookDirection * speed);

            if (transform.position.y < -3)
            {
                Destroy(gameObject);
                gameManager.UpdateScore(points);
            }

        }

    }


    //Applies an additional force multiplier if a Hard Enemy collides with the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("HardEnemy"))
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * forceMultiplier, ForceMode.Impulse);
        }

    }


}
