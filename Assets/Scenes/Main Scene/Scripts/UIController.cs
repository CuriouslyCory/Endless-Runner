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
    
    void Start()
    {
        playerState.OnCoinCollected += Player_OnCoinsCollected;
        playerState.OnThrowMarker += Player_MarkerThrown;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

}
