using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _timeBeforeDestroyed = 5;

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
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            gameObject.SetActive(false);
            collision.GetComponent<Enemy>().Die();
        }

        if (collision.tag == "Projectile Bounds")
        {
            gameObject.SetActive(false);
        }
    }
}
