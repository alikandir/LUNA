using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSpaceship : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity=Vector2.up*moveSpeed;
    }
}
