using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [HideInInspector]
    public GameObject playerObject;
    [HideInInspector]
    public PlayerController playerController;
    
    [SerializeField]
    private GameObject pfPlayer;

    protected int coins;

    public EventHandler<CoinCollectedEventArg> OnCoinCollected;
    public EventHandler OnPlayerDeath;
    public EventHandler OnThrowMarker;

    public bool hasJumpped = false;
    public bool hasThrownMarker = false;
    public bool hasTeleported = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer(Vector3 position)
    {
        playerObject = Instantiate(pfPlayer, position , Quaternion.identity, transform.parent);
        playerController = playerObject.GetComponent<PlayerController>();
        playerController.OnCoinCollected += PlayerController_OnCoinCollected;
        playerController.OnPlayerDeath += PlayerController_OnPlayerDeath;
        playerController.OnThrowMarker += PlayerController_OnThrowMarker;
    }

    public void DestroyPlayer()
    {
        playerController.OnCoinCollected -= PlayerController_OnCoinCollected;
        playerController.OnPlayerDeath -= PlayerController_OnPlayerDeath;
        playerController.OnThrowMarker -= PlayerController_OnThrowMarker;
        Destroy(playerObject);
    }
    
    private void PlayerController_OnCoinCollected(object sender, CoinCollectedEventArg e)
    {
        coins += e.value;
        OnCoinCollected?.Invoke(this, new CoinCollectedEventArg {value = coins});
    }

    private void PlayerController_OnPlayerDeath(object sender, EventArgs e) {
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        DestroyPlayer();
    }

    private void PlayerController_OnThrowMarker(object sender, EventArgs e) {
        OnThrowMarker?.Invoke(this, EventArgs.Empty);
    }

}

public class CoinCollectedEventArg: EventArgs
{
    public int value;
}
