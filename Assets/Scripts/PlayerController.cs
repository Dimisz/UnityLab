using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 200;
    private float zBound = 12;
    private bool onGround;
    private Rigidbody rbPlayer;

    private void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        MovePlayer();
        ConstrainMovement();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            onGround = true;
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Coollided with an enemy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Debug.Log("Got powerup");
            Destroy(other.gameObject);
        }
    }

    // moves the player (WASD & jumping) basing on the user input
    private void MovePlayer()
    {
        if (onGround)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            rbPlayer.AddForce(Vector3.right * speed * horizontalInput);
            rbPlayer.AddForce(Vector3.forward * speed * verticalInput);
        }

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rbPlayer.AddForce(Vector3.up * 200, ForceMode.Impulse);
            onGround = false;
        }
    }

    //constraiins player's movements so that the player doesn't fall out of the scene
    private void ConstrainMovement()
    {
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
        else if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }

 
}
