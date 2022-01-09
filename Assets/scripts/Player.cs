using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;

    private Animator _playerAnimator;
    public AudioSource sound;

    private Vector2 _movement;
    private float _moveSpeed = 5f;
    private bool moving;

    private float _tick = 7;
    
    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        sound = FindObjectOfType<AudioSource>();
        Physics2D.callbacksOnDisable = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        // player moves according to pressed arrow
        
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _movement.x = -1f;
            _movement.y = 0;
            moving = true;
            _playerAnimator.SetTrigger("leftWalk");
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _movement.x = 1f;
            _movement.y = 0;
            moving = true;
            _playerAnimator.SetTrigger("rightWalk");
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _movement.x = 0;
            _movement.y = 1f;
            moving = true;
            _playerAnimator.SetTrigger("leftWalk");
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _movement.x = 0;
            _movement.y = -1f;
            moving = true;
            _playerAnimator.SetTrigger("leftWalk");
        }
        else
        {
            if (!moving)
            {
                _movement.x = 0;
                _movement.y = 0;
                
            }
        }
        var hit = Physics2D.Raycast(gameObject.GetComponent<Rigidbody2D>().position, _movement, 1f);
        if (hit.collider != null && hit.collider.CompareTag("rock"))
        {
            hit.collider.GetComponent<Rock>().MoveInDir(_movement);
        }
        
        
        // explodes the rock near player and what around it
        
        if (_tick > 0)
        {
            _tick -= Time.deltaTime;
        }
        else // if tick == 0, means 15 seconds passed, need to explode near rock
        {
            RockExplosion();
            _tick = 7;
        }
    }

    private void FixedUpdate()
    {
        rigidBody2D.velocity = Vector2.zero;
        
        if (moving)
        {
            sound.Play();
            Vector2 newPos = rigidBody2D.position + _movement;
            rigidBody2D.MovePosition(newPos);
            _movement = Vector2.zero;
            moving = false;
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
    }
    
   
    private void OnTriggerExit2D(Collider2D other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("sand"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("diamond"))
        {
            other.gameObject.SetActive(false);
            GameManager.AddPoints();
            GameManager.AddDiamond();
        }
        if (other.gameObject.CompareTag("fallOnPlayer"))
        {
            if (other.GetComponentInParent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                Vector2 size = new Vector2(2, 2);
                Vector2 lastPosPlayer = rigidBody2D.position;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                gameObject.GetComponent<Animator>().SetTrigger("explosion");
                var hits = Physics2D.OverlapBoxAll(lastPosPlayer, size, 90);
                foreach (var hit in hits)
                {
                    if (hit.gameObject.CompareTag("wall") == false && 
                        hit.gameObject.CompareTag("wallFloor") == false && 
                        hit.gameObject.CompareTag("fallOnPlayer") == false && 
                        hit.gameObject.CompareTag("sandOtherCollider") == false)
                    {
                        hit.gameObject.GetComponent<Animator>().SetTrigger("explosion");
                    }
                }
                
            }
        }
        if (other.name == "Square")
        {
            GameManager.xPos = 10;
            GameManager.yPos = -0.5f;
        }
        if (other.name == "Square2")
        {
            GameManager.xPos = 21.5f;
            GameManager.yPos = -0.5f;
        }
        if (other.name == "Square3")
        {
            GameManager.xPos = 21.5f;
            GameManager.yPos = -6f;
        }
        if (other.name == "Square4")
        {
            GameManager.xPos = 10;
            GameManager.yPos = -6f;
        }
        if (other.name == "Square5")
        {
            GameManager.xPos = -0.501f;
            GameManager.yPos = -6f;
        }
        if (other.name == "Square6")
        {
            GameManager.xPos = -0.501f;
            GameManager.yPos = -13.2f;
        }
        if (other.name == "Square7")
        {
            GameManager.xPos = 12f;
            GameManager.yPos = -13.2f;
        }
        if (other.name == "Square8")
        {
            GameManager.xPos = 21.5f;
            GameManager.yPos = -13.2f;
        }
    }

    private void RockExplosion()
    // do the blinking of the near rock to the player and calls the function that explodes what around this rock
    {
        Vector2 size = new Vector2(5, 5);
        Vector2 lastPosPlayer = rigidBody2D.position;
        var hits = Physics2D.OverlapBoxAll(lastPosPlayer, size, 90);
        foreach (var hit in hits)
        {
            if (hit.gameObject.CompareTag("rock"))
            {
                hit.gameObject.GetComponent<Rock>().rockAnimator.SetBool("blink", true);
                hit.gameObject.GetComponent<Rock>().Invoke("Explode", 3f);
                break;
            }
        }
        
    }
    
    public void dead()
    {
        GameManager.MinusLive();
    }
    
    
}
