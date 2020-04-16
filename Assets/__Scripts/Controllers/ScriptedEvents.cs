using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptedEvents : MonoBehaviour
{
    // == delegate types used for event methods ==
    public delegate void LaunchRaiderSortie(ScriptedEvents events);
    public delegate void LaunchVanguardSortie(ScriptedEvents events);
    public delegate void LaunchAsteroidSortie(ScriptedEvents events);
    public delegate void LaunchBoss(ScriptedEvents events);
    public delegate void SpawnAmmoPowerUp(ScriptedEvents events);
    public delegate void SpawnShieldPowerUp(ScriptedEvents events);
    public delegate void SpawnTorpedoPowerUp(ScriptedEvents events);
    public delegate void MissionDialogueEvent(ScriptedEvents events);
    // == static methods to be implemented by the event listner ==
    public static LaunchRaiderSortie RaiderSortieEvent;
    public static LaunchVanguardSortie VanguardSortieEvent;
    public static LaunchAsteroidSortie AsteroidSortieEvent;
    public static LaunchBoss BossEvent;
    public static SpawnAmmoPowerUp AmmoPowerUpEvent;
    public static SpawnShieldPowerUp ShieldPowerUpEvent;
    public static SpawnTorpedoPowerUp TorpedoPowerUpEvent;
    public static MissionDialogueEvent DialogueEvent;
    // == public variables ==
    [SerializeField] public float missionDuration = 60f;
    // == private variables ==
    private int currentScene;

    // == gets/sets == 
    public float getMissionDuration()
    {
        return missionDuration;
    } 
    
    void Start()
    {
        //find out what level the player is on
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 1)
        {
            this.LevelOneEvents();
        }
        else if(currentScene == 2)
        {
            this.LevelTwoEvents();
        }
        else if(currentScene == 3)
        {
            this.LevelThreeEvents();
        }
        else if(currentScene == 4)
        {
            this.LevelFourEvents();
        }
    }

    private void LevelOneEvents()
    {
        Invoke("PublishMissionDialogueEvent", 0.5f);
        Invoke("PublishAmmoPowerUpEvent", 20.5f);
        Invoke("PublishShieldPowerUpEvent", 40.5f);
        Invoke("PublishTorpedoPowerUpEvent", 60.5f);
    }

    private void LevelTwoEvents()
    {
        Invoke("PublishMissionDialogueEvent", 0.5f);
        Invoke("PublishRaiderSortieEvent", 20.5f);
        Invoke("PublishAmmoPowerUpEvent", 30.5f);
        Invoke("PublishRaiderSortieEvent", 40.5f);
        Invoke("PublishVanguardSortieEvent", 60.5f);
    }

    private void LevelThreeEvents()
    {
        Invoke("PublishMissionDialogueEvent", 0.5f);
        Invoke("PublishRaiderSortieEvent", 15.5f);
        Invoke("PublishRaiderSortieEvent", 20.5f);
        Invoke("PublishShieldPowerUpEvent", 30.5f);
        Invoke("PublishRaiderSortieEvent", 40.5f);
        Invoke("PublishRaiderSortieEvent", 50.5f);
        Invoke("PublishAsteroidSortieEvent", 60.5f);
        Invoke("PublishAsteroidSortieEvent", 65.5f);
    }

    private void LevelFourEvents()
    {
        Invoke("PublishMissionDialogueEvent", 0.5f);
        Invoke("PublishRaiderSortieEvent", 15.5f);
        Invoke("PublishShieldPowerUpEvent", 20.5f);
        Invoke("PublishRaiderSortieEvent", 30.5f);
        Invoke("PublishAmmoPowerUpEvent", 40.5f);
        Invoke("PublishRaiderSortieEvent", 50.5f);
        Invoke("PublishVanguardSortieEvent", 60.5f);
        Invoke("PublishTorpedoPowerUpEvent", 70.5f);
        Invoke("PublishShieldPowerUpEvent", 80.5f);
        Invoke("PublishBossEvent", 89.5f);
        Invoke("PublishTorpedoPowerUpEvent", 99.5f);
    }

    private void PublishRaiderSortieEvent()
    {
        if(RaiderSortieEvent != null)
        {
            RaiderSortieEvent(this);
        }
    }

    private void PublishVanguardSortieEvent()
    {
        if(VanguardSortieEvent != null)
        {
            VanguardSortieEvent(this);
        }
    }

    private void PublishAsteroidSortieEvent()
    {
        if(AsteroidSortieEvent != null)
        {
            AsteroidSortieEvent(this);
        }
    }

    private void PublishBossEvent()
    {
        if(BossEvent != null)
        {
            BossEvent(this);
        }
    }

    private void PublishAmmoPowerUpEvent()
    {
        if(AmmoPowerUpEvent != null)
        {
            AmmoPowerUpEvent(this);
        }
    }

    private void PublishShieldPowerUpEvent()
    {
        if(ShieldPowerUpEvent != null)
        {
            ShieldPowerUpEvent(this);
        }
    }

    private void PublishTorpedoPowerUpEvent()
    {
        if(TorpedoPowerUpEvent != null)
        {
            TorpedoPowerUpEvent(this);
        }
    }

    private void PublishMissionDialogueEvent()
    {
        if(DialogueEvent != null)
        {
            DialogueEvent(this);
        }
    }
}
