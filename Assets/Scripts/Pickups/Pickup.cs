using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public float PickupBuffTimeInSeconds = 30;
    public bool HasEffect;
    public virtual void PickupEffect(PlayerController playerController) { }
    public virtual void ShootEffect() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Trigger Pickup Effect
        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            PickupEffect(player);
            player.PickupActive = true;
            Destroy(gameObject);
        }
    }
}
