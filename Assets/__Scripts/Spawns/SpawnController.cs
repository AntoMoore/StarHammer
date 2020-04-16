using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class SpawnController : MonoBehaviour
{
    // == private fields ==
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField]private GameObject bossPrefab;
    [SerializeField]private Sortie sortiePrefab;
    [SerializeField]private Powerup powerUpPrefab;
    private GameObject enemyParent;
    private IList<SpawnPoint> spawnPoints;
    private Stack<SpawnPoint> spawnStack;
    private const string SPAWN_ENEMY_METHOD = "SpawnOneEnemy";
    [SerializeField] private float spawnDelay = 0.25f;
    [SerializeField] private float spawnInterval = 0.35f;
    
    // == private methods ==
    private void Start() 
    {
        enemyParent = GameObject.Find("EnemyParent");
        if(!enemyParent)
        {
            enemyParent = new GameObject("EnemyParent");
        }

        //get the spawn points
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        SpawnEnemyWaves();
    }

    public void StopEnemyWaves()
    {
        // stop spawning enemys
        CancelInvoke();
    }

    private void SpawnEnemyWaves()
    {
        //create stack of points
        spawnStack = ListUtils.createShuffleStack(spawnPoints);
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    }

    //stack version
    private void SpawnOneEnemy()
    {
        if(spawnStack.Count == 0)
        {
            spawnStack = ListUtils.createShuffleStack(spawnPoints);
        }
    
        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        var sp = spawnStack.Pop();
        enemy.transform.position = sp.transform.position;
    }

    public void SpawnSortie()
    {
        //set the enemy position to one of the spawn points
        var sortie = Instantiate(sortiePrefab, enemyParent.transform);
        // index of spawn point
        var index = 1;
        // actual element on list
        var sp = spawnPoints[index];   
        // set the new enemy position
        sortie.transform.position = sp.transform.position;
    }

    public void SpawnBoss()
    {
        //set the enemy position to one of the spawn points
        var sortie = Instantiate(bossPrefab, enemyParent.transform);
        // index of spawn point
        var index = 1;
        // actual element on list
        var sp = spawnPoints[index];   
        // set the new enemy position
        sortie.transform.position = sp.transform.position;
    }

    public void SpawnPowerUp()
    {
        //set the powerup position to one of the spawn points
        var power = Instantiate(powerUpPrefab);
        // index of spawn point
        var index = UnityEngine.Random.Range(0, spawnPoints.Count);
        // actual element on list
        var sp = spawnPoints[index];   
        // set the new enemy position
        power.transform.position = sp.transform.position;
    }
}
