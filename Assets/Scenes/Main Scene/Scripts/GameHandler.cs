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

    [SerializeField]
    private Transform playerSpawnPoint;

    [SerializeField]
    private PlayerState playerState;
    
    // prefabs
    [SerializeField]
    private Transform pfPlayer;

    [SerializeField]
    private GameObject pfCoin;

    [SerializeField]
    private GameObject[] pfGrounds;

    [SerializeField]
    private GameObject[] pfBackgrounds;

    // generated level objects
    private List<GameObject> backgrounds = new List<GameObject>();
    private List<GameObject> platforms = new List<GameObject>();
    private List<GameObject> coins = new List<GameObject>();

    // main game area container
    [SerializeField]
    private Transform gameArea;
    
    private void Awake() {
        SpawnLevelStart();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(backgrounds.Count);
        //Debug.Log(backgrounds[backgrounds.Count - 1]);
        if(playerState.playerObject.transform.position.x > backgrounds[backgrounds.Count - 1].transform.position.x - 10f){
            Vector3 lastBgPos = backgrounds[backgrounds.Count - 1].transform.position;
            int platformSelection = playerState.hasJumpped ? pfGrounds.Length : pfGrounds.Length - 1;
            backgrounds.Add(Instantiate(pfBackgrounds[Random.Range(0, pfBackgrounds.Length)], new Vector3(lastBgPos.x + 30, lastBgPos.y, 30f), Quaternion.identity, backgroundContainer));
            platforms.Add(Instantiate(pfGrounds[Random.Range(0, platformSelection)], new Vector3(lastBgPos.x + 30, 0f), Quaternion.identity, groundContainer));
            if(playerState.hasJumpped){
                SpawnCoins(lastBgPos.x + 30);
            }
            if(backgrounds.Count > 4){
                Destroy(backgrounds[0]);
                backgrounds.Remove(backgrounds[0]);
                Destroy(platforms[0]);
                platforms.Remove(platforms[0]);
            }
        }
    }

    private void LateUpdate() {
        Camera.main.transform.position = new Vector3(playerState.playerObject.transform.position.x, 3.5f, -30);
    }

    private void SpawnCoins(float startPos){
        int numCoins = UnityEngine.Random.Range(5, 15);
        for (int i = 0; i < numCoins; i++) {
            float coinY = UnityEngine.Random.Range(1f, 3.5f);
            float coinX = UnityEngine.Random.Range(0, 30);
            coins.Add(Instantiate(pfCoin, new Vector3(coinX + startPos, coinY, 0), Quaternion.identity, coinContainer));
        }
    }

    private void SpawnPlayer()
    {
        playerState.SpawnPlayer(playerSpawnPoint.position);
    }   

    private void SpawnLevelStart()
    {
        backgrounds.Add(Instantiate(pfBackgrounds[0], new Vector3(0f, 1.25f, 30f), Quaternion.identity, backgroundContainer));
        backgrounds.Add(Instantiate(pfBackgrounds[0], new Vector3(25f, 1.25f, 30f), Quaternion.identity, backgroundContainer));
        platforms.Add(Instantiate(pfGrounds[0], new Vector3(0,0), Quaternion.identity, groundContainer));
        platforms.Add(Instantiate(pfGrounds[0], new Vector3(30f,0), Quaternion.identity, groundContainer));
        SpawnCoins(0f);
        SpawnCoins(25f);
    }
    

    private void Player_OnPlayerDeath(object sender, System.EventArgs e){
        SpawnLevelStart();
        SpawnPlayer();
    }
}
