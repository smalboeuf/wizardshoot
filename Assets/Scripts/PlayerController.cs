using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    private Rigidbody2D _rb2d;

    public ProjectileShooter ProjectileShooter;

    public Pickup CurrentPickup;
    public float CurrentPickupTimer = 0;
    public bool PickupActive = false;

    // Inputs
    private Vector3 _moveInputs;
    private Vector3 _shootInputs;


    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        ProjectileShooter = GetComponent<ProjectileShooter>();
    }

    void Update()
    {
        HandleMoveInputs();
        HandleShootInputs();
        UpdateAnimationAndMove();
        TrackPickupTimer();
    }

    public void Die() {
        print("Player died");
        // Play death animation

        // Save the current round score to the leaderboards to track highest rounds

        // Reset Game state
        // - Remove existing enemies
        // - Remove player and position him back at the center of the game
        // - Reset the round counter
    }

    private void UpdateAnimationAndMove() {
    }

    private void MoveCharacter() {
        _rb2d.MovePosition(transform.position + _moveInputs.normalized * _speed * Time.deltaTime);
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
