using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTurretFinalBoss : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] Transform upperPositionMarker;
    [SerializeField] Transform lowerPositionMarker;

    private Transform targetPosition;

    private void Start()
    {
        // Start by moving towards the upper position
        targetPosition = upperPositionMarker;
    }

    private void Update()
    {
        // Move the enemy towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

        // Check if the enemy has reached the target position
        if (Vector2.Distance(transform.position, targetPosition.position) <= 0.1f)
        {
            // Switch to the other target position
            if (targetPosition == upperPositionMarker)
            {
                targetPosition = lowerPositionMarker;
            }
            else
            {
                targetPosition = upperPositionMarker;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}

