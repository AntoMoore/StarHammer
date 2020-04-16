using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{   
    // == private variables ==
    private int playerScore;
    private int playerHealth;
    private int enemyHealth;
    private bool playerShielded;
    private float timeDelay = 2f;
    private int enemyKilled = 0;
    private int asteroidKilled = 0;
    private Player player;
    private GameMenu gameMenu;
    private SpawnController upper;
    private SpawnController middle;
    private SpawnController lower;
    private SpawnController left;
    private SpawnController right;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI killedText;

    void Start() 
    {
        gameMenu = GameObject.Find("GameUI").GetComponent<GameMenu>();
        upper = GameObject.Find("UpperSpawnPoints").GetComponent<SpawnController>();
        middle = GameObject.Find("MiddleSpawnPoints").GetComponent<SpawnController>();
        lower = GameObject.Find("LowerSpawnPoints").GetComponent<SpawnController>();
        left = GameObject.Find("LeftSpawnPoints").GetComponent<SpawnController>();
        right = GameObject.Find("RightSpawnPoints").GetComponent<SpawnController>();
        player = GameObject.Find("Player").GetComponent<Player>();
        playerScore = player.getPlayerScore();
        playerHealth = player.getPlayerHealth();
    }

    // == EVENTS ==
    private void OnEnable()
    {
        //killed events
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
        Asteroid.AsteroidKilledEvent += OnAsteroidKilledEvent;
        Player.PlayerKilledEvent += OnPlayerKilledEvent;

        //damaged events
        Enemy.PlayerDamagedEvent += OnPlayerDamagedEvent;
        Enemy.EnemyBulletDamagedEvent += OnEnemyBulletDamagedEvent;
        Enemy.EnemyTorpedoDamagedEvent += OnEnemyTorpedoDamagedEvent;
        Asteroid.PlayerDamagedEvent += OnPlayerDamagedEvent;
        Asteroid.AsteroidBulletDamagedEvent += OnAsteroidBulletDamagedEvent;
        Asteroid.AsteroidTorpedoDamagedEvent += OnAsteroidTorpedoDamagedEvent;
        Player.PlayerPlasmaDamagedEvent += OnPlayerPlasmaDamagedEvent;

        //powerup events
        Powerup.PowerUpPickedEvent += OnPowerUpPickedEvent;

        //victory events
        Player.StopSpawnersEvent += OnPlayerStopSpawnerEvent;
        Player.PlayerVictoryEvent += OnPlayerVictoryEvent;

        // scripted events
        ScriptedEvents.RaiderSortieEvent += OnRaiderSpawnEvent;
        ScriptedEvents.VanguardSortieEvent += OnVanguardSpawnEvent;
        ScriptedEvents.AsteroidSortieEvent += OnAsteroidSpawnEvent;
        ScriptedEvents.BossEvent += OnBossSpawnEvent;
        ScriptedEvents.DialogueEvent += OnStartDialogueEvent;
        ScriptedEvents.AmmoPowerUpEvent += OnAmmoPowerUpEvent;
        ScriptedEvents.ShieldPowerUpEvent += OnShieldPowerUpEvent;
        ScriptedEvents.TorpedoPowerUpEvent += OnTorpedoPowerUpEvent;

        // dialogue events
        DialogueManager.DialogueEvent += OnFinishDialogueEvent;
    }

    private void OnDisable()
    {
        //killed events
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
        Asteroid.AsteroidKilledEvent -= OnAsteroidKilledEvent;
        Player.PlayerKilledEvent -= OnPlayerKilledEvent;

        //damaged events
        Enemy.PlayerDamagedEvent -= OnPlayerDamagedEvent;
        Enemy.EnemyBulletDamagedEvent -= OnEnemyBulletDamagedEvent;
        Enemy.EnemyTorpedoDamagedEvent -= OnEnemyTorpedoDamagedEvent;
        Asteroid.PlayerDamagedEvent -= OnPlayerDamagedEvent;
        Asteroid.AsteroidBulletDamagedEvent -= OnAsteroidBulletDamagedEvent;
        Asteroid.AsteroidTorpedoDamagedEvent -= OnAsteroidTorpedoDamagedEvent;
        Player.PlayerPlasmaDamagedEvent -= OnPlayerPlasmaDamagedEvent;

        //powerup events
        Powerup.PowerUpPickedEvent -= OnPowerUpPickedEvent;

        //victory event
        Player.StopSpawnersEvent -= OnPlayerStopSpawnerEvent;
        Player.PlayerVictoryEvent -= OnPlayerVictoryEvent;

        // scripted events
        ScriptedEvents.RaiderSortieEvent -= OnRaiderSpawnEvent;
        ScriptedEvents.VanguardSortieEvent -= OnVanguardSpawnEvent;
        ScriptedEvents.AsteroidSortieEvent -= OnAsteroidSpawnEvent;
        ScriptedEvents.BossEvent -= OnBossSpawnEvent;
        ScriptedEvents.DialogueEvent -= OnStartDialogueEvent;
        ScriptedEvents.AmmoPowerUpEvent -= OnAmmoPowerUpEvent;
        ScriptedEvents.ShieldPowerUpEvent -= OnShieldPowerUpEvent;
        ScriptedEvents.TorpedoPowerUpEvent -= OnTorpedoPowerUpEvent;
        
        // dialogue events
        DialogueManager.DialogueEvent -= OnFinishDialogueEvent;
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        //add the score value for the enemy to the player score
        playerScore += enemy.getEnemyValue();
        player.setPlayerScore(playerScore);
        enemyKilled++;
    }

    private void OnAsteroidKilledEvent(Asteroid asteroid)
    {
        //add the score value for the asteroid to the player score
        playerScore += asteroid.getAsteroidValue();
        player.setPlayerScore(playerScore);
        asteroidKilled++;
    }

    private void OnPlayerKilledEvent(Player player)
    {
        Invoke("PlayerDestroyed", timeDelay);
    }

    private void OnPlayerDamagedEvent(Enemy enemy) //enemy damage
    {
        //check if player has shields active
        playerShielded = player.getPlayerInvincible(); 
        if(playerShielded == false  && player.getPlayerAlive() == true)
        {
            //damage sound
            FindObjectOfType<AudioManager>().Play("PlayerDamage");

            //apply damage to player based on enemy damage value
            playerHealth -= enemy.getEnemyDamage();
            player.setPlayerHealth(playerHealth);
        }   
    }

    private void OnEnemyBulletDamagedEvent(Enemy enemy) //enemy damage
    {
        if(player.getPlayerAlive() == true)
        {
            //apply damage to enemy based on weapon damage value
            enemyHealth = enemy.getEnemyHealth();
            enemyHealth -= player.GetComponent<WeaponsController>().getBulletDamage();
            enemy.setEnemyHealth(enemyHealth);
        }
    }

    private void OnEnemyTorpedoDamagedEvent(Enemy enemy) //enemy damage
    {
        if(player.getPlayerAlive() == true)
        {
            //apply damage to enemy based on weapon damage value
            enemyHealth = enemy.getEnemyHealth();
            enemyHealth -= player.GetComponent<WeaponsController>().getTorpedoDamage();
            enemy.setEnemyHealth(enemyHealth);
        }
    }

    private void OnAsteroidBulletDamagedEvent(Asteroid asteroid) //enemy damage
    {
        if(player.getPlayerAlive() == true)
        {
            //apply damage to asteroid based on weapon damage value
            enemyHealth = asteroid.getAsteroidHealth();
            enemyHealth -= player.GetComponent<WeaponsController>().getBulletDamage();
            asteroid.setAsteroidHealth(enemyHealth);
        }  
    }

    private void OnAsteroidTorpedoDamagedEvent(Asteroid asteroid)
    {
        if(player.getPlayerAlive() == true)
        {
            //apply damage to asteroid based on weapon damage value
            enemyHealth = asteroid.getAsteroidHealth();
            enemyHealth -= player.GetComponent<WeaponsController>().getTorpedoDamage();
            asteroid.setAsteroidHealth(enemyHealth);
        }
    }

    private void OnPlayerDamagedEvent(Asteroid asteroid) //asteroid damage
    {
        //check if player has shields active
        playerShielded = player.getPlayerInvincible(); 
        if(playerShielded == false && player.getPlayerAlive() == true)
        {
            //damage sound
            FindObjectOfType<AudioManager>().Play("PlayerDamage");

            //apply damage to player based on asteroid damage value
            playerHealth -= asteroid.getAsteroidDamage();
            player.setPlayerHealth(playerHealth);
        }
    }

    private void OnPlayerPlasmaDamagedEvent(Plasma plasma)
    {
        //check if player has shields active
        playerShielded = player.getPlayerInvincible(); 
        if(playerShielded == false && player.getPlayerAlive() == true)
        {
            //damage sound
            FindObjectOfType<AudioManager>().Play("PlayerDamage");

            // find plasma damage
            var damage = plasma.plasmaDamage; 

            //apply damage to player
            playerHealth -= damage;
            player.setPlayerHealth(playerHealth);
        }
        
    }

    private void OnPowerUpPickedEvent(Powerup power)
    {
        if(power.name.Equals("ShieldPower(Clone)"))
        {
            //change player shields
            player.setPlayerShields(true);
        }
        else if(power.name.Equals("AmmoPower(Clone)"))
        {
            //change player ammo
            player.setPlayerAmmo(true); 
        }
        else if(power.name.Equals("TorpedoPower(Clone)"))
        {
            //change player torpedo
            player.setPlayerTorpedo(true);
        }
    }

    private void OnAmmoPowerUpEvent(ScriptedEvents events)
    {
        // spawn ammo power up 
        lower.SpawnPowerUp();
    }

    private void OnShieldPowerUpEvent(ScriptedEvents events)
    {
        // spawn shield power up 
        middle.SpawnPowerUp();
    }

    private void OnTorpedoPowerUpEvent(ScriptedEvents events)
    {
        // spawn torpedo power up 
        upper.SpawnPowerUp();
    }

    private void OnPlayerStopSpawnerEvent()
    {
        // stop spawning hostiles
        upper.StopEnemyWaves();
        middle.StopEnemyWaves();
        lower.StopEnemyWaves();
        left.StopEnemyWaves();
        right.StopEnemyWaves();
    }

    private void OnPlayerVictoryEvent(Player player)
    {
        Invoke("PlayerWins", timeDelay);
    }

    private void OnRaiderSpawnEvent(ScriptedEvents events)
    {
        this.SpawnRaiderSortie();
    }

    private void OnVanguardSpawnEvent(ScriptedEvents events)
    {
        this.SpawnVanguardSortie();
    }

    private void OnAsteroidSpawnEvent(ScriptedEvents events)
    {
        this.SpawnAsteroidSortie();
    }

    private void OnBossSpawnEvent(ScriptedEvents events)
    {
        this.SpawnBossFight();
    }

    private void OnStartDialogueEvent(ScriptedEvents events)
    {
        gameMenu.PlayDialogue();
    }

    private void OnFinishDialogueEvent()
    {
        gameMenu.ResumeGamePlay();
    }

    private void PlayerDestroyed()
    {
        gameMenu.PlayerDefeat();
    }

    private void PlayerWins()
    {
        // save player data
        player.setTotalScore(player.getTotalScore() + playerScore);
        SaveSystem.SavePlayer(player);
        // set score text
        scoreText.text = playerScore.ToString();
        // set killed text
        killedText.text = (asteroidKilled + enemyKilled).ToString();
        // enable victory menu
        gameMenu.PlayerVictory();
    }

    private void SpawnRaiderSortie()
    {
        int num = UnityEngine.Random.Range(0,2);
        if(num == 0)
        {
            left.SpawnSortie();
        }
        else if(num == 1)
        {
            right.SpawnSortie();
        }
    }

    private void SpawnVanguardSortie()
    {
        lower.SpawnSortie();
    }

    private void SpawnAsteroidSortie()
    {
        middle.SpawnSortie();
    }

    private void SpawnBossFight()
    {
        middle.SpawnBoss();
    }
}
