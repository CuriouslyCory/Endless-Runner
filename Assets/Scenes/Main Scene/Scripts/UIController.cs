using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradePanel;

    [SerializeField]
    private GameObject openUpgradeButton;

    [SerializeField]
    private CountdownTimerBar markerTimerBar;

    [SerializeField]
    private TextMeshProUGUI coinText;

    [SerializeField]
    private PlayerState playerState;

    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private TextMeshProUGUI tutorialText;
    
    void Start()
    {
        playerState.OnCoinCollected += Player_OnCoinsCollected;
        playerState.OnThrowMarker += Player_MarkerThrown;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !playerState.hasJumpped){
            playerState.hasJumpped = true;
            tutorialText.text = "Great! Now try clicking somewhere on the screen to throw a teleport marker.";
        }


    }

    public void OnClickOpenUpgradePanelButton()
    {
        OpenUpgradePanel();
    }

    private void OpenUpgradePanel()
    {
        upgradePanel.SetActive(true);
        openUpgradeButton.SetActive(false);
    }

    public void OnClickCloseUpgradePanelButton()
    {
        CloseUpgradePanel();
    }

    private void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
        openUpgradeButton.SetActive(true);
    }

    private void Player_OnCoinsCollected(object sender, CoinCollectedEventArg e)
    {
        coinText.text = e.value.ToString();
    }

    private void Player_MarkerThrown(object sender, EventArgs e)
    {
        markerTimerBar.StartTimer(playerState.playerController.markerCooldownTimer);
        if(!playerState.hasThrownMarker && playerState.hasJumpped == true){
            playerState.hasThrownMarker = true;
            tutorialText.text = "The red bar indicates how long until you can throw another marker.";
        }
    }

}
