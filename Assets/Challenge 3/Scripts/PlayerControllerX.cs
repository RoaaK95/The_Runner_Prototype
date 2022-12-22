using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public bool isLowEnough;
    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;
    private float topBound = 13.0f;
    private float lowerBound = 3.4f;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {    
        if (transform.position.y > topBound)
        {
            isLowEnough = false;
            transform.position = new Vector3(transform.position.x, topBound, transform.position.z);
            playerRb.AddForce(Vector3.down * 1, ForceMode.Impulse);
        }
        if (transform.position.y< topBound)
        { 
            isLowEnough = true; 
        }

        if (transform.position.y < lowerBound)
        {
            transform.position = new Vector3(transform.position.x, lowerBound, transform.position.z);
            playerRb.AddForce(Vector3.up * 3,ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && isLowEnough)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

    


}
