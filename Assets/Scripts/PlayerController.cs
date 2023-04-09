using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private float speed = 4.0f;
    private float zBound = 4.5f;
    private float xBound = 8.5f;
    private float rotationSpeed = 720.0f;
    private int score = 0;

    public bool hasPowerUp = false;
    public GameObject powerUpIndicator;
    public ParticleSystem playerCollisionParticle;
    public ParticleSystem enemyCollisionParticle;

    [SerializeField] TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        powerUpIndicator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Boundaries();

        powerUpIndicator.transform.position = transform.position;

        scoreText.SetText("Score: " + score);
    }

    //Moves the player based on input
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }


        /*
        //Move the vehicle forward.
        playerRb.AddForce(transform.forward * verticalInput * speed); ;
        //Turn the vehicle
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizontalInput);
        */
    }

    //Creates Endless Boundaries
    void Boundaries()
    {
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") &! hasPowerUp)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            Debug.Log("GAME OVER");
            EnemyMovement.IsDestroyed = true;
            playerCollisionParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Destroy(collision.gameObject);
            enemyCollisionParticle.Play();
            score++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
            Debug.Log("Has Power Up.");
            powerUpIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(3);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
        Debug.Log(" ");
    }
}
