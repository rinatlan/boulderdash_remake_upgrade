using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeRemains : MonoBehaviour
{
    
    TextMeshProUGUI _time;
    
    private float tick = 1; // if one second passed
    private int timeRemains = 150;
    
    //private bool isTimeRuns = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _time = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tick > 0)
        {
            tick -= Time.deltaTime;
        }
        else // if tick == 0, means 1 second passed, need to display
        {
            if (timeRemains > 0) // there is more time - game not over
            {
                _time.text = System.Convert.ToString(timeRemains);
                timeRemains -= 1;
            }
            else // GAME OVER
            {
                
                //if (isTimeRuns)
                //{
                //    timeRemains = 0;
                //    isTimeRuns = false;
                //}
            }
            tick = 1;
            
        }
    }
}
