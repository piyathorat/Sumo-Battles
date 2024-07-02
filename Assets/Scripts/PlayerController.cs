using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public float speed = 5.0f;
    private GameObject focalPoint;
    private Rigidbody playerRb;
    private float powerupStrenth = 30.0f;
    public bool hasPowerup = false;// this is false because is its not collided then there will be no power up 
    public GameObject PowerupIndicator;
    void Start()
    {
        focalPoint = GameObject.Find("FocalPoint");
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward* forwardInput * speed);
        PowerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0); // set indicator to follow the player vector for indictor down
    }

    private void OnTriggerEnter(Collider other)// its for triggers
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;//if the powerup is collided  with object then destroy the powerup
            PowerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCoundownRoutine());
           
        }
    }
    IEnumerator PowerupCoundownRoutine()
    {
        yield return new WaitForSeconds(7);
        PowerupIndicator.gameObject.SetActive(false);
        hasPowerup = false;
    }
    private void OnCollisionEnter(Collision collision)// its is because if he has power then we can kick the enemy out so it uses physical method
    {
        if(collision.gameObject.CompareTag("Enemy")&& hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;// enemy current position -player position

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrenth , ForceMode.Impulse);// throw the enemy to opposite direction
            Debug.Log("Collided with " + collision.gameObject.name + " with power setup to" + hasPowerup);

            
        }
    }

}
