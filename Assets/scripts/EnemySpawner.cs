using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public MapGeneration mapGenerator;
    private int spawnX;
    private int spawnY;
    // Use this for initialization
    void Start()  
    {
        System.Random rng = new System.Random();
        bool foundSpawn = false;
        while (!foundSpawn)
        {
            spawnX = rng.Next(0, mapGenerator.getXCells());
            spawnY = rng.Next(0, mapGenerator.getYCells());
            if (mapGenerator.getCellAt(spawnX, spawnY) != null)
            {
                foundSpawn = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
