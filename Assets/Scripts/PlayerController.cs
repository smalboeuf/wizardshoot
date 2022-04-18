using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    private Rigidbody2D _rb2d;
    private ProjectileShooter _projectileShooter;

    // Inputs
    private Vector3 _moveInputs;
    private Vector3 _shootInputs;


    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _projectileShooter = GetComponent<ProjectileShooter>();
    }

    void Update()
    {
        HandleMoveInputs();
        HandleShootInputs();
        UpdateAnimationAndMove();
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

    public void Die() { 
    
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
            _projectileShooter.Shoot(_shootInputs);
        }
    }

    
}
