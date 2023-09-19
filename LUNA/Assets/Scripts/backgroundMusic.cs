using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip bgMusic;
    
    AudioSource audioSource;
    DrillBehaviour drill;
    bool isPlayingAnything = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        drill = FindObjectOfType<DrillBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drill.drillSwitch == true && !isPlayingAnything)
        {
            PlaySong();
        }
        
    }

    void PlaySong()
    {
        isPlayingAnything = true;
        audioSource.clip = bgMusic;
        audioSource.loop = true;
        audioSource.Play();
        
    }
}
