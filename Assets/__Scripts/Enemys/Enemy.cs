using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ===== public fields =======

    // == delegate types used for event methods ==
    public delegate void EnemyKilled(Enemy enemy);
    public delegate void PlayerDamaged(Enemy enemy);
    public delegate void EnemyBulletDamaged(Enemy enemy);
    public delegate void EnemyTorpedoDamaged(Enemy enemy);

    // == static methods to be implemented by the event listner ==
    public static EnemyKilled EnemyKilledEvent;
    public static PlayerDamaged PlayerDamagedEvent;
    public static EnemyBulletDamaged EnemyBulletDamagedEvent;
    public static EnemyTorpedoDamaged EnemyTorpedoDamagedEvent;

    // == public reference variables == 
    public EnemyHealthBar healthBar;

    // == private enemy variables == 
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int currentHealth;
    [SerializeField] private int enemyValue = 10;
    [SerializeField] private int enemyDamage = 10;
    // == particle effects ==
    [SerializeField] private GameObject explosionFX;
    private float explosionDuration = 1.0f;

    // == gets/sets ==
    public int getEnemyHealth()
    {
        return currentHealth;
    }

    public void setEnemyHealth(int health){
        this.currentHealth = health;
    }

    public int getEnemyValue()
    {
        return enemyValue;
    }

    public void setEnemyValue(int value){
        enemyValue = value;
    }

    public int getEnemyDamage()
    {
        return enemyDamage;
    }

    public void setEnemyDamage(int damage){
        enemyDamage = damage;
    }

    // == collision triggers ==
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        // reference to player and bullet
        var player = whatHitMe.GetComponent<Player>();
        var bullet = whatHitMe.GetComponent<Bullet>();
        var torpedo = whatHitMe.GetComponent<Torpedo>();

        if(player)
        {
            //damage player
            PublishPlayerDamagedEvent();

            //destroy enemy
            if(!gameObject.name.Equals("Mothership"))
            Destroy(gameObject);
        }

        if(bullet)
        {
            // Damage enemy depending on weapon damage
            PublishEnemyBulletDamagedEvent();
        }

        if(torpedo)
        {
            // Damage enemy depending on weapon damage
            PublishEnemyTorpedoDamagedEvent();
        }

        
    }

    private void PublishEnemyKilledEvent()
    {
        if(EnemyKilledEvent != null)
        {
            EnemyKilledEvent(this);
        }
    }

    private void PublishPlayerDamagedEvent()
    {
        if(PlayerDamagedEvent != null)
        {
            PlayerDamagedEvent(this);
        }
    }

    private void PublishEnemyBulletDamagedEvent()
    {
        if(PlayerDamagedEvent != null)
        {
            EnemyBulletDamagedEvent(this);
        }
    }

    private void PublishEnemyTorpedoDamagedEvent()
    {
        if(PlayerDamagedEvent != null)
        {
            EnemyTorpedoDamagedEvent(this);
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
            PublishEnemyKilledEvent();
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
