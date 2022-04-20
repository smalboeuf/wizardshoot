using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    public Transform _firePoint;
    public GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed;

    [SerializeField] float _baseTimeBetweenProjectiles = 0.25f;
    public float TimeBetweenProjectiles;
    private float _projectileShootCounter;

    private void Start()
    {
        TimeBetweenProjectiles = _baseTimeBetweenProjectiles;
    }

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
            if (HasShootEffect())
            {
                // Do shoot effect
            } else
            {
                GameObject projectileGameObject = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
                Projectile projectile = projectileGameObject.GetComponent<Projectile>();
                projectile.SetProjectile(direction, _projectileSpeed);

                // Reset counter for shooting
                _projectileShootCounter = TimeBetweenProjectiles;
            }
            
        }
    }
    private bool HasShootEffect()
    {
        return playerController.CurrentPickupTimer > 0 && playerController.CurrentPickup != null && playerController.CurrentPickup.HasEffect;
    }

    public void ResetProjectileShooterStats()
    {
        TimeBetweenProjectiles = _baseTimeBetweenProjectiles;
    }
}
