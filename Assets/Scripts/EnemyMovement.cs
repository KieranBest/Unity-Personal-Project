using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player; 
    private float rotationSpeed = 720.0f;

    public float speed = 0.5f;

    public static bool IsDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDestroyed == false)
        {            
            Destination();
        }

        if (transform.position.x > 10 || transform.position.x < -10 || transform.position.z > 6 || transform.position.z < -6)
        {
            Destroy(gameObject);

        }
    }

    void Destination()
    {
        Vector3 Destination = (player.transform.position - transform.position).normalized;

        enemyRb.AddForce(Destination * speed);

        Quaternion rotation = Quaternion.LookRotation(Destination, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);


    }
}