using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickup : MonoBehaviour
{
    [SerializeField] AudioClip sfx;
    [SerializeField] int coinScore=1;
    bool wasCollected = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !wasCollected)
        {
            /// to play the sound at the camera position, PlayClipAtPoint makes the sound not destroy.
            FindObjectOfType<gameSession>().IncreaseScore(coinScore);
            AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position);
            wasCollected= true;
            Destroy(gameObject);
        }
       
    }
}
