using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _timeBeforeDestroyed = 10;

    private Vector3 _shootingDirection;
    private float _speed;

    public void SetProjectile(Vector3 shootingDirection, float speed)
    {
        _shootingDirection = shootingDirection;
        _speed = speed;
    }

    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private void Update()
    {
        transform.position += _shootingDirection.normalized * _speed * Time.deltaTime;
    }

    private IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(_timeBeforeDestroyed);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            print("Enemy hit");
            Destroy(gameObject);
            collision.GetComponent<Enemy>().Die();
        }
    }
}
