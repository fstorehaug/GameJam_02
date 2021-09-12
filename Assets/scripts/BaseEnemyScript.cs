using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    public LiverCell nextCell;
    private float speed = 1f;
    private int baseDamage = 30;

    public void initializeEnemy(LiverCell nextCell)
    {
        this.nextCell = nextCell;
        this.transform.position = nextCell.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - .2f);
    }

    private void Update()
    {
        if (nextCell == null)
        {
            return;
        }

        UpdatePosition();

        if (transform.position.x == nextCell.transform.position.x && transform.position.y == nextCell.transform.position.y)
        {
            OnCellArival();
            nextCell = findNextCell(nextCell);
        }
    }
    private void UpdatePosition()
    {
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 destination = new Vector2(nextCell.gameObject.transform.position.x, nextCell.gameObject.transform.position.y);
        Vector2 nextPosition = Vector2.MoveTowards(enemyPosition, destination, speed * Time.deltaTime);

        transform.position = nextPosition;
        transform.position = transform.position + new Vector3(0f, 0f, -0.2f);
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
            nextCell = null;
            return null;
        }

        return potentiallCells[Mathf.FloorToInt( Random.Range(0, potentiallCells.Count -1))];
    }

    protected virtual void OnCellArival()
    {
        nextCell.takeDamage(Mathf.RoundToInt(baseDamage/10f));
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
        GameObject.DestroyImmediate(this.gameObject);
    }
}
