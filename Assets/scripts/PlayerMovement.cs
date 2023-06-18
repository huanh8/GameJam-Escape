using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public bool CanDash = false;
    public bool CanMove;
    public bool Invincible = false;
    public Transform SpawnPoint;
    public Transform SpawnPointFinal;
    public static UnityAction OnPlayerReset;
    public GameObject AbilityTrigger;

    private void OnEnable()
    {
        GameManager.OnRestart += ResetPlayer;
    }
    private void OnDisable()
    {
        GameManager.OnRestart -= ResetPlayer;
    }

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
        CanMove = false;
    }
    private void Update()
    {
        if (!CanMove) return;
        PlayerInput();
        MovePlayer();
        UIManager.instance?.ShowHint(CanDash);
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
            AudioManager.instance?.PlayPlayerMove();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _moveDir.x = 1;
            AudioManager.instance?.PlayPlayerMove();
        }

        if (Input.GetKey(KeyCode.W))
        {
            _moveDir.y = 1;
            AudioManager.instance?.PlayPlayerMove();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _moveDir.y = -1;
            AudioManager.instance?.PlayPlayerMove();
        }
        _moveDir.Normalize();

        if (Input.GetKeyDown(KeyCode.Space) && CanDash)
        {
            if (Time.time >= nextDash)
            {
                nextDash = Time.time + _coolDown;
                PlayDash();
                StartCoroutine(PlayInvincible());
            }
        }
    }

    IEnumerator PlayInvincible()
    {
        Invincible = true;
        _renderer.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.8f);
        _renderer.color = new Color(1f, 1f, 1f, 1f);
        Invincible = false;
    }
    private void PlayDash()
    {
        DashSpeed = 15f;
        _animator.SetTrigger("IsDashing");
        _dashDir = _moveDir;
        PlayerState = State.Dashing;
        AudioManager.instance?.PlayPlayerDash();
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
        CanMove = false;
        AudioManager.instance?.PlayPlayerDeath();
    }

    [ContextMenu("Reset")]
    public void ResetPlayer()
    {
        if (GameManager.instance.keyCount == GameManager.instance.maxKeyCount)
        {
            _rb.position = SpawnPointFinal.position;
        }
        else
        {
            _rb.position = SpawnPoint.position;
        }
        _animator.SetBool("IsDead", false);
        _rb.velocity = Vector2.zero;

        Debug.Log("Reset Player");
        OnPlayerReset?.Invoke();
        // wait for animation to finish
        StartCoroutine(EnableMovement());
    }
    IEnumerator EnableMovement()
    {
        float waitTime = 2f;
        if (GameManager.instance.keyCount == 0)
        {
            waitTime = 0;
        }
        yield return new WaitForSeconds(waitTime);
        CanMove = true;
    }
    public void EnableDash()
    {
        CanDash = true;
        UIManager.instance.ShowHint(CanDash);
        AudioManager.instance?.PlayUnlockDash();
        AbilityTrigger.SetActive(false);
    }
    public void DisableDash()
    {
        CanDash = false;
        UIManager.instance.ShowHint(CanDash);
        AbilityTrigger.SetActive(true);
    }
}
