using System.Collections;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    // == private variables ==
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed = 10.0f;
    [SerializeField] private float rateOfFire = 0.25f;
    [SerializeField] private int bulletDamage = 10;

    [SerializeField] private Torpedo torpedoPrefab;
    [SerializeField] private float torpedoSpeed = 7.0f;
    [SerializeField] private int torpedoDamage = 1000;
    [SerializeField] private int torpedoAoeDamage = 20;
    [SerializeField] private float torpedoExplosionRadius = 5.0f;

    private Player player;
    private Coroutine firingCoroutine;
    private GameObject bulletParent;
    private GameObject gun;

    // == gets/sets ==
    public int getBulletDamage()
    {
        return bulletDamage;
    }

    public void setBulletDamage(int damage)
    {
        bulletDamage = damage;
    }

    public int getTorpedoDamage()
    {
        return torpedoDamage;
    }

    public void setTorpedoDamage(int damage)
    {
        torpedoDamage = damage;
    }

    public int getTorpedoAoeDamage()
    {
        return torpedoAoeDamage;
    }

    public void setTorpedoAoeDamage(int damage)
    {
        torpedoAoeDamage = damage;
    }

    public float getTorpedoRadius()
    {
        return torpedoExplosionRadius;
    }

    public void setTorpedoRadius(float aoe)
    {
        torpedoExplosionRadius = aoe;
    }

    // == private methods ==
    private void Start()
    {
        // reference to player object
        player = GameObject.Find("Player").GetComponent<Player>();

        // get players gun 
        gun = GameObject.Find("Gun");

        // set bullets to green (default)
        bulletPrefab.GetComponent<SpriteRenderer>().color = Color.green;

        // parent object for bullets (container)
        bulletParent = GameObject.Find("BulletParent");
        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }
    }

    //private methods
    void Update()
    {
        // start shooting
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // start coroutine to fire
            firingCoroutine = StartCoroutine(FireCoroutine());
        }

        // stop shooting
        if(Input.GetKeyUp(KeyCode.Space))
        {
            // stop coroutine to fire
            StopCoroutine(firingCoroutine);
        }

        // anti-matter powerup
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //check if player has ammo loaded
            if(player.getPlayerAmmo())
            {
                // use anti matter rounds
                LoadAntiMatter(true);

                // after set period of time reset ammo
                Invoke("ResetAmmo", 10f);  
            }
        }

        // hyperShields powerup
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            //check if player has shields loaded
            if(player.getPlayerShields())
            {
                // reset shields back to empty
                player.setPlayerShields(false);
                // use hyper shields
                ActivateShields();
                // after set period of time reset shields
                Invoke("ResetShields", 10f);  
            }
        }

        // torpedo powerup
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            //check if player has torpedo loaded
            if(player.getPlayerTorpedo())
            {
                // fire torpedo
                LaunchTorpedo();
            }
        }
    }

    // coroutine return a IEnumerator type
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            // create a bullet at the players gun position
            Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
            bullet.transform.position = gun.transform.position;

            // get rigidbody and apply speed to bullet
            Rigidbody2D rb1 = bullet.GetComponent<Rigidbody2D>();
            rb1.velocity = Vector2.up * bulletSpeed;

            //bullet sound
            if(bulletDamage == 10)
            {
                FindObjectOfType<AudioManager>().Play("PlayerLaser");
            }
            else if(bulletDamage == 30)
            {
                FindObjectOfType<AudioManager>().Play("PlayerAnti");
            }
            
            // sleep for a short time
            yield return new WaitForSeconds(rateOfFire); // pick a number
        }
    }

    private void LaunchTorpedo()
    {
        //instanciate bullet
        Torpedo torpedo = Instantiate(torpedoPrefab, bulletParent.transform);

        //give same position as player
        torpedo.transform.position = gun.transform.position;

        //give velocity
        Rigidbody2D rb2 = torpedo.GetComponent<Rigidbody2D>();

        //rbb.velocity = new Vector2(1, 0);
        rb2.velocity = Vector2.up * torpedoSpeed;

        //torpedo sound
        FindObjectOfType<AudioManager>().Play("Torpedo");

        // reset torpedos back to empty
        player.setPlayerTorpedo(false);
    }

    private void LoadAntiMatter(bool ammo)
    {
        if(ammo == true)
        {
            player.setPlayerAmmo(false);
            bulletDamage = 30;
            bulletPrefab.setBulletEffects("AntiMatterFX", "yellow");
            //play sounds
            FindObjectOfType<AudioManager>().Play("LoadAmmo");
        }
        else
        {
            bulletDamage = 10;
            bulletPrefab.setBulletEffects("LaserFX", "green");
        }
    }

    private void ResetAmmo()
    {
        this.LoadAntiMatter(false);
    }

    private void ActivateShields()
    {
        //shields sound
        FindObjectOfType<AudioManager>().Play("ShieldBoost");
        //make invincible
        player.setPlayerInvincible(true);
    }

    private void ResetShields()
    {
        // turn off invincible
        player.setPlayerInvincible(false);
    }
}
