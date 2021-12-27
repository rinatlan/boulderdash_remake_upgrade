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
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!(other.gameObject.CompareTag("Player") ))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        // MoveInDir(other);
    }

    public void MoveInDir(Vector2 direction)
    {
        if (gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
                // player push to the right
            {
                if (CanMove("right"))
                {
                    print("b");
                    _currPos.x = 1;
                    _newPos = GetComponent<Rigidbody2D>().position + direction;
                    Vector2 newVel = new Vector2(1f, 0);
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    GetComponent<Rigidbody2D>().constraints = (~RigidbodyConstraints2D.FreezePositionX) &
                                                              RigidbodyConstraints2D.FreezePositionY;
                    GetComponent<Rigidbody2D>().MovePosition(_newPos);
                    //
                    _pushed = true;
                }
            }
    }
    //
    //
    // NEW CODE HERE 
    
    private bool CanMove(string directionToCheck)
    {
        Vector2 pos = gameObject.GetComponent<Rigidbody2D>().position;
        RaycastHit2D[] downHits = Physics2D.RaycastAll(pos, Vector2.down, 1f);
        // not null for true
        // foreach (var downHit in downHits)
        // {
        //     if ( (downHit.collider.CompareTag("sand") || 
        //           downHit.collider.CompareTag("diamond") ||
        //           downHit.collider.CompareTag("rock") ||
        //           downHit.collider.CompareTag("wall") ||
        //           downHit.collider.CompareTag("wallFloor")) &&
        //          downHit.collider.GetComponent<Rigidbody2D>().position != pos) // there is object down
        if (downHits.Length > 0) // there is object down
            {
                if (directionToCheck == "right")
                {
                    RaycastHit2D[] rightHits = Physics2D.RaycastAll(pos, Vector2.right, 1f);
                    if (rightHits.Length == 0) // nothing on right
                    {
                        return true;
                    }
                    print(rightHits);
                    return false;
                }
            }
        print(downHits);
        return false;
    }


    
}
