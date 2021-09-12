using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public LiverCell liverCell;
    private const int XCells = 36;
    private const int YCells = 41;
    public readonly LiverCell[,] LiverCells = new LiverCell[XCells, YCells];

    private void Awake()
    {
        InstantiateLiverCells();
        PlaceLiverCells();
        ShapeMap();
        PopulateNeighbours();
        Cleanup();
    }

    private void InstantiateLiverCells()
    {
        for (var i = 0; i < XCells; i++)
        {
            for (var j = 0; j < YCells; j++)
            {
                LiverCells[i, j] = Instantiate(liverCell);
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