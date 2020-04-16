using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // == delegate types used for event methods ==
    public delegate void AsteroidKilled(Asteroid asteroid);
    public delegate void PlayerDamaged(Asteroid asteroid);
    public delegate void AsteroidBulletDamaged(Asteroid asteroid);
    public delegate void AsteroidTorpedoDamaged(Asteroid asteroid);

    // == static methods to be implemented by the event listner ==
    public static AsteroidKilled AsteroidKilledEvent;
    public static PlayerDamaged PlayerDamagedEvent;
    public static AsteroidBulletDamaged AsteroidBulletDamagedEvent;
    public static AsteroidTorpedoDamaged AsteroidTorpedoDamagedEvent;

    // == private enemy variables == 
    public EnemyHealthBar healthBar;

    // == private enemy variables == 
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int currentHealth;
    [SerializeField] private int asteroidValue = 5;
    [SerializeField] private int asteroidDamage = 30;
    [SerializeField] private float asteroidSpeed = 1f;

    // == particle effects ==
    [SerializeField] private GameObject explosionFX;
    private float explosionDuration = 1.0f;

    // == gets/sets ==
    public int getAsteroidValue()
    {
        return asteroidValue;
    }

    public void setAsteroidValue(int value)
    {
        asteroidValue = value;
    }

    public int getAsteroidDamage()
    {
        return asteroidDamage;
    }

    public void setAsteroidDamage(int damage)
    {
        asteroidDamage = damage;
    }

    public int getAsteroidHealth()
    {
        return currentHealth;
    }

    public void setAsteroidHealth(int health){
        this.currentHealth = health;
    }

    public float getAsteroidSpeed()
    {
        return asteroidSpeed;
    }

    public void setAsteroidSpeed(float speed){
        this.asteroidSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        // reference to player and bullet
        var player = whatHitMe.GetComponent<Player>();
        var bullet = whatHitMe.GetComponent<Bullet>();
        var torpedo = whatHitMe.GetComponent<Torpedo>();

        if (player)
        {
            //damage player
            PublishPlayerDamagedEvent();

            //destroy asteroid
            Destroy(gameObject);
        }

        if (bullet)
        {
            // publish event
            PublishAsteroidBulletDamagedEvent();
        }

        if (torpedo)
        {
            // publish event
            PublishAsteroidTorpedoDamagedEvent();
        }
    }

    private void PublishAsteroidKilledEvent()
    {
        if (AsteroidKilledEvent != null)
        {
            AsteroidKilledEvent(this);
        }
    }

    private void PublishPlayerDamagedEvent()
    {
        if (PlayerDamagedEvent != null)
        {
            PlayerDamagedEvent(this);
        }
    }

    private void PublishAsteroidBulletDamagedEvent()
    {
        if (AsteroidBulletDamagedEvent != null)
        {
            AsteroidBulletDamagedEvent(this);
        }
    }

    private void PublishAsteroidTorpedoDamagedEvent()
    {
        if (AsteroidTorpedoDamagedEvent != null)
        {
            AsteroidTorpedoDamagedEvent(this);
        }
    }

    private void Awake() 
    {
        currentHealth = maxHealth;
        healthBar.setHealth(maxHealth);
    }

    private void Update(){

        // track player health
        if(currentHealth <= 0)
        {
            //enemy destroyed
            PublishAsteroidKilledEvent();
            //show explosion
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            //play sounds
            FindObjectOfType<AudioManager>().Play("SmallEnemyDeath");
            //destroy explosion
            Destroy(explosion, explosionDuration);
            //destroy game object
            Destroy(gameObject);
        }

        // update health bar
        healthBar.setHealth(currentHealth);
    }
}
