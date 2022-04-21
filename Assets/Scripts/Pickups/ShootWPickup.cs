using UnityEngine;

public class ShootWPickup : Pickup
{
    public override void PickupEffect(PlayerController playerController, float speed, GameObject projectilePrefab, Transform defaultFirePoint)
    {
        playerController.ProjectileShooter.SetShootAction(ShootAction);
    }

    private void ShootAction(Vector2 direction, float speed, GameObject projectilePrefab, Transform defaultFirePoint)
    {
        GameObject projectileGameObject1 = Instantiate(projectilePrefab, defaultFirePoint.position, defaultFirePoint.rotation);
        Projectile projectile = projectileGameObject1.GetComponent<Projectile>();
        projectile.SetProjectile(direction, speed);

        if (direction.x == -1 || direction.x == 1)
        {
            GameObject projectileGameObject2 = Instantiate(projectilePrefab, defaultFirePoint.position, defaultFirePoint.rotation);
            Projectile projectile2 = projectileGameObject2.GetComponent<Projectile>();
            projectile2.SetProjectile(direction + new Vector2(0, 1), speed);

            GameObject projectileGameObject3 = Instantiate(projectilePrefab, defaultFirePoint.position, defaultFirePoint.rotation);
            Projectile projectile3 = projectileGameObject3.GetComponent<Projectile>();
            projectile3.SetProjectile(direction + new Vector2(0, -1), speed);
        } else if (direction.y == 1 || direction.y == -1)
        {
            GameObject projectileGameObject2 = Instantiate(projectilePrefab, defaultFirePoint.position, defaultFirePoint.rotation);
            Projectile projectile2 = projectileGameObject2.GetComponent<Projectile>();
            projectile2.SetProjectile(direction + new Vector2(1, 0), speed);

            GameObject projectileGameObject3 = Instantiate(projectilePrefab, defaultFirePoint.position, defaultFirePoint.rotation);
            Projectile projectile3 = projectileGameObject3.GetComponent<Projectile>();
            projectile3.SetProjectile(direction + new Vector2(-1, 0), speed);
        } 
    }
}
