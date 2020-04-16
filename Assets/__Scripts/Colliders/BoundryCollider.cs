using UnityEngine;

public class BoundryCollider : MonoBehaviour
{
    // destroys any objects that touch collider
    // prevents build-up of game objects outside camera view
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<Bullet>();
        if(bullet)
        {
            Destroy(bullet.gameObject);
        }

        var plasma = collision.GetComponent<Plasma>();
        if(plasma)
        {
            Destroy(plasma.gameObject);
        }

        var torpedo = collision.GetComponent<Torpedo>();
        if(torpedo)
        {
            Destroy(torpedo.gameObject);
        }

        var enemy = collision.GetComponent<Enemy>();
        if(enemy)
        {
            Destroy(enemy.gameObject);
        }

        var asteroid = collision.GetComponent<Asteroid>();
        if(asteroid)
        {
            Destroy(asteroid.gameObject);
        }
    }
}
