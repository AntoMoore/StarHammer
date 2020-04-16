using UnityEngine;

public class Torpedo : MonoBehaviour
{
    // == private variables ==
    [SerializeField] private GameObject explosionFX;
    private float explosionDuration = 1.0f;
    private float aoeRadius;
    private int aoeDamage;

    private void Start() 
    {
        aoeRadius = GameObject.FindObjectOfType<WeaponsController>().getTorpedoRadius();
        aoeDamage = GameObject.FindObjectOfType<WeaponsController>().getTorpedoAoeDamage();
    }
    
    // == collision events ==
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var enemy = whatHitMe.GetComponent<Enemy>();
        var asteroid = whatHitMe.GetComponent<Asteroid>();

        // asteroid 
        if(enemy || asteroid)
        {
            // Find all the colliders on the Enemies layer within the expRadius.
            Collider2D[] enemies = Physics2D.OverlapCircleAll (transform.position, aoeRadius, 1 << LayerMask.NameToLayer("EnemyLayer"));
            // For each collider...
            foreach(Collider2D en in enemies)
            {
                // Check if it has a rigidbody (since there is only one per enemy, on the parent).
                Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
                if(rb != null && rb.tag == "Hostile")
                {
                    // Find the Enemy script and apply damage.
                    GameObject obj = rb.transform.gameObject;

                    if(obj.GetComponent<Enemy>())
                    {
                        //find targets hp
                        int targetHp = obj.GetComponent<Enemy>().getEnemyHealth();
                        //apply damage
                        targetHp -= aoeDamage;
                        //set new health
                        obj.GetComponent<Enemy>().setEnemyHealth(targetHp);
                    }
                    else
                    {
                        //find targets hp
                        int targetHp = obj.GetComponent<Asteroid>().getAsteroidHealth();
                        //apply damage
                        targetHp -= aoeDamage;
                        //set new health
                        obj.GetComponent<Asteroid>().setAsteroidHealth(targetHp);
                    }
                }
            }
            //show explosion
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);

            //play sounds
            FindObjectOfType<AudioManager>().Play("LargeEnemyDeath");

            //destroy explosion
            Destroy(explosion, explosionDuration);

            // destroy torpedo
            Destroy(gameObject);
        }  
    }
}
