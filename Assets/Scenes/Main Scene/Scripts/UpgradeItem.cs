using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UpgradeItem : MonoBehaviour
{
    public struct UpgradeDefinition
    {
        public string title;
        public string description;
        public int level;
        public Image image;
    }
    
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private TextMeshProUGUI level;

    [SerializeField]
    private Image image;

    public void Setup(UpgradeDefinition upgrade){
        title.text = upgrade.title;
        description.text = upgrade.description;
        level.text = upgrade.level.ToString();
        image = upgrade.image;
    }
    
}
