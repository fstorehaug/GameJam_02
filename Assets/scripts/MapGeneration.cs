using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public LiverCell liverCell;
    private int XCells;
    private int YCells;
    public LiverCell[,] LiverCells;

    private void Awake()
    {
        InstantiateLiverCells();
        PlaceLiverCells();
        ShapeMap();
        PopulateNeighbours();
        Cleanup();
    }

    private void ReadFromSettings()
    {
        //Hardcoding max height and with here is bad
        XCells = Mathf.Clamp(SettingsScriptableObject.GetMapWidht(), 10, 100); 
        YCells = Mathf.Clamp(SettingsScriptableObject.GetMapHeight(), 10, 60);
        LiverCells = new LiverCell[XCells, YCells];
    }

    private void InstantiateLiverCells()
    {
        for (var i = 0; i < XCells; i++)
        {
            for (var j = 0; j < YCells; j++)
            {
                var cell = Instantiate(liverCell);
                cell.name =  i + ":" + j;
                LiverCells[i, j] = cell;
            }
        }
    }

    private void PlaceLiverCells()
    {
        for (var i = 0; i < XCells; i++)
        {
            for (var j = 0; j < YCells; j++)
            {
                LiverCells[i, j].transform.position = new Vector3(i, j - 0.5f * i);
            }
        }
    }

    private void ShapeMap()
    {
        foreach (var cell in LiverCells)
        {
            if (cell.transform.position.y < 0 || cell.transform.position.y > YCells / 2.0f)
            {
                cell.gameObject.SetActive(false);
            }
        }
    }

    private void PopulateNeighbours()
    {
        for (var i = 0; i < XCells; i++)
        {
            for (var j = 0; j < YCells; j++)
            {
                if (!LiverCells[i, j].isActiveAndEnabled)
                {
                    continue;
                }

                PopulateNeighbour(LiverCells[i, j], i + 0, j + 1);
                PopulateNeighbour(LiverCells[i, j], i + 1, j + 1);
                PopulateNeighbour(LiverCells[i, j], i + 1, j + 0);
                PopulateNeighbour(LiverCells[i, j], i + 0, j - 1);
                PopulateNeighbour(LiverCells[i, j], i - 1, j - 1);
                PopulateNeighbour(LiverCells[i, j], i - 1, j + 0);
            }
        }
    }

    private void PopulateNeighbour(LiverCell cell, int i, int j)
    {
        if (i > XCells - 1 || i < 0 || j > YCells - 1 || j < 0 || !LiverCells[i,j].isActiveAndEnabled)
        {
            return;
        }

        cell.neighbours.Add(LiverCells[i, j]);
    }

    public LiverCell[,] getMap()
    {
        return LiverCells;
    }

    public int getXCells()
    {
        return XCells;
    }

    public int getYCells()
    {
        return YCells;
    }

    public LiverCell getCellAt(int x, int y)
    {
        return LiverCells[x, y];
    }

    private void Cleanup()
    {
        foreach (var cell in LiverCells)
        {
            if (!cell.isActiveAndEnabled)
            {
                DestroyImmediate(cell.gameObject);
            }
        }
    }
}