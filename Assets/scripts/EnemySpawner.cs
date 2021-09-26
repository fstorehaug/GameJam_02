using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
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

    private void OnSpawnerDeath()
    {
        spawner.isSpawner = false;
        findAndSetSpawn();
    }

    private void ReadSpawnrateFromSettings()
    {
        //Hardcoding base spawnrate to 2 is bad
        spawnInterval = 2f / (SettingsScriptableObject.GetSpawnRate() / 100);
    }

    // Update is called once per frame
    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            spawnEnemy();
            timeSinceLastSpawn = 0;
        }
    }

    private void findAndSetSpawn()
    {
        var cells = LivingCells.getLiveCells();
        do
        { // Pick a spawner at random until we get a live cell
            spawner = cells[Random.Range(0, cells.Count)];
        } while (spawner.isDead);
        spawner.onDeath += OnSpawnerDeath;
        spawner.isOpen = true;
        spawner.isSpawner = true;
        spawner.meshRenderer.material = spawnerMat;
    }

    public void spawnEnemy()
    {
        spawnedEnemy = Instantiate(enemyScript);
        spawnedEnemy.initializeEnemy(spawner);
    }
}
