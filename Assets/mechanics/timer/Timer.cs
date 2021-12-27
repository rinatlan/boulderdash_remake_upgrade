using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour
{
    // shows (by printing) the time remains until its over
    
    public float tick = 1; // if one second passed
    public int timeRemains = 150;
    public bool isTimeRuns = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tick > 0)
        {
            tick -= Time.deltaTime;
        }
        else
        {
            if (timeRemains > 0)
            {
                print(timeRemains);
                timeRemains -= 1;
            }
            else
            {
                if (isTimeRuns)
                {
                    print("time has run out!");
                    timeRemains = 0;
                    isTimeRuns = false;
                }
            }
            tick = 1;
            
        }
    }
    
}