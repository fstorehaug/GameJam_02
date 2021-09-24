using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public MapGeneration mapGenerator;
    public BaseEnemyScript enemyScript;
    [SerializeField]
    private Material spawnerMat;

    private LiverCell spawner;

    private float spawnInterval;
    private float timeSinceLastSpawn;

    BaseEnemyScript spawnedEnemy;
    // Use this for initialization

    public void Start()
    {
        ReadSpawnrateFromSettings();
        findAndSetSpawn();
    }

    public void OnSawnerDeath()
    {
        findAndSetSpawn();
    }

    private void ReadSpawnrateFromSettings()
    {
        //Hardcoding base spawnrate to 2 is bad
        spawnInterval = 2f / (SettingsScriptableObject.GetSpawnRate() / 100);
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
        spawner.meshRenderer.material = spawnerMat;
    }

    public void spawnEnemy()
    {
        spawnedEnemy = Instantiate(enemyScript);
        spawnedEnemy.initializeEnemy(spawner);
    }
}
