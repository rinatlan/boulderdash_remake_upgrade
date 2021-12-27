using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    // supposed to be the block, when the player (circle) reach it it will disappear.
    // in the game it is sand, a diamond, or a rock.
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D()
    {
        gameObject.SetActive(false);
    }
}
