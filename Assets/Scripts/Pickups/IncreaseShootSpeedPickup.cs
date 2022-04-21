using UnityEngine;

public class IncreaseShootSpeedPickup : Pickup
{
    [SerializeField] private float _increasedTimeBetweenProjectiles = 0.15f;
   
    public override void PickupEffect(PlayerController playerController, float speed, GameObject projectilePrefab, Transform defaultFirePoint)
    {
        playerController.ProjectileShooter.TimeBetweenProjectiles = _increasedTimeBetweenProjectiles;
    }
}
