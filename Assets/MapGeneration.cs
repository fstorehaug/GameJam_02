using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public LiverCell liverCell;
    private const int XCells = 20;
    private const int YCells = 20;
    private readonly LiverCell[,] _liverCells = new LiverCell[XCells, YCells];

    private void Start()
    {
        for (var i = 0; i < XCells; i++)
        {
            for (var j = 0; j < YCells; j++)
            {
                _liverCells[i, j] = GameObject.Instantiate(liverCell);
            }
        }

        Place();
    }

    private void Place()
    {
        for (int i = 1; i < XCells - 1; i++)
        {
            for (int j = 1; j < YCells - 1; j++)
            {
                _liverCells[i, j].transform.position = new Vector3(i - XCells / 2, j - YCells / 2 - 0.5f * i);
            }
        }
    }

    private void PopulateNeighbours(int i, int j)
    {
        var neighbours = new LiverCell[6];   
        neighbours[0] = _liverCells[i + 0, j + 1];
        neighbours[1] = _liverCells[i + 1, j + 1];
        neighbours[2] = _liverCells[i + 1, j + 0];
        neighbours[3] = _liverCells[i + 0, j - 1];
        neighbours[4] = _liverCells[i - 1, j - 1];
        neighbours[5] = _liverCells[i - 1, j + 0];
    }

    private void PopulateNeighbour(int i, int j)
    {
        
    }
}