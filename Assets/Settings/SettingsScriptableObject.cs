using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SettingsScriptableObject : ScriptableObject
{
    [SerializeField]
    private int _mapHeight;
    [SerializeField]
    private int _mapWidth;
    [SerializeField]
    private int _spawnrate;
    
    private static int mapWidth;
    private static int mapHeight;
    private static int spawnRate;

    private const int MaxMapWidht = 40;
    private const int MinMapWidth = 10;
    private const int DefaultMapWidht = 20;

    private const int MaxMapHeight = 40;
    private const int MinMapHeight = 10;
    private const int DefaultMapHeight = 20;

    private void OnDisable()
    {
        _mapHeight = mapHeight;
        _mapWidth = mapWidth;
        _spawnrate = spawnRate;
    }

    private void OnEnable()
    {
        SetMapDimentions(_mapWidth, _mapHeight);
        SetSpawnRate(_spawnrate);
    }


    public static void SetMapDimentions(int width, int height)
    {
        mapHeight = Mathf.Clamp(height, MinMapHeight, MaxMapHeight);
        mapWidth = Mathf.Clamp(width, MinMapWidth, MaxMapWidht);
    }

    public static void SetSpawnRate(int spawnrate)
    {
        spawnRate = spawnrate;
    }

    public static int GetMapHeight()
    {
        if (mapHeight > MaxMapHeight || mapHeight < MinMapHeight)
            mapHeight = DefaultMapHeight;

        return mapHeight;
    }

    public static int GetMapWidht()
    {
        if (mapWidth > MaxMapWidht || mapWidth < MinMapWidth)
            mapWidth = DefaultMapWidht;

        return mapWidth;
    }

    public static int GetSpawnRate()
    {
        if (spawnRate < 50 || spawnRate > 500)
            spawnRate = 100;

        return spawnRate;
    }

}
