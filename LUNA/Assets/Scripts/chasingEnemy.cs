using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float detectionRange = 5f;

    private Transform playerTransform;
    private Rigidbody2D rb;
    bool isChasing;
    Animator anim;
    public bool isAlive;
    [SerializeField] AudioClip deathSound;
    AudioSource audioSource;

    private void Start()
    {
        playerTransform = FindObjectOfType<playerMovement>().transform; // Find the player's Transform
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();

    }

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // If the player is within the detection range, chase the player
        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
            ChasePlayer();
            anim.SetBool("isChasing", true);
        }
        else
        {
            isChasing = false;
            rb.velocity = new Vector2(0f, 0f);
            anim.SetBool("isChasing", false);
        }
    }

    private void ChasePlayer()
    {
        if (isChasing)
        {
            // Calculate the direction towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Move the enemy towards the player
            rb.velocity = direction * moveSpeed;


        }
        if (playerTransform.position.x < transform.position.x)
        {
            // Flip the sprite horizontally by negating the x scale
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Reset the sprite's scale to its original state
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player"&&isAlive)

        {
            
            isAlive = false;
            anim.SetTrigger("isDead");
            audioSource.clip = deathSound;
            audioSource.Play();
            FindObjectOfType<playerMovement>().Die();

            rb.Sleep();
            Destroy(gameObject, 1f);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet"&&isAlive)
        {
            anim.SetTrigger("isDead");
            isAlive = false;
            audioSource.clip = deathSound;
            audioSource.Play();
            rb.Sleep();
            Destroy(gameObject, 1f);
        }
    }
}

