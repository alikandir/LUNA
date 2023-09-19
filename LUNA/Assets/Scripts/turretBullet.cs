using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBullet : MonoBehaviour
{
    public float bulletTime;
    [SerializeField] float bulletSpeed;
    Rigidbody2D rb;
    wallTurret wallTurret;
    Vector2 bulletDirection;

    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        wallTurret=FindObjectOfType<wallTurret>();
        
    }
    private void Update()
    {
        bulletTime -= Time.deltaTime;
        if (bulletTime <= 0)
        {
            Destroy(gameObject);
        }
        Fly();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<playerMovement>().Die();
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

    }

    public void SetDirection(Vector2 direction)
    {
        bulletDirection = direction;
        if(direction.x < 0)
        {
            transform.localScale = new Vector2(-1,1);
        }
    }

    void Fly()
    {
        rb.velocity = bulletDirection * bulletSpeed;
    }

}
