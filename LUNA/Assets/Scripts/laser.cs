using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
    
{
    public float laserTime;
    

    private void Update()
    {
        laserTime -= Time.deltaTime;
        if(laserTime<=0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<playerMovement>().Die();
        }
        
    }
}
