using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
  
    Rigidbody2D rb;
    BoxCollider2D turningCollider;
   

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        turningCollider= GetComponent<BoxCollider2D>();
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
}
