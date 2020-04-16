using UnityEngine;

public class Powerup : MonoBehaviour
{
    // == delegate types used for event methods ==
    public delegate void PowerUpPicked(Powerup power);
    // == static methods to be implemented by the event listner ==
    public static PowerUpPicked PowerUpPickedEvent;

    // == collision triggers ==
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            CollectPowerup();
        }
    }

    private void CollectPowerup()
    {
        // make sound effect
        FindObjectOfType<AudioManager>().Play("Pickup");

        //publish event to system to give the player the powerup
        PublishPowerUpPickedEvent();

        //destroy game object
        Destroy(gameObject);
    }

    private void PublishPowerUpPickedEvent()
    {
        if(PowerUpPickedEvent != null)
        {
            PowerUpPickedEvent(this);
        }
    }

}
