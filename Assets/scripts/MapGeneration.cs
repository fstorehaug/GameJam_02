using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public LiverCell liverCell;
    private const int XCells = 11;
    private const int YCells = 11;
    public readonly LiverCell[,] LiverCells = new LiverCell[XCells, YCells];

    private void Start()
    {
        InstantiateLiverCells();
        PlaceLiverCells();
        PopulateNeighbours();
    }

    private void InstantiateLiverCells()
    {
        for (var i = 0; i < XCells; i++)
        {
            for (var j = 0; j < YCells; j++)
            {
                LiverCells[i, j] = GameObject.Instantiate(liverCell);
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

    private void PopulateNeighbours()
    {
        for (var i = 0; i < XCells; i++)
        {
            for (var j = 0; j < YCells; j++)
            {
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
        if (i > XCells || i < 0 || j > YCells || j < 0)
        {
            return;
        }
        cell.neighbours.Add(LiverCells[i, j]);
    }

    public LiverCell[,] getMap()
    {
        return _liverCells;
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
        return _liverCells[x, y];
    }
}