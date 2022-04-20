using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseShootSpeedPickup : Pickup
{
    [SerializeField] private float _increasedTimeBetweenProjectiles = 0.15f;
   
    public override void PickupEffect(PlayerController playerController)
    {
        playerController.CurrentPickup = this;
        playerController.CurrentPickupTimer = PickupBuffTimeInSeconds;
        playerController.ProjectileShooter.TimeBetweenProjectiles = _increasedTimeBetweenProjectiles;
    }
}
