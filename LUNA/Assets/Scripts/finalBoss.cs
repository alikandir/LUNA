using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalBoss : MonoBehaviour
{
    [SerializeField] int bossHealth;
    [SerializeField] Slider bossHealthUI;
    [SerializeField] GameObject levelExit;
    Animator anim;
    [SerializeField] float hurtAnimationLength;
    [SerializeField] GameObject wallTurrets;
    [SerializeField] AudioClip hurtSfx;
    [SerializeField] AudioClip deathSfx;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        bossHealthUI.maxValue = bossHealth;
        bossHealthUI.value = bossHealth;
        FindObjectOfType<DrillBehaviour>().drillSwitch = true;
        anim=GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(TakeHealth());
            Destroy(collision.gameObject);
        }
    }
    IEnumerator TakeHealth()
    {
        audioSource.clip = hurtSfx;
        audioSource.Play();
        bossHealth--;
        bossHealthUI.value = bossHealth;
        anim.SetBool("isHit", true);
        yield return new WaitForSeconds(hurtAnimationLength);
        anim.SetBool("isHit", false);


        if (bossHealth == 0)
        {
            audioSource.clip = deathSfx;
            audioSource.Play();
            wallTurrets.gameObject.SetActive(false);
            anim.SetTrigger("isDead");
            levelExit.gameObject.SetActive(true);
            FindObjectOfType<DrillBehaviour>().LevelWin();
            
        }
    }
}
