using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrillBehaviour : MonoBehaviour
{
    Animator animator;
    BoxCollider2D collision;
    
    [SerializeField] GameObject player;
    [SerializeField] float initialTimer=10f;
    float drillTimer;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Slider slider;
    [SerializeField] GameObject levelExit;
    [SerializeField] GameObject pointerArrow;

    AudioSource audioSource;

    public bool drillSwitch = false;
    playerMovement playerScript;
    void Start()
    {
        animator=GetComponent<Animator>();
        collision = GetComponent<BoxCollider2D>();
        playerScript = player.GetComponent<playerMovement>();
        drillTimer=initialTimer;
        if (slider != null)
        {
            slider.maxValue = initialTimer;
            slider.value = initialTimer;
            slider.minValue = 0f;
        }
        
        audioSource = GetComponent<AudioSource>();
         
    }

    private void Update()
    {
        StartDrill();
        UpdateTimer();

    }

    void StartDrill()
    {
        if (collision.IsTouchingLayers(LayerMask.GetMask("Player"))&&playerScript.moveInput.y>0&&!drillSwitch)
        {
            audioSource.Play();
            animator.SetTrigger("drillStarted");
            drillSwitch = true;

        }
       
    }
    void UpdateTimer()
    {
        if (slider != null)
        {
            if (drillSwitch && slider.value > 0f)
            {

                drillTimer -= Time.deltaTime;
                int timerValue = Mathf.FloorToInt(drillTimer);

                slider.value = drillTimer;
            }
            else if (slider.value == 0f)
            {
                audioSource.Stop();
                LevelWin();
            }
        }
        
    }
    public void TimerReset()
    {
        drillTimer=initialTimer;
    }
    
    public void LevelWin()
    {
       
        drillSwitch = false;
        animator.SetTrigger("drillFinished");
        timerText.gameObject.SetActive(true);
        pointerArrow.gameObject.SetActive(true);
        levelExit.gameObject.SetActive(true);
        
       
    }
}
