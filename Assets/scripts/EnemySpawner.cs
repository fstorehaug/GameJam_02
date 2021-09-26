using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public BaseEnemyScript enemyScript;
    [SerializeField]
    private Material spawnerMat;

    private LiverCell _spawner;

    private float _spawnInterval;
    private float _timeSinceLastSpawn;

    private BaseEnemyScript _spawnedEnemy;
    // Use this for initialization

    public void Start()
    {
        ReadSpawnRateFromSettings();
        FindAndSetSpawn();
    }

    private void OnSpawnerDeath()
    {
        _spawner.isSpawner = false;
        FindAndSetSpawn();
    }

    private void ReadSpawnRateFromSettings()
    {
        //Hard coding base spawn rate to 2 is bad
        _spawnInterval = 2f / (SettingsScriptableObject.GetSpawnRate() / 100f);
    }

    // Update is called once per frame
    private void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;
        if (!(_timeSinceLastSpawn >= _spawnInterval)) return;
        SpawnEnemy();
        _timeSinceLastSpawn = 0;
    }

    private void FindAndSetSpawn()
    {
        var cells = LivingCells.GETLiveCells();
        do
        { // Pick a spawner at random until we get a live cell
            _spawner = cells[Random.Range(0, cells.Count)];
        } while (_spawner.isDead);
        _spawner.ONDeath += OnSpawnerDeath;
        _spawner.isOpen = true;
        _spawner.isSpawner = true;
        _spawner.meshRenderer.material = spawnerMat;
    }

    private void SpawnEnemy()
    {
        _spawnedEnemy = Instantiate(enemyScript);
        _spawnedEnemy.InitializeEnemy(_spawner);
    }
}
