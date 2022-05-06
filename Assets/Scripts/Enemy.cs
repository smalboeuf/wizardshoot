using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    private GameObject _player;

    [SerializeField] private float _dropChance;
    [SerializeField] private List<GameObject> _drops;

    [SerializeField] private float _timeToMoveInSeconds;
    private Vector3 _originalPosition, _targetPosition;
    private bool _isMoving = false;

    [SerializeField] private float _spawnWalkTimeInSeconds = 1;
    private float _spawnWalkTimeCounter;

    public SpawnArea SpawnArea;

    private GameManager _gameManager;
    private Animator _animator;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Handle default walking upon spawn
        if (_spawnWalkTimeCounter > 0)
        {
            SpawnWalking();
            _spawnWalkTimeCounter -= Time.deltaTime;

        } else
        {
            Attack();
            _spawnWalkTimeCounter = 0;
        }
    }

    private void Attack()
    {
        Vector3 playerPosition = _player.transform.position;
        var difference = transform.position - playerPosition;
        float xDistance = Mathf.Abs(difference.x);
        float yDistance = Mathf.Abs(difference.y);

        if (!_isMoving)
        {
            if (xDistance > yDistance)
            {
                // If xDistance is farther, move on x axis
                if (difference.x < 0)
                {
                    StartCoroutine(MoveEnemy(Vector3.right));
                }
                else if (difference.x > 0)
                {
                    StartCoroutine(MoveEnemy(Vector3.left));
                }
            }
            else if (xDistance <= yDistance)
            {
                // If yDistance is farther, move on y axis
                if (difference.y < 0)
                {
                    StartCoroutine(MoveEnemy(Vector3.up));
                }
                else if (difference.y > 0)
                {
                    StartCoroutine(MoveEnemy(Vector3.down));
                }
            }
        }
    }

    public void SpawnEvents()
    {
        _spawnWalkTimeCounter = _spawnWalkTimeInSeconds;
    }

    public void Die()
    {
        // Check if item drops
        if (Random.Range(0f, 1f) <= _dropChance)
        {
            // Get random number for drops
            int randomNumber = Random.Range(0, _drops.Count);
            // Spawn drop based on number
            Instantiate(_drops[randomNumber], transform.position, Quaternion.identity);
            _gameManager.EnemiesKilledSinceLastDrop = 0;
        } else
        {
            _gameManager.EnemiesKilledSinceLastDrop++;

            if (_gameManager.MinimumEnemiesForADropKilled())
            {
                // Get random number for drops
                int randomNumber = Random.Range(0, _drops.Count);
                // Spawn drop based on number
                Instantiate(_drops[randomNumber], transform.position, Quaternion.identity);
                _gameManager.EnemiesKilledSinceLastDrop = 0;
            }
        }

        Destroy(gameObject);
    }

    private void SpawnWalking()
    {
        if (!_isMoving)
        {
            switch (SpawnArea)
            {
                case SpawnArea.Top:
                    StartCoroutine(MoveEnemy(Vector3.down));
                    break;
                case SpawnArea.Left:
                    StartCoroutine(MoveEnemy(Vector3.right));
                    break;
                case SpawnArea.Right:
                    StartCoroutine(MoveEnemy(Vector3.left));
                    break;
                case SpawnArea.Bottom:
                    StartCoroutine(MoveEnemy(Vector3.up));
                    break;
            }
        }
    }

    private IEnumerator MoveEnemy(Vector3 direction)
    {
        _animator.SetBool("DownRun", direction.y < 0);

        _animator.SetBool("UpRun", direction.y > 0);

        _isMoving = true;
        
        float elapsedTime = 0;

        _originalPosition = transform.position;
        _targetPosition = _originalPosition + direction;

        while (elapsedTime < _timeToMoveInSeconds)
        {
            transform.position = Vector3.Lerp(_originalPosition, _targetPosition, (elapsedTime / _timeToMoveInSeconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _targetPosition;
        _isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerController>().Die();
        }
    }
}
public enum SpawnArea
{
    Top,
    Bottom,
    Left,
    Right
}