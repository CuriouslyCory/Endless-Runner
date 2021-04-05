using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradePanel;

    [SerializeField]
    private GameObject openUpgradeButton;
    
    void Start()
    {
        
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
}
