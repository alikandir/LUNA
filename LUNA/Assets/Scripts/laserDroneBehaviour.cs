using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserDroneBehaviour : MonoBehaviour
{
    [SerializeField] float shootingTime;
    float shootingTimeCounter;
    [SerializeField] GameObject laser;
    [SerializeField] Transform shootingPoint;
    Animator anim;
    float laserTime;

    // Start is called before the first frame update
    void Start()
    {
        shootingTimeCounter = 0f;
        anim = GetComponent<Animator>();
        laserTime = laser.GetComponent<laser>().laserTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimeCounter-= Time.deltaTime;
        Shoot();
        
        
    }

    void Shoot()
    {

        if (shootingTimeCounter <= 0f)
        {
            Instantiate(laser,shootingPoint.position,transform.rotation);
            shootingTimeCounter = shootingTime;
            anim.SetBool("isShooting", true);
            StartCoroutine(Waiting(laserTime));
        }   
    }

    public IEnumerator Waiting(float laserLength)
    {
        yield return new WaitForSeconds(laserLength);
        anim.SetBool("isShooting", false);
    }
    
}
