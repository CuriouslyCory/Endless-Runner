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

    private int coinsCollected;
    public EventHandler<CoinCollectedEventArg> OnCoinCollected;

    public Camera playerCam;

    public float jumpTime;
    private float jumpTimeCounter;

    private enum PlayerState {
        Jumping,
        Floating,
        Idle,
    }

    private PlayerState playerState = PlayerState.Idle;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();

    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
    }

    private void Update()
    {

        if(IsGrounded() && Input.GetKeyDown(KeyCode.Space)){
            playerState = PlayerState.Jumping;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpVelocity;
        }

        if(Input.GetKey(KeyCode.Space) && playerState == PlayerState.Jumping){
            if(jumpTimeCounter > 0){
                rb.velocity = Vector2.up * jumpVelocity;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                playerState = PlayerState.Idle;
            }
            
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            playerState = PlayerState.Idle;
        }

        if(
            !IsGrounded() && jumpTimeCounter > 0 &&
            (playerState == PlayerState.Floating || playerState == PlayerState.Idle) && 
            (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
        ){
            rb.velocity = Vector2.up * 0;
            jumpTimeCounter -= Time.deltaTime;
            playerState = PlayerState.Floating;
        }   


    }

    private bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.01f, platformLayermask);
        Debug.Log(raycast.collider);
        return raycast.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject);
        if(((1<<collider.gameObject.layer) & coinLayerMask) != 0) {
            Destroy(collider.gameObject);
            coinsCollected++;
            OnCoinCollected?.Invoke(this, new CoinCollectedEventArg {value = coinsCollected});
        }
    }

    


    
}
public class CoinCollectedEventArg: EventArgs
{
    public int value;
}
