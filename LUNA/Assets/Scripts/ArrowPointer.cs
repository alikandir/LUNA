using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    [SerializeField] Transform drill;
    [SerializeField] Transform exit;
    [SerializeField] GameObject exitObject;
    DrillBehaviour drillScript;
    private Transform player;
    bool isFlipped=false;

    private void Start()
    {
        drillScript = FindObjectOfType<DrillBehaviour>();
        player = FindObjectOfType<playerMovement>().transform;
    }
    private void Update()
    {
        if(drillScript.drillSwitch) 
        {
            gameObject.SetActive(false);
        }
        if (!drillScript.drillSwitch && !exitObject.gameObject.activeInHierarchy)
        {
            PointTowards(drill);
        }
        if (exitObject.gameObject.activeInHierarchy)
        {
           
            PointTowards(exit);
        }
    }

    void PointTowards(Transform target)
    {
        // Calculate the direction from the player to the target (drill).
        Vector3 direction = target.position - player.position;

        // Calculate the rotation angle in degrees.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(player.localScale.x<0f)
        {
            // Apply the rotation to the arrow GameObject.
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (isFlipped)
            {
                isFlipped = false;
                transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            }
            
        }
        else if (player.localScale.x > 0f)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (!isFlipped)
            {
                isFlipped = true;
                transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            }


        }


    }
}
