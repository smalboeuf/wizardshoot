using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public Transform _firePoint;
    public GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed;

    [SerializeField] private float _timeBetweenProjectiles = 0.25f;
    private float _projectileShootCounter;

    private void Update()
    {
        if (_projectileShootCounter > 0)
        {
            _projectileShootCounter -= Time.deltaTime;
        }

        if (_projectileShootCounter <= 0)
        {
            _projectileShootCounter = 0;
        }
    }

    public void Shoot(Vector2 direction)
    {
        if (_projectileShootCounter <= 0)
        {
            GameObject projectileGameObject = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
            Projectile projectile = projectileGameObject.GetComponent<Projectile>();
            projectile.SetProjectile(direction, _projectileSpeed);

            // Reset counter for shooting
            _projectileShootCounter = _timeBetweenProjectiles;
        }
    }
}
