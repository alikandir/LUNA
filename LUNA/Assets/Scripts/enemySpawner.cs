using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject comgo;
    [SerializeField] bool isComgoSpawner;
    [SerializeField] float comgoSpawnRate;
    float comgoSpawnCounter;

    [SerializeField] GameObject chaser;
    [SerializeField] bool isChaserSpawner;
    [SerializeField] float chaserSpawnRate;
    float chaserSpawnCounter;

    [SerializeField] GameObject oxygenTank;
    [SerializeField] bool isOxygenSpawner;
    [SerializeField] float oxygenSpawnRate;
    float oxygenSpawnCounter;

    DrillBehaviour drill;
    Animator anim;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        comgoSpawnCounter = comgoSpawnRate;
        chaserSpawnCounter = chaserSpawnRate;
        oxygenSpawnCounter = oxygenSpawnRate;
        drill = FindObjectOfType<DrillBehaviour>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drill.drillSwitch == true)
        {
            Spawn();
        }
        
    }

    void Spawn()
    {
        if (isComgoSpawner)
        {
            comgoSpawnCounter -= Time.deltaTime;
            if (comgoSpawnCounter <= 0f)
            {

                audioSource.pitch = 0.80f;
                audioSource.Play();
                anim.SetBool("isSpawning", true);
                Instantiate(comgo, spawnPoint.position, transform.rotation);
                comgoSpawnCounter = comgoSpawnRate;
                Invoke("AnimatorReset", 2f);
               
                
            }
        }
        if (isChaserSpawner)
        {
            chaserSpawnCounter -= Time.deltaTime;
            if (chaserSpawnCounter <= 0f)
            {
                audioSource.pitch = 0.80f;
                audioSource.Play();
                anim.SetBool("isSpawning", true);
                Instantiate(chaser, spawnPoint.position, transform.rotation);
                chaserSpawnCounter = chaserSpawnRate;
                Invoke("AnimatorReset", 2f);
            }
        }

        if (isOxygenSpawner)
        {
            oxygenSpawnCounter -= Time.deltaTime;
            if (oxygenSpawnCounter <= 0f)
            {

                audioSource.pitch = 1.20f;
                audioSource.Play();
                Instantiate(oxygenTank, spawnPoint.position, transform.rotation);
                oxygenSpawnCounter = oxygenSpawnRate;
            }
        }

    }
    void AnimatorReset()
    {
        anim.SetBool("isSpawning", false);
    }
}
