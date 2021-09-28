using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class BaseEnemyScript : MonoBehaviour
{
    public LiverCell previousCell;
    public LiverCell currentCell;
    public LiverCell nextCell;
    private readonly float _speed = 1f;
    private readonly int _baseDamage = 30;

    private Coroutine turnCoroutine;

    public void InitializeEnemy(LiverCell spawnCell)
    {
        previousCell = spawnCell;
        currentCell = spawnCell;
        nextCell = spawnCell;
        var position = nextCell.transform.position;
        transform.position = new Vector3(position.x, position.y, -0.08f);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void Update()
    {
        if (nextCell == null)
        {
            return;
        }

        UpdatePosition();

        if (!transform.position.x.Equals(nextCell.transform.position.x) ||
            !transform.position.y.Equals(nextCell.transform.position.y)) return;
        OnCellArrival();
        previousCell = currentCell;
        currentCell = nextCell;
        nextCell = FindNextCell();

        var lookRotation = (transform.position - nextCell.transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(
            new Vector3(lookRotation.x, lookRotation.y, 0f),
            Vector3.forward);

        StopAllCoroutines(); 
        turnCoroutine = StartCoroutine(RotateTowardsEnumerator(targetRotation));
    }

    private IEnumerator RotateTowardsEnumerator(Quaternion targetRotation)
    {
        const float stepAngle = 0.1f;

        for (var i = 0; i < 256; i++)
        {
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, stepAngle/Time.deltaTime);
            yield return 0;
        }

        yield return 0;
    }

    private void UpdatePosition()
    {
        var position = transform.position;
        var currentPosition = position;
        var destinationPosition = nextCell.transform.position;
        var enemyPosition = new Vector2(currentPosition.x, currentPosition.y);
        var destination = new Vector2(destinationPosition.x, destinationPosition.y);
        var nextPosition = Vector2.MoveTowards(enemyPosition, destination, _speed * Time.deltaTime);

        position = nextPosition;
        transform.position = position + new Vector3(0f, 0f, -0.08f);
    }

    private LiverCell FindNextCell()
    {
        var potentialCells = currentCell.neighbours.Where(cell => cell.isOpen && cell != previousCell).ToList();
        if (potentialCells.Count != 0) return potentialCells[Random.Range(0, potentialCells.Count)];
        
        // No cells to move to, so we explode.
        TerminateSelf();
        nextCell = null;
        return null;
    }

    private void OnCellArrival()
    {
        nextCell.TakeDamage(Mathf.RoundToInt(_baseDamage/10f));
    }

    private void DealDamage(LiverCell cell)
    {
        var toDamage = new List<LiverCell>{cell};
        toDamage.AddRange(cell.neighbours);

        foreach (var cellToDamage in toDamage)
        {
            cellToDamage.TakeDamage(_baseDamage);
        }
    }

    private void TerminateSelf()
    {
        DealDamage(nextCell);
        StopAllCoroutines();
        DestroySelf();
    }

    private void DestroySelf()
    {
        DestroyImmediate(gameObject);
    }
}