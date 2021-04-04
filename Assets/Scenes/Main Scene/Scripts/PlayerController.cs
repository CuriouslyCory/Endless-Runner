using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpVelocity = 7f;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private LayerMask platformLayermask;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.velocity = Vector2.up * jumpVelocity;    
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down * 0.1f, platformLayermask);
        return raycast.collider != null;
    }
    
}
