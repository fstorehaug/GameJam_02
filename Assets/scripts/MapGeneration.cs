using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public LiverCell liverCell;
    private int _xCells;
    private int _yCells;
    private LiverCell[,] _liverCells;

    private void Awake()
    {
        ReadFromSettings();
        InstantiateLiverCells();
        PlaceLiverCells();
        ShapeMap();
        PopulateNeighbours();
        Cleanup();
    }

    private void ReadFromSettings()
    {
        // Hard coding max height and width here is bad
        _xCells = Mathf.Clamp(SettingsScriptableObject.GetMapWidht(), 10, 100); 
        _yCells = Mathf.Clamp(SettingsScriptableObject.GetMapHeight(), 10, 60);
        _liverCells = new LiverCell[_xCells, _yCells];
    }

    private void InstantiateLiverCells()
    {
        for (var i = 0; i < _xCells; i++)
        {
            for (var j = 0; j < _yCells; j++)
            {
                var cell = Instantiate(liverCell);
                cell.name =  i + ":" + j;
                _liverCells[i, j] = cell;
            }
        }
    }

    private void PlaceLiverCells()
    {
        for (var i = 0; i < _xCells; i++)
        {
            for (var j = 0; j < _yCells; j++)
            {
                _liverCells[i, j].transform.position = new Vector3(i, j - 0.5f * i);
            }
        }
    }

    private void ShapeMap()
    {
        foreach (var cell in _liverCells)
        {
            if (cell.transform.position.y < 0 || cell.transform.position.y > _yCells / 2.0f)
            {
                cell.gameObject.SetActive(false);
            }
        }
    }

    private void PopulateNeighbours()
    {
        for (var i = 0; i < _xCells; i++)
        {
            for (var j = 0; j < _yCells; j++)
            {
                if (!_liverCells[i, j].isActiveAndEnabled)
                {
                    continue;
                }

                PopulateNeighbour(_liverCells[i, j], i + 0, j + 1);
                PopulateNeighbour(_liverCells[i, j], i + 1, j + 1);
                PopulateNeighbour(_liverCells[i, j], i + 1, j + 0);
                PopulateNeighbour(_liverCells[i, j], i + 0, j - 1);
                PopulateNeighbour(_liverCells[i, j], i - 1, j - 1);
                PopulateNeighbour(_liverCells[i, j], i - 1, j + 0);
            }
        }
    }

    private void PopulateNeighbour(LiverCell cell, int i, int j)
    {
        if (i > _xCells - 1 || i < 0 || j > _yCells - 1 || j < 0 || !_liverCells[i,j].isActiveAndEnabled)
        {
            return;
        }

        cell.neighbours.Add(_liverCells[i, j]);
    }

    private void Cleanup()
    {
        foreach (var cell in _liverCells)
        {
            if (!cell.isActiveAndEnabled)
            {
                DestroyImmediate(cell.gameObject);
            }
        }
    }
}