using UnityEngine;

//require physics and circle collider
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    // == private variables ==
    [SerializeField] private GameObject explosionFX;
    private float explosionDuration = 1.0f;
    
    // == collision events ==
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var enemy = whatHitMe.GetComponent<Enemy>();
        var asteroid = whatHitMe.GetComponent<Asteroid>();

        if(enemy || asteroid)
        {
            //show explosion
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);

            //destroy explosion
            Destroy(explosion, explosionDuration);

            // destroy bullet
            Destroy(gameObject);
        }  
    }

    // == public methods ==
    public void setBulletEffects(string particle, string color)
    {
        //create empty particle effect
        GameObject p = null;
        // set path
        string path = particle;
        // load resource
        p = Resources.Load<GameObject>(path);
        // apply particle effects
        gameObject.GetComponent<Bullet>().explosionFX = p;

        // apply bullet color
        if(color.Equals("yellow"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

}
