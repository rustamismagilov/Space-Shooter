using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private GameManager _gameManager;
    private bool isPlayerOne;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //If a key or left arrow is down or up
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _animator.SetBool("Turn_Left", true);
            _animator.SetBool("Turn_Right", false);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _animator.SetBool("Turn_Left", false);
            _animator.SetBool("Turn_Right", false);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _animator.SetBool("Turn_Right", true);
            _animator.SetBool("Turn_Left", false);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _animator.SetBool("Turn_Right", false);
            _animator.SetBool("Turn_Left", false);
        }
    }
}
