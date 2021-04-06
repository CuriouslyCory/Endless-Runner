using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimerBar : MonoBehaviour
{
    
    [SerializeField]
    private Transform timerBar;

    private float startTime;
    private float currentTime;
    
    // Start is called before the first frame update
    void Start()
    {
        timerBar.localScale = new Vector3(0f, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime >= 0){
            currentTime -= Time.deltaTime;
            timerBar.localScale = new Vector3(currentTime/startTime, 1, 0);
        }else{
            timerBar.localScale = new Vector3(0, 1, 0);
        }
    }

    public void StartTimer(float startTime)
    {
        this.startTime = startTime;
        currentTime = startTime;
    }
}
