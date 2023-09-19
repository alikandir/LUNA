using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] float arrowRange = 6f;
    playerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playerMovement>();
        bulletSpeed = Mathf.Sign(player.transform.localScale.x) * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(bulletSpeed, 0f);
        ArrowRange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy" )
        {
            
            Destroy(gameObject);
        }
        
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

 
    void ArrowRange()
    {
        arrowRange -= Time.deltaTime;
        if (arrowRange < 0f)
        {
            Destroy(gameObject);
        }
    }
}
