using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    
    // object containers
    [SerializeField]
    private Transform backgroundContainer;

    [SerializeField]
    private Transform groundContainer;

    [SerializeField]
    private Transform coinContainer;

    // player transform
    private Transform tfPlayer;
    
    // prefabs
    [SerializeField]
    private Transform pfPlayer;

    [SerializeField]
    private GameObject pfCoin;

    [SerializeField]
    private GameObject pfFlatGround;

    [SerializeField]
    private GameObject pfBackground;

    // generated level objects
    private List<GameObject> backgrounds = new List<GameObject>();
    private List<GameObject> platforms = new List<GameObject>();
    private List<GameObject> coins = new List<GameObject>();

    // main game area container
    [SerializeField]
    private Transform gameArea;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private TextMeshProUGUI coinText;
    
    
    private void Awake() {
        backgrounds.Add(Instantiate(pfBackground, new Vector3(0f, 1.25f, 30f), Quaternion.identity, backgroundContainer));
        backgrounds.Add(Instantiate(pfBackground, new Vector3(25f, 1.25f, 30f), Quaternion.identity, backgroundContainer));
        platforms.Add(Instantiate(pfFlatGround, new Vector3(0,0), Quaternion.identity, groundContainer));
        platforms.Add(Instantiate(pfFlatGround, new Vector3(25f,0), Quaternion.identity, groundContainer));
        SpawnCoins(0f);
        SpawnCoins(25f);
    }

    // Start is called before the first frame update
    void Start()
    {
        tfPlayer = Instantiate(pfPlayer, new Vector3(0f, 0f, 0f), Quaternion.identity, gameArea);
        tfPlayer.GetComponent<PlayerController>().OnCoinCollected += Player_OnCoinsCollected;
        canvas.worldCamera = tfPlayer.GetComponent<PlayerController>().playerCam;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(backgrounds.Count);
        //Debug.Log(backgrounds[backgrounds.Count - 1]);
        if(tfPlayer.transform.position.x > backgrounds[backgrounds.Count - 1].transform.position.x - 10f){
            Vector3 lastBgPos = backgrounds[backgrounds.Count - 1].transform.position;
            backgrounds.Add(Instantiate(pfBackground, new Vector3(lastBgPos.x + 25, lastBgPos.y, 30f), Quaternion.identity, backgroundContainer));
            platforms.Add(Instantiate(pfFlatGround, new Vector3(lastBgPos.x + 25, 0f), Quaternion.identity, groundContainer));
            SpawnCoins(lastBgPos.x + 25);
            if(backgrounds.Count > 4){
                Destroy(backgrounds[0]);
                backgrounds.Remove(backgrounds[0]);
                Destroy(platforms[0]);
                platforms.Remove(platforms[0]);
            }
        }
    }

    private void SpawnCoins(float startPos){
        int numCoins = UnityEngine.Random.Range(5, 15);
        for (int i = 0; i < numCoins; i++) {
            float coinY = UnityEngine.Random.Range(0.5f, 2.5f);
            float coinX = UnityEngine.Random.Range(0, 25);
            coins.Add(Instantiate(pfCoin, new Vector3(coinX + startPos, coinY, 0), Quaternion.identity, coinContainer));
        }
    }

    private void Player_OnCoinsCollected(object sender, CoinCollectedEventArg e)
    {
        coinText.text = e.value.ToString();
    }
}
