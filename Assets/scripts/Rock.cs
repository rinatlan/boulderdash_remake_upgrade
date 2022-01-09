using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    public Animator rockAnimator;
    
    private Vector2 _currPos;
    public bool _pushed = false;
    private Vector2 _newPos;

    private Collider2D[] hitsExplosion;
    private Vector2 ExplosionPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _newPos = GetComponent<Rigidbody2D>().position;
    }
    
    private void FixedUpdate()
    {
        if ( gameObject.GetComponent<Rigidbody2D>().position == _newPos &&
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
        // player pushes the rock
        if (gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
                // player push to the direction
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
        // checks if rock can  move to direction
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

    public void Explode()
    {
        // explodes the rock and objects near it
        Vector2 size = new Vector2(2, 2);
        ExplosionPos = gameObject.GetComponent<Rigidbody2D>().position;
        gameObject.GetComponent<Animator>().SetTrigger("explosion");
        hitsExplosion = Physics2D.OverlapBoxAll(ExplosionPos, size, 90);
        foreach (var hit in hitsExplosion)
        {
            if (hit.gameObject.CompareTag("sand") ||
                hit.gameObject.CompareTag("rock") ||
                hit.gameObject.CompareTag("diamond") ||
                hit.gameObject.CompareTag("Player"))
            {
                hit.gameObject.GetComponent<Animator>().SetTrigger("explosion");
                if (hit.gameObject.CompareTag("Player"))
                {
                    GameManager.MinusLive();
                }
            }
        }
        Invoke("DontActivate", 3f);
        
    }
    
    
    private void DontActivate()
    {
        // set active falls to game objects due explosion
        foreach (var hit in hitsExplosion)
        {
            Vector2 newPos = _newPos;
            if ( (!(hit.gameObject.name == "Door")) &&
                 (!(hit.gameObject.CompareTag("wallFloor"))) &&
                 !(hit.gameObject.CompareTag("squareCamera")))
            {
                newPos = hit.GetComponent<Transform>().position;
                hit.gameObject.SetActive(false);
            }

            
            RaycastHit2D hitAfterExplosion = Physics2D.Raycast(newPos, Vector2.up);
            if (hitAfterExplosion.collider != null && hitAfterExplosion.collider.CompareTag("fallOnPlayer"))
            {
                hitAfterExplosion.collider.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
        }
        
    }


    
}
