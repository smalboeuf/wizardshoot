using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private float _speed = 10;
    private Rigidbody2D _rb2d;

    public ProjectileShooter ProjectileShooter;
    private SpriteRenderer _spriteRenderer;

    public Pickup CurrentPickup;
    public float CurrentPickupTimer = 0;
    public bool PickupActive = false;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _lives = 3;
    [SerializeField] private float _invincibilityAfterDeathInSeconds = 3;
    private float _invincibilityCounter = 0;
    [SerializeField] private bool _invincible = false;

    public bool CanMove = true;

    // Inputs
    private Vector3 _moveInputs;
    private Vector3 _shootInputs;

    private Animator _animator;

    [Header("Invincibility Animation")]
    [SerializeField] private float _minimumAlpha = 0.3f;
    [SerializeField] private float _maximumAlpha = 1f;
    [SerializeField] private float _cyclesPerSecond = 2.0f;
    private float _spriteAlpha;
    private bool _increasing = true;
    private Color _color;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ProjectileShooter = GetComponent<ProjectileShooter>();
        _spriteAlpha = _maximumAlpha;
        _color = _spriteRenderer.color;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMoveInputs();
        HandleShootInputs();
        TrackPickupTimer();
        HandleInvincibilityTimer();
        InvincibilityFlashAnimation();
        HandleAnimationStates();
        HandlePauseInput();
    }

    private void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameManager.Pause();
        }
    }

    private void HandleAnimationStates()
    {
        _animator.SetBool("DownRun", (_moveInputs.y < 0 && _shootInputs == Vector3.zero) || _shootInputs.y < 0);

        _animator.SetBool("UpRun", (_moveInputs.y > 0 && _shootInputs == Vector3.zero) || _shootInputs.y > 0);

        _animator.SetBool("LeftRun", (_moveInputs.x < 0 && _shootInputs == Vector3.zero) || _shootInputs.x < 0);

        _animator.SetBool("RightRun", (_moveInputs.x > 0 && _shootInputs == Vector3.zero) || _shootInputs.x > 0);
    }

    private void MoveCharacter() {
        if (CanMove)
        {
            _rb2d.MovePosition(transform.position + _moveInputs.normalized * _speed * Time.deltaTime);
        }
    }

    private void Shooting() {
        // When holding down arrows, shoot in that direction
        if (_shootInputs != Vector3.zero)
        {
            ProjectileShooter.Shoot(_shootInputs);
        }
    }

    private void RefreshPlayerStatsAndBuffs()
    {
        print("Refresh Player stats");
        CurrentPickup = null;
        PickupActive = false;
        ProjectileShooter.ResetProjectileShooterStats();
    }

    private void TrackPickupTimer()
    {
        if (CurrentPickupTimer > 0)
        {
            CurrentPickupTimer -= Time.deltaTime;
        }

        if (CurrentPickupTimer <= 0 && PickupActive == true)
        {
            RefreshPlayerStatsAndBuffs();
        }
    }

    public void Die()
    {
        if (!_invincible)
        {
            _lives--;

            if (_lives <= 0)
            {
                CanMove = false;
                _gameManager.GameOver();
                transform.position = _spawnPoint.position;
            }
            else
            {
                // Make player invincible
                _invincible = true;
                _invincibilityCounter = _invincibilityAfterDeathInSeconds;
                // Set position to middle
                transform.position = _spawnPoint.position;
            }
        }
    }

    private void HandleInvincibilityTimer()
    {
        if (_invincibilityCounter > 0)
        {
            _invincibilityCounter -= Time.deltaTime;
        }

        if (_invincibilityCounter <= 0)
        {
            _invincibilityCounter = 0;
            _invincible = false;
        }
    }

    private void InvincibilityFlashAnimation()
    {
        if (_invincible || _spriteAlpha < _maximumAlpha)
        {
            float t = Time.deltaTime;
            if (_spriteAlpha >= _maximumAlpha) _increasing = false;
            if (_spriteAlpha <= _minimumAlpha) _increasing = true;
            _spriteAlpha = _increasing ? _spriteAlpha += t * _cyclesPerSecond * 2 : _spriteAlpha -= t * _cyclesPerSecond;
            _color.a = _spriteAlpha;
            _spriteRenderer.color = _color;
        }
    }

    private void HandleMoveInputs()
    {
        _moveInputs = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _moveInputs.y = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _moveInputs.x = -1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _moveInputs.y = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveInputs.x = 1;
        }
    }

    private void HandleShootInputs()
    {
        _shootInputs = Vector3.zero;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _shootInputs.y = -1;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _shootInputs.y = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _shootInputs.x = -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _shootInputs.x = 1;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        Shooting();
    }
}
