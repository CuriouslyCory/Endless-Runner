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

    [SerializeField]
    GameObject pfMarker;

    [SerializeField]
    private ParticleSystem pfTeleportPop;
    private ParticleSystem teleportPop;

    public float jumpTime;
    private float jumpTimeCounter;

    public float markerCooldown = 2f;
    public float markerVelocity = 5f;
    
    public float markerCooldownTimer {get; private set;}

    public EventHandler OnPlayerDeath;
    public EventHandler<CoinCollectedEventArg> OnCoinCollected;
    public EventHandler OnThrowMarker;


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

        if(Input.GetMouseButtonDown(0) && markerCooldownTimer <= 0){
            Vector3 playerPosition = transform.position;
            Vector3 mousePosition = GetWorldPositionOnPlane(Input.mousePosition, 0);
            Vector3 direction = mousePosition - playerPosition;
            Debug.Log(mousePosition);
            Debug.Log(playerPosition);
            Debug.Log(direction);
            ThrowMarker(direction);            
        }

        if(markerCooldownTimer > 0){
            markerCooldownTimer -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.T)){
            Teleport();
        }


    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, platformLayermask);
        //Debug.Log(raycast.collider);
        return raycast.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject);
        if(((1<<collider.gameObject.layer) & coinLayerMask) != 0) {
            Destroy(collider.gameObject);
            OnCoinCollected?.Invoke(this, new CoinCollectedEventArg {value = 1});
        }
    }

    private void ThrowMarker(Vector3 direction) {
        markerCooldownTimer = markerCooldown;
        GameObject newMarker = Instantiate(pfMarker, transform);
        newMarker.GetComponent<Rigidbody2D>().velocity = direction * markerVelocity;
        OnThrowMarker?.Invoke(this, EventArgs.Empty);
    }

    private void Teleport()
    {
        GameObject[] marker = GameObject.FindGameObjectsWithTag("Marker");
        if(marker.Length > 0){
            if(teleportPop == null){
                teleportPop = Instantiate(pfTeleportPop, transform.position, Quaternion.identity, transform.parent);
            }else{
                teleportPop.transform.position = transform.position;
            }
            teleportPop.Play();
            transform.position = marker[0].transform.position;
            Destroy(marker[0]);
        }
    }

    private void OnBecameInvisible() 
    {
        PlayerDeath();
    }

    private void PlayerDeath()
    {
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }


    
}
