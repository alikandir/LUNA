using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oxygenTankBehaviour : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    bool hasCollected=false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&!hasCollected)
        {
            hasCollected = true;
            audioSource.clip = collectSound;
            audioSource.Play();
            FindObjectOfType<oxygenTankManager>().OxygenTankCollect();
            spriteRenderer.enabled = false;
            
            Destroy(gameObject,1f);
            
        }
    }
    
}
