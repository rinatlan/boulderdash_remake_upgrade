using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Man : MonoBehaviour
{
    // the player is the circle, when it reaches the wall it cant move through it 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = gameObject.transform.position;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= 0.04f;
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += 0.04f;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += 0.04f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= 0.04f;
        }
        gameObject.transform.position = pos;
    }
}