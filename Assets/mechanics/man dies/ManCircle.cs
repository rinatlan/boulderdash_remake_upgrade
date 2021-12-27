using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManCircle : MonoBehaviour
{
    // the player is the circle and the rock that falls on him is the square. 
    // in the game if the rock falls on the player it disappears.
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "block")
        {
            gameObject.SetActive(false);
        }
    }
}
