using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    
    [SerializeField]
    GameObject pfBackground;
    
    [SerializeField]
    private Transform player;

    private List<GameObject> backgrounds;

    
    private void Awake() {
        backgrounds = new List<GameObject>();
        backgrounds.Add(Instantiate(pfBackground, new Vector3(0f,0f), Quaternion.identity));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(backgrounds.Count);
        Debug.Log(backgrounds[backgrounds.Count - 1]);
        if(player.transform.position.x > backgrounds[backgrounds.Count - 1].transform.position.x - 10){
            Vector3 lastBgPos = backgrounds[backgrounds.Count - 1].transform.position;
            backgrounds[backgrounds.Count] = Instantiate(pfBackground, new Vector3(lastBgPos.x + 10, lastBgPos.y), Quaternion.identity);
        }
    }
}
