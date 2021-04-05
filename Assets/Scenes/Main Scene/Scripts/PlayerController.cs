using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [SerializeField]
    private float jumpVelocity = 7f;

    [SerializeField]
    private LayerMask platformLayermask;

    [SerializeField]
    private LayerMask coinLayerMask;
    
    [SerializeField]
    private float playerSpeed = 1f;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if(IsGrounded() && Input.GetKeyDown(KeyCode.Space)){
            rb.velocity = Vector2.up * jumpVelocity;    
        }
        rb.velocity = new Vector2(+playerSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 1f, platformLayermask);
        //Debug.Log(raycast.collider);
        return raycast.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject);
        if(((1<<collider.gameObject.layer) & coinLayerMask) != 0) {
            Destroy(collider.gameObject);
        }
    }


    
}
