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
    public State PlayerState;
    [Range(0, 5)][SerializeField] private float _coolDown = 1.5f;
    [SerializeField] private float nextDash = 0;

    public bool _canDash = false;
    public bool _canMove = true;
    public bool invincible = false;
    public GameObject SpawnPoint;

    public enum State
    {
        Normal,
        Dashing
    }

    private void Start()
    {
        PlayerState = State.Normal;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator.ResetTrigger("IsDashing");
        _animator.SetBool("IsDead", false);
        _canMove = true;
    }
    private void Update()
    {
        if (!_canMove) return;
        PlayerInput();
        MovePlayer();
        UIManager.instance.ShowHint(_canDash);
    }

    private void PlayerInput()
    {
        switch (PlayerState)
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
                    PlayerState = State.Normal;
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
                StartCoroutine(Invincible());
            }
        }
    }

    IEnumerator Invincible()
    {
        invincible = true;
        _renderer.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.8f);
        _renderer.color = new Color(1f, 1f, 1f, 1f);
        invincible = false;
    }
    private void PlayDash()
    {
        DashSpeed = 15f;
        _animator.SetTrigger("IsDashing");
        _dashDir = _moveDir;
        PlayerState = State.Dashing;
    }

    private void FlipPlayer()
    {
        if (_moveDir.x == 0) return;
        transform.localScale = new Vector3(_moveDir.x > 0 ? 1 : -1, 1, 1);
    }

    private void MovePlayer()
    {

        switch (PlayerState)
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
    public void PlayerDead()
    {
        _animator.SetBool("IsDead", true);
        _rb.velocity = Vector2.zero;
        _canDash = false;
        _canMove = false;
    }

    [ContextMenu("Reset")]
    public void Reset()
    {
        _animator.SetBool("IsDead", false);
        _canMove = true;
        _rb.position = SpawnPoint.transform.position;
    }

    public void EnableDash()
    {
        _canDash = true;
    }
    public void DisableDash()
    {
        _canDash = false;
    }
}
