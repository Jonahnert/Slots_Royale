using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {

    public GameObject[] enemyToSpawn;
    public bool spawnEnemies = true;
    public Transform destination;

    public float spawnCooldown;
    private float currentSpawnCooldown;
    public int minEnemyIndex = 0;
    public int maxEnemyIndex = 9;
    
    void Awake()
    {
        /*
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        */
        //maxEnemyIndex += 1;
        //minEnemyIndex += 1;
        if (maxEnemyIndex > enemyToSpawn.Length)
        {
            maxEnemyIndex = enemyToSpawn.Length;
        }
        if(minEnemyIndex > enemyToSpawn.Length - 3)
        {
            minEnemyIndex = enemyToSpawn.Length - 3;
        }
    }
    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(minEnemyIndex,maxEnemyIndex);
        GameObject myEnemy = Instantiate(enemyToSpawn[spawnIndex], transform.position, Quaternion.identity) as GameObject;
        myEnemy.name = enemyToSpawn[spawnIndex].name;
        Unit myUnit = myEnemy.GetComponent<Unit>();
        if(myUnit)
        {
            myUnit.targetGoal = destination;
            myUnit.teamIndex = Unit.TeamIndex.Team_2;
        }
        currentSpawnCooldown = spawnCooldown * spawnIndex;
    }

    private void Update()
    {
        if(spawnEnemies)
        {
            if (currentSpawnCooldown <= 0)
            {
                SpawnEnemy();
            }
            else
            {
                currentSpawnCooldown -= Time.deltaTime;
            }
        }
    }
}
