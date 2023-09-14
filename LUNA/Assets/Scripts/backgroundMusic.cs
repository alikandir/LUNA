using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip introMusic;
    [SerializeField] AudioClip loopingMusic;
    AudioSource audioSource;
    DrillBehaviour drill;
    bool isPlayingIntro = true;
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
            StartCoroutine(PlaySong());
        }
        
    }

    IEnumerator PlaySong()
    {
        if (isPlayingIntro&&!isPlayingAnything)
        {
            isPlayingAnything = true;
            audioSource.clip = introMusic;
            audioSource.Play();
            yield return new WaitForSeconds(introMusic.length);
            isPlayingIntro = false;
            isPlayingAnything = false;
        }
        if (!isPlayingIntro && !isPlayingAnything)
        {
            isPlayingAnything = true;
            audioSource.clip = loopingMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
