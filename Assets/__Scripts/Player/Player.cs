using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    // == delegate types used for event methods ==
    public delegate void PlayerKilled(Player player);
    public delegate void PlayerVictory(Player player);
    public delegate void StopSpawners();
    public delegate void PlayerPlasmaDamaged(Plasma plasma);

    // == static methods to be implemented by the event listner ==
    public static PlayerKilled PlayerKilledEvent;
    public static PlayerVictory PlayerVictoryEvent;
    public static StopSpawners StopSpawnersEvent;
    public static PlayerPlasmaDamaged PlayerPlasmaDamagedEvent;

    // == public reference variables == 
    public HealthBar healthBar;
    public GameMenu gameMenu;

    // == private variables ==
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private int shieldRegen = 2;
    [SerializeField] private int score = 0;
    [SerializeField] private int level = 0;
    [SerializeField] private int totalScore;
    private float explosionDuration = 1.0f;
    private bool hasShields = false;
    private bool hasAmmo = false;
    private bool hasTorpedo = false;
    private bool isInvincible = false;
    private bool isAlive = true;
    private bool isTime = false;
    private bool isEnemys = false;
    private float playerTime = 0;
    private float missionTime;
    private int counter;
    
    // == gets/sets ==
    public int getPlayerHealth()
    {
        return currentHealth;
    }

    public void setPlayerHealth(int health){
        this.currentHealth = health;
    }

    public int getPlayerScore()
    {
        return score;
    } 

    public void setPlayerScore(int score)
    {
        this.score = score;
        this.UpdateScoreGUI();
    }

    public int getTotalScore()
    {
        return totalScore;
    } 

    public void setTotalScore(int totalScore)
    {
        this.totalScore = totalScore;
    }

    public int getPlayerLevel()
    {
        return level;
    } 

    public void setPlayerLevel(int level)
    {
        this.level = level;
    }

    public bool getPlayerShields()
    {
        return hasShields;
    } 

    public void setPlayerShields(bool shields)
    {
        hasShields = shields;
        if(hasShields)
        {
            ChangeSprite("Shields", "shieldLogo");
        }
        else
        {
            ChangeSprite("Shields", "IconX");
        }
    }

    public bool getPlayerAmmo()
    {
        return hasAmmo;
    }

    public void setPlayerAmmo(bool ammo)
    {
        hasAmmo = ammo;
        if(hasAmmo)
        {
            ChangeSprite("Ammo", "bulletIcon");
        }
        else
        {
            ChangeSprite("Ammo", "IconX");
        }
    }

    public bool getPlayerTorpedo()
    {
        return hasTorpedo;
    } 

    public void setPlayerTorpedo(bool torp)
    {
        hasTorpedo = torp;
        if(hasTorpedo)
        {
            ChangeSprite("Torpedos", "torpedoIcon");
        }
        else
        {
            ChangeSprite("Torpedos", "IconX");
        }
    }

    public bool getPlayerInvincible()
    {
        return isInvincible;
    } 

    public void setPlayerInvincible(bool invincible)
    {
        isInvincible = invincible;
        if(isInvincible)
        {
            ChangeSprite("Player", "bubble");
        }
        else
        {
            ChangeSprite("Player", "Fighter");
        }
    }

    public bool getPlayerAlive()
    {
        return isAlive;
    } 

    public void setPlayerAlive(bool alive)
    {
        this.isAlive = alive;
    }

    public void ChangeSprite(string ui, string str)
    {
        //create empty sprite
        Sprite s = null;
        // set path
        string path = str;
        // load resource
        s = Resources.Load<Sprite>(path);

        if(ui.Equals("Player"))
        {
            // apply player sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = s; 
        }
        else
        {
            //change HUD icon
            gameMenu.transform.Find("PlayerHUD").transform.Find(ui).GetComponent<Image>().sprite = s;
        }
    }

    // == private methods ==
    private void PublishPlayerKilledEvent()
    {
        if(PlayerKilledEvent != null)
        {
            PlayerKilledEvent(this);
        }
    }

    private void PublishPlayerVictoryEvent()
    {
        if(PlayerVictoryEvent != null)
        {
            PlayerVictoryEvent(this);
        }
    }

    private void PublishStopSpawnersEvent()
    {
        if(StopSpawnersEvent != null)
        {
            StopSpawnersEvent();
        }
    }

    private void PublishPlayerPlasmaDamagedEvent(Plasma plasma)
    {
        if(PlayerPlasmaDamagedEvent != null)
        {
            PlayerPlasmaDamagedEvent(plasma);
        }
    }

    private void UpdateScoreGUI()
    {
        scoreText.text = score.ToString();
    }

    private void UpdateTimeGUI()
    {
        timerText.text = ((int)playerTime).ToString();
    }

    private void ShieldRegen()
    {
        // if health is less than 100%
        if(this.currentHealth < maxHealth)
        {
            // incresse player shields by regen amount
            this.setPlayerHealth(this.getPlayerHealth() + shieldRegen);
        }

        // if greater than max 
        if(this.currentHealth > maxHealth)
        {
           // set shields back to 100%
           this.setPlayerHealth(maxHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        // reference to plasma
        var plasma = whatHitMe.GetComponent<Plasma>();

        if(plasma)
        {
            //damage player
            PublishPlayerPlasmaDamagedEvent(plasma);
        }
    }

    private void Awake() 
    {
        currentHealth = maxHealth;
        healthBar.setHealth(maxHealth);
    }

    private void Start() 
    {
        this.missionTime = GameObject.FindObjectOfType<ScriptedEvents>().getMissionDuration();
        this.level = SceneManager.GetActiveScene().buildIndex;
        if(level == 1)
        {
            // new game
            this.totalScore = 0;
        }
        else
        {
            // load game
            PlayerData data = SaveSystem.LoadPlayer();
            if(data != null)
            {
                this.totalScore = data.score;
            }
        }
    
        InvokeRepeating("UpdateTimeGUI", 1f, 1f);
        InvokeRepeating("ShieldRegen", 1f, 1f);
    }

    private void Update()
    {
        //update mission time
        playerTime += Time.deltaTime;
        if(playerTime >= missionTime && isTime == false)
        {
            //stop enemies spawning
            PublishStopSpawnersEvent();

            // mission duration passed
            isTime = true;
        }

        //check if enemies are left
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        counter = enemies.Length;
        if(isTime == true && counter == 0 && isEnemys == false)
        {
            //all enemys are dead
            isEnemys = true;
        }

        // check if time has passed and all remaining enemies are dead
        if(isTime == true && isEnemys == true)
        {
            //mission success
            PublishPlayerVictoryEvent();
        }

        // track player health
        if(currentHealth <= 0)
        {
            //player killed
            this.setPlayerAlive(false);
            // publish killed event
            PublishPlayerKilledEvent();
            // show explosion
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            //play sounds
            FindObjectOfType<AudioManager>().Play("PlayerExplosion");
            //destroy explosion
            Destroy(explosion, explosionDuration);

            //destroy game object
            Destroy(gameObject);
        }

        // update health bar
        healthBar.setHealth(currentHealth);
    }
}
