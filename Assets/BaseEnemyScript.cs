using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    private LiverCell nextCell;
    private float speed;


    private void Update()
    {
        if (transform.position == nextCell.transform.position)
        {

        }
    }

    private LiverCell findNextCell(LiverCell currentCell)
    {
        List<LiverCell> potentiallCells= new List<LiverCell>();
        foreach(LiverCell cell in currentCell.neighbours)
        {
            if (cell.IsOpen)
            {
                potentiallCells.Add(cell);
            }
        }

        if (potentiallCells.Count == 0)
        {
            return null;
        }

        return potentiallCells[Mathf.FloorToInt( Random.Range(0, potentiallCells.Count))];
    }


}
