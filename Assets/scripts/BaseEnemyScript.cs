using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    public LiverCell previousCell;
    public LiverCell currentCell;
    public LiverCell nextCell;
    private MapGeneration _mapGeneration;
    private float speed = 1f;
    private int baseDamage = 30;

    public void initializeEnemy(LiverCell spawnCell)
    {
        previousCell = spawnCell;
        currentCell = spawnCell;
        nextCell = spawnCell;
        transform.position = nextCell.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - .2f);
    }

    private void Update()
    {
        if (nextCell == null)
        {
            return;
        }

        UpdatePosition();

        if (transform.position.x == nextCell.transform.position.x &&
            transform.position.y == nextCell.transform.position.y)
        {
            OnCellArrival();
            previousCell = currentCell;
            currentCell = nextCell;
            nextCell = findNextCell(currentCell);
        }
    }

    private void UpdatePosition()
    {
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 destination = new Vector2(nextCell.gameObject.transform.position.x,
            nextCell.gameObject.transform.position.y);
        Vector2 nextPosition = Vector2.MoveTowards(enemyPosition, destination, speed * Time.deltaTime);

        transform.position = nextPosition;
        transform.position = transform.position + new Vector3(0f, 0f, -0.2f);
    }

    private LiverCell findNextCell(LiverCell currentCell)
    {
        var potentialCells = new List<LiverCell>();
        foreach (var cell in currentCell.neighbours)
        {
            if (cell.isOpen && cell != previousCell)
            {
                potentialCells.Add(cell);
            }
        }

        if (potentialCells.Count == 0)
        {
            TerminateSelf();
            nextCell = null;
            return null;
        }

        return potentialCells[Random.Range(0, potentialCells.Count)];
    }

    protected virtual void OnCellArrival()
    {
        nextCell.TakeDamage(Mathf.RoundToInt(baseDamage/10f));
    }

    protected virtual void DealDamage(LiverCell cell)
    {
        var toDamage = new List<LiverCell>{cell};
        toDamage.AddRange(cell.neighbours);

        foreach (var cellToDamage in toDamage)
        {
            cellToDamage.TakeDamage(baseDamage);
        }
    }

    protected virtual void TerminateSelf()
    {
        DealDamage(nextCell);
        DestroySelf();
    }

    public void DestroySelf()
    {
        DestroyImmediate(gameObject);
    }
}