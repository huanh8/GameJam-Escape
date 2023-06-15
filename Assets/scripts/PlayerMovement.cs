using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public float DashSpeed;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    private Vector2 _dashDir;

    private SpriteRenderer _renderer;
    private State _state;
    [Range(0, 5)][SerializeField] private float _coolDown = 1.5f;
    [SerializeField] private float nextDash = 0;

    private bool _canDash = true;

    enum State
    {
        Normal,
        Dashing,
    }

    private void Start()
    {
        _state = State.Normal;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator.ResetTrigger("IsDashing");
    }
    private void Update()
    {
        PlayerInput();
        MovePlayer();
    }

    private void PlayerInput()
    {
        switch (_state)
        {
            case State.Normal:
                NormalInput();
                break;
            case State.Dashing:
                float dashSpeedDropMultiplier = 3f;
                DashSpeed -= DashSpeed * dashSpeedDropMultiplier * Time.deltaTime;
                float dashSpeedMinimum = 10f;
                if (DashSpeed < dashSpeedMinimum)
                {
                    _state = State.Normal;
                }
                break;
        }

    }
    void NormalInput()
    {
        _moveDir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            _moveDir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _moveDir.x = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            _moveDir.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _moveDir.y = -1;
        }
        _moveDir.Normalize();

        if (Input.GetKeyDown(KeyCode.Space) && _canDash)

        {
            if (Time.time >= nextDash)
            {
                nextDash = Time.time + _coolDown;
                PlayDash();
            }
            
        }
    }

    private void PlayDash()
    {
        DashSpeed = 15f;
        _animator.SetTrigger("IsDashing");
        _dashDir = _moveDir;
        _state = State.Dashing;
    }

    private void FlipPlayer()
    {
        if (_moveDir.x == 0) return;
        transform.localScale = new Vector3(_moveDir.x > 0 ? 1 : -1, 1, 1);
    }

    private void MovePlayer()
    {

        switch (_state)
        {
            case State.Normal:
                _rb.velocity = MoveSpeed * _moveDir;
                _animator.SetBool("IsMoving", _moveDir.magnitude > 0);
                break;
            case State.Dashing:
                _rb.velocity = _dashDir * DashSpeed;
                break;
        }
        FlipPlayer();

    }


}
