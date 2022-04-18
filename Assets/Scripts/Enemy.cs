using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    private GameObject _player;


    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed *Time.deltaTime);
    }


    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("Player died");
            collision.GetComponent<PlayerController>().Die();
        }
    }
}
