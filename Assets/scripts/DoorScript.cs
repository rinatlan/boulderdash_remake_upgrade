using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool _isDoorOpen = false;

    private Animator _animator;
    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetDiamonds() >= 15)
        {
            _animator.Play("door is open");
            _isDoorOpen = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _isDoorOpen)
        {
            SceneManager.LoadScene("Game Won", LoadSceneMode.Single);   
        }
    }
}
