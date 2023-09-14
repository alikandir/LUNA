using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    AudioSource audioSource;
    float gravityAtStart;
    float animatorSpeedDefault;
    float deathAnimationLength=3f;
    

    public bool isAlive=true;

    [SerializeField] Image reloadImage;
    [SerializeField] float gunCoolDown = 2f;
    float gunCoolDownCounter;
    [SerializeField] float runSpeed = 5;
    [SerializeField] float jumpSpeed=5;
    [SerializeField] float climbSpeed = 5;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;
    [SerializeField] AudioClip[] gunShotSounds;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip deathSound;


    LayerMask ground;
    LayerMask ladder;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        ground = LayerMask.GetMask("Ground");
        ladder = LayerMask.GetMask("Ladder");
        feetCollider = GetComponent<BoxCollider2D>();
        gravityAtStart = rb.gravityScale;
        animatorSpeedDefault = animator.speed;
        gunCoolDownCounter = gunCoolDown;
        audioSource = GetComponent<AudioSource>();
              
  
    }

    
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite(); 
        ClimbLadder();
        gunCoolDownCounter += Time.deltaTime;
        reloadImage.fillAmount = gunCoolDownCounter / gunCoolDown;
   
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (value.isPressed && isGrounded())
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
            audioSource.clip = jumpSound;
            audioSource.pitch = 0.8f;
            audioSource.Play();
        }
        
    }
    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        if (gunCoolDownCounter >= gunCoolDown)
        {
            animator.SetBool("isShooting",true);
            Instantiate(arrow, bow.position, transform.rotation);
            gunCoolDownCounter = 0f;
            int randomIndex = Random.Range(0, gunShotSounds.Length);
            audioSource.clip = gunShotSounds[randomIndex];
            audioSource.pitch = 1.25f;
            audioSource.Play();
            yield return new WaitForSeconds(0.4f);
            animator.SetBool("isShooting", false);

        }
        
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x*runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        
        if (playerHasHorizontalSpeed)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        /// if not touching the ladder get out of the method
        if (!bodyCollider.IsTouchingLayers(ladder)) 
        {
            rb.gravityScale = gravityAtStart;
            animator.SetBool("isClimbing", false);
            animator.speed = animatorSpeedDefault;
            return; 
        }
        /// This prevents autograbbing the ladder
        if(!(moveInput.y==0)) 
        {
            animator.SetBool("isClimbing", true);
        }
        if (animator.GetBool("isClimbing")==true)
        {
            Vector2 playerVelocity = new Vector2 (rb.velocity.x, moveInput.y * climbSpeed);
            rb.velocity = playerVelocity;
            rb.gravityScale = 0f;
            bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
            if(!isGrounded())
            {
                animator.SetBool("isClimbing", true);
            }
            else if(isGrounded())
            { animator.SetBool("isClimbing", false); }
            // to pause the animation while stopping climbing
            if (!playerHasVerticalSpeed&& animator.GetBool("isClimbing"))
            {
                animator.speed = 0;
            }
            else
            {
                animator.speed = animatorSpeedDefault;
            }

        }
    }
    bool isGrounded()
    {
        return feetCollider.IsTouchingLayers(ground);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Die();
        }
    }
    public void Die()
    {
       
        isAlive = false;
        audioSource.clip = deathSound;
        audioSource.volume = 1;
        audioSource.pitch = 1;
        audioSource.Play();
        animator.SetTrigger("Dying");
        rb.velocity = deathKick;
        StartCoroutine(FindObjectOfType<gameSession>().ProcessPlayerDeath(deathAnimationLength));        
        
    }

}
