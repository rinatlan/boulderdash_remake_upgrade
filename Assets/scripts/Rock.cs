using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    private Vector2 _currPos;
    public bool _pushed = false;
    private Vector2 _newPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void FixedUpdate()
    {
        if (gameObject.GetComponent<Rigidbody2D>().position == _newPos &&
            gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic)
        {
            _currPos = Vector2.zero;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Rigidbody2D>().constraints = (RigidbodyConstraints2D.FreezePositionX) &
                                                      ~RigidbodyConstraints2D.FreezePositionY;
            _pushed = false;
            // after rock was pushed and there is nothing under it - supposed to be dynamic and fall
            RaycastHit2D[] downHits = Physics2D.RaycastAll(GetComponent<Rigidbody2D>().position, 
                Vector2.down, 0.5f);
            if (downHits.Length == 1)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
        
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!(other.gameObject.CompareTag("Player") ))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

    }

    public void MoveInDir(Vector2 direction)
    {
        if (gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
                // player push to the right
            {
                if (CanMove(direction))
                {
                    _currPos.x = 1;
                    _newPos = GetComponent<Rigidbody2D>().position + direction;
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    GetComponent<Rigidbody2D>().constraints = (~RigidbodyConstraints2D.FreezePositionX) &
                                                              RigidbodyConstraints2D.FreezePositionY;
                    GetComponent<Rigidbody2D>().MovePosition(_newPos);
                    
                    _pushed = true;
                }
            }
    }
    
    
    private bool CanMove(Vector2 directionToCheck)
    {
        Vector2 pos = gameObject.GetComponent<Rigidbody2D>().position;
        RaycastHit2D[] downHits = Physics2D.RaycastAll(pos, Vector2.down, 1f);
        if (downHits.Length > 0) // there is object down
            { 
                if (directionToCheck == Vector2.right)
                {
                    RaycastHit2D[] rightHits = Physics2D.RaycastAll(pos, Vector2.right, 1f);
                    if (rightHits.Length == 0) // nothing on right
                    {
                        return true;
                    }
                    return false;
                }
                
                else if (directionToCheck == Vector2.left)
                {
                    RaycastHit2D[] rightHits = Physics2D.RaycastAll(pos, Vector2.left, 1f);
                    if (rightHits.Length == 0) // nothing on left
                    {
                        return true;
                    }
                    return false;
                }
            }
        return false;
    }


    
}
