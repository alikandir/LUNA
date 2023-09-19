using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTurret : MonoBehaviour
{
    [SerializeField] float shootingTime;
    float shootingTimeCounter;
    [SerializeField] GameObject turretBullet;
    [SerializeField] Transform shootingPoint;
    Animator anim;
    float bulletTime;
    // Start is called before the first frame update
    void Start()
    {
        shootingTimeCounter = shootingTime;
        anim = GetComponent<Animator>();
        bulletTime = turretBullet.GetComponent<turretBullet>().bulletTime;
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimeCounter -= Time.deltaTime;
        Shoot();

    }
    void Shoot()
    {

        if (shootingTimeCounter <= 0f)
        {
            
            shootingTimeCounter = shootingTime;
            anim.SetBool("isShooting", true);
            StartCoroutine(Waiting(0.3f));
            
        }
    }

    public IEnumerator Waiting(float bulletLength)
    {
        yield return new WaitForSeconds(bulletLength);

        // Instantiate the bullet
        GameObject bullet = Instantiate(turretBullet, shootingPoint.position, Quaternion.identity);

        // Set the bullet's direction based on the turret's scale
        if (transform.localScale.x < 0f)
        {
            // If the turret is facing left, flip the bullet's direction
            bullet.GetComponent<turretBullet>().SetDirection(Vector2.left);
        }
        else
        {
            // Otherwise, the turret is facing right, keep the bullet's default direction (right)
            bullet.GetComponent<turretBullet>().SetDirection(Vector2.right);
        }

        anim.SetBool("isShooting", false);
    }

}
