using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    private LiverCell nextCell;
    private float speed = 0.1f;
    private int baseDamage = 5;

    private void Update()
    {
        if (nextCell == null)
        {
            return;
        }

        if (transform.position == nextCell.transform.position)
        {
            OnCellArival();
            findNextCell(nextCell);
        }

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position = (Vector3.MoveTowards(transform.position, nextCell.gameObject.transform.position, speed*Time.deltaTime));
    }

    private LiverCell findNextCell(LiverCell currentCell)
    {
        List<LiverCell> potentiallCells= new List<LiverCell>();
        foreach(LiverCell cell in currentCell.neighbours)
        {
            if (cell.isOpen)
            {
                potentiallCells.Add(cell);
            }
        }

        if (potentiallCells.Count == 0)
        {
            TerminateSelf();
            return null;
        }

        return potentiallCells[Mathf.FloorToInt( Random.Range(0, potentiallCells.Count))];
    }

    protected virtual void OnCellArival()
    {

    }

    protected virtual void DealDamage(LiverCell cell)
    {
        List<LiverCell> toDamage = new List<LiverCell>();
        toDamage.Add(cell);
        toDamage.AddRange(cell.neighbours);

        foreach(LiverCell cellToDamage in toDamage)
        {
            cellToDamage.takeDamage(baseDamage);
        }

    }

    protected virtual void TerminateSelf()
    {
        DealDamage(nextCell);
        DestroySelf();
    }

    public void DestroySelf()
    {
        GameObject.Destroy(this.gameObject);
    }
}
