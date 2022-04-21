using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public float PickupBuffTimeInSeconds = 30;
    public virtual void PickupEffect(PlayerController playerController, float speed, GameObject projectilePrefab, Transform defaultFirePoint) { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Trigger Pickup Effect
        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            PickupEffect(player, player.ProjectileShooter.ProjectileSpeed, player.ProjectileShooter.ProjectilePrefab, player.ProjectileShooter.FirePoint);
            player.PickupActive = true;
            player.CurrentPickup = this;
            player.CurrentPickupTimer = PickupBuffTimeInSeconds;
            Destroy(gameObject);
        }
    }
}
