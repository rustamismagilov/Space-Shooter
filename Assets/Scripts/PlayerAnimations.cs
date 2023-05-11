using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerController.isPlayerOne == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _animator.SetBool("Turn_Left", true);
                _animator.SetBool("Turn_Right", false);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                _animator.SetBool("Turn_Left", false);
                _animator.SetBool("Turn_Right", false);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                _animator.SetBool("Turn_Right", false);
                _animator.SetBool("Turn_Left", false);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _animator.SetBool("Turn_Right", true);
                _animator.SetBool("Turn_Left", false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _animator.SetBool("Turn_Left", true);
                _animator.SetBool("Turn_Right", false);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _animator.SetBool("Turn_Left", false);
                _animator.SetBool("Turn_Right", false);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                _animator.SetBool("Turn_Right", false);
                _animator.SetBool("Turn_Left", false);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _animator.SetBool("Turn_Right", true);
                _animator.SetBool("Turn_Left", false);
            }
        }

    }
}
