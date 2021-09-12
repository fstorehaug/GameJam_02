using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public MapGeneration mapGenerator;
    public BaseEnemyScript enemyScript;

    private LiverCell spawner;

    private float spawnInterval= 2f;
    private float timeSinceLastSpawn;

    BaseEnemyScript spawnedEnemy;
    // Use this for initialization

    public void Start()
    {
        findAndSetSpawn();
    }

    public void OnSawnerDeath()
    {
        findAndSetSpawn();
    }


    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            spawnEnemy();
            timeSinceLastSpawn = 0;
        }
    }

    public void findAndSetSpawn()
    {
        List<LiverCell> cells = LivingCells.getLiveCells();
        spawner = cells[Random.Range(0,cells.Count)];
        spawner.onDeath += OnSawnerDeath;
    }

    public void spawnEnemy()
    {
        spawnedEnemy = Instantiate(enemyScript);
        spawnedEnemy.initializeEnemy(spawner);
    }
}
