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

    private const int _maxMapWidht = 40;
    private const int _minMapWidth = 10;
    private const int _defaultMapWidht = 20;

    private const int _maxMapHeight = 40;
    private const int _minMapHeight = 10;
    private const int _defaultMapHeight = 20;

    public static void SetMapDimentions(int width, int height)
    {
        _mapHeight = Mathf.Clamp(height, _minMapHeight, _maxMapHeight);
        _mapWidth = Mathf.Clamp(width, _minMapWidth, _maxMapWidht);
    }

    public static void SetSpawnRate(int spawnrate)
    {
        _spawnRate = spawnrate;
    }

    public static int GetMapHeight()
    {
        if (_mapHeight < _maxMapHeight || _mapHeight > _minMapHeight)
            _mapHeight = _defaultMapHeight;

        return _mapHeight;
    }

    public static int GetMapWidht()
    {
        if (_mapWidth < _maxMapWidht || _mapWidth > _minMapWidth)
            _mapWidth = _defaultMapWidht;

        return _mapWidth;
    }

    public static int GetSpawnRate()
    {
        if (_spawnRate < 50 || _spawnRate > 500)
            _spawnRate = 100;

        return _spawnRate;
    }

}
