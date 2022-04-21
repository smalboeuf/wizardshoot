using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject ProjectilePrefab;
    public float ProjectileSpeed;
    public float TimeBetweenProjectiles;

    [SerializeField] private float _baseTimeBetweenProjectiles = 0.25f;
    private float _projectileShootCounter;

    public delegate void ShootActionDelegate(Vector2 direction, float speed, GameObject projectilePrefab, Transform defaultFirePoint);
    private ShootActionDelegate ShootAction = delegate { };

    private void Start()
    {
        ShootAction = DefaultShoot;
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
            ShootAction(direction, ProjectileSpeed, ProjectilePrefab, FirePoint);

            // Reset counter for shooting
            _projectileShootCounter = TimeBetweenProjectiles;
        }
    }

    public void DefaultShoot(Vector2 direction, float speed, GameObject projectilePrefab, Transform defaultFirePoint)
    {
        GameObject projectileGameObject = Instantiate(ProjectilePrefab, FirePoint.position, FirePoint.rotation);
        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.SetProjectile(direction, speed);

        // Reset counter for shooting
        _projectileShootCounter = TimeBetweenProjectiles;
    }

    public void SetShootAction(ShootActionDelegate shootActionDelegate)
    {
        ShootAction = shootActionDelegate;
    }

    public void ResetProjectileShooterStats()
    {
        TimeBetweenProjectiles = _baseTimeBetweenProjectiles;
    }
}
