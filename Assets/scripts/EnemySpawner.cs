using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public MapGeneration mapGenerator;
    public BaseEnemyScript enemyScript; 
    private int spawnX;
    private int spawnY;
    private float spawnInterval;
    private float timeSinceLastSpawn;
    // Use this for initialization
    void Start()  
    {
        System.Random rng = new System.Random();
        findAndSetSpawn(mapGenerator.getXCells(), mapGenerator.getYCells());
        spawnEnemy();
        timeSinceLastSpawn = 0;
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

    public void findAndSetSpawn(int width, int height)
    {
        bool hasFoundSpawn = false;
        System.Random rng = new System.Random();
        while (!hasFoundSpawn)
        {
            spawnX = rng.Next(0, width);
            spawnY = rng.Next(0, height);
            if (mapGenerator.getCellAt(spawnX, spawnY) != null)
            {
                hasFoundSpawn = true;
            }
        }
    }

    public void spawnEnemy()
    {
        BaseEnemyScript spawnedEnemy = Instantiate(enemyScript);
        spawnedEnemy.initializeEnemy(mapGenerator.getCellAt(spawnX, spawnY));
    }
}
