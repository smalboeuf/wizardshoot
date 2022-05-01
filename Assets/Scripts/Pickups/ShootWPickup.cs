using UnityEngine;

public class ShootWPickup : Pickup
{
    [SerializeField] private float _spaceBetweenProjectiles = 0.5f;
    public override void PickupEffect(PlayerController playerController, float speed, GameObject projectilePrefab, Transform defaultFirePoint)
    {
        playerController.ProjectileShooter.ResetProjectileShooterStats();
        playerController.ProjectileShooter.SetShootAction(ShootAction);
    }

    private void ShootAction(Vector2 direction, float speed, GameObject projectilePrefab, Transform defaultFirePoint)
    {
        ProjectileShooter.ShootProjectile(direction, speed, projectilePrefab, defaultFirePoint);

        if (direction.x == -1 || direction.x == 1)
        {
            ProjectileShooter.ShootProjectile(direction + new Vector2(0, _spaceBetweenProjectiles), speed, projectilePrefab, defaultFirePoint);
            ProjectileShooter.ShootProjectile(direction + new Vector2(0, -_spaceBetweenProjectiles), speed, projectilePrefab, defaultFirePoint);
        } else if (direction.y == 1 || direction.y == -1)
        {
            ProjectileShooter.ShootProjectile(direction + new Vector2(_spaceBetweenProjectiles, 0), speed, projectilePrefab, defaultFirePoint);
            ProjectileShooter.ShootProjectile(direction + new Vector2(-_spaceBetweenProjectiles, 0), speed, projectilePrefab, defaultFirePoint);
        } 
    }
}
