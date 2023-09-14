using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] AudioClip deathSound;
    AudioSource audioSource;
  
    Rigidbody2D rb;
    BoxCollider2D turningCollider;
    Animator anim;
    bool isAlive=true;
   

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        turningCollider= GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
        rb.velocity = new Vector2(moveSpeed, 0f);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed),1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && FindObjectOfType<playerMovement>().isAlive&&isAlive) 

        {

            FindObjectOfType<playerMovement>().Die();
            rb.Sleep();

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet"&&isAlive)
        {
            isAlive = false;
            audioSource.clip = deathSound;
            audioSource.Play();
            anim.SetTrigger("isDead");
            Destroy(gameObject, 0.5f);
            
        }
    }

}
