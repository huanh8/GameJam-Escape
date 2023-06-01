using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Animator _animator;
    private Rigidbody2D _rb;
    private Vector2 _dir;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        PlayerInput();
        MovePlayer(_dir);
    }

    private void PlayerInput()
    {
        _dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            _dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _dir.x = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            _dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _dir.y = -1;
        }
    }

    private void FlipPlayer()
    {
        _renderer.flipX = _dir.x < 0;
    }
    private void MovePlayer(Vector2 dir)
    {
         FlipPlayer();
        _dir.Normalize();
        _rb.velocity = speed * dir;
        _animator.SetBool("IsMoving", _dir.magnitude > 0);
    }
}
