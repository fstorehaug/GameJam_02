using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SettingsScriptableObject : ScriptableObject
{
    [SerializeField]
    private static int _mapWidth;

    [SerializeField]
    private static int _mapHeight;

    [SerializeField]
    private static int _spawnRate; 


    public static void SetMapDimentions(int width, int height)
    {
        _mapHeight = height;
        _mapWidth = width;
    }

    public static void SetSpawnRate(int spawnrate)
    {
        _spawnRate = spawnrate;
    }

    public static int GetMapHeight()
    {
        return _mapHeight;
    }

    public static int GetMapWidht()
    {
        return _mapWidth;
    }

    public static int GetSpawnRate()
    {
        return _spawnRate;
    }

}
