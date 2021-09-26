using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LiverCell : MonoBehaviour
{
    public List<LiverCell> neighbours = new List<LiverCell>();

    [SerializeField] private Material openMat;

    [SerializeField] private Material closedMat;

    [SerializeField] private Material deadMat;

    [SerializeField] private Material unavailableMat;

    [SerializeField] public MeshRenderer meshRenderer;

    [SerializeField] private GameObject damageIndicatorPrefab;
    private GameObject _damageIndicator;

    [SerializeField] private GameObject fatIndicatorPrefab;
    private GameObject _fatIndicator;

    public bool isOpen;
    public bool isDead;
    public bool isSpawner;
    public bool isUnavailable;

    private int _damageTaken;
    private readonly int _fatHealingValue = 1;
    private readonly float _healingInterval = 1;
    private readonly int _health = 100;
    private readonly int _maxFat = 100;

    private float _timeSinceHeal;

    private int _currentFat;

    public UnityAction ONDeath;

    private void Awake()
    {
        _damageIndicator = Instantiate(damageIndicatorPrefab,transform);
        _fatIndicator = Instantiate(fatIndicatorPrefab,transform);
        LivingCells.RegisterLiveCell(this);
        UpdateIndicator(_fatIndicator, _currentFat, _maxFat);
        UpdateIndicator(_damageIndicator, _damageTaken, _health);
    }

    private void Update()
    {
        if (isDead) return;

        _timeSinceHeal += Time.deltaTime;
        if (_timeSinceHeal <= _healingInterval) return;

        _currentFat = Mathf.Clamp(_currentFat - _fatHealingValue, _damageTaken, _maxFat);
        _timeSinceHeal = 0;
        UpdateIndicator(_fatIndicator, _currentFat, _maxFat);
        UpdateIndicator(_damageIndicator, _damageTaken, _health);
    }

    private void OnDestroy()
    {
        LivingCells.RegisterDeadCell(this);
    }

    public void ToggleCell()
    {
        if (isDead || isSpawner || isUnavailable) return;
        isOpen = ToggleWouldInduceOpenLoop() ? isOpen : !isOpen;
        meshRenderer.material = isOpen ? openMat : closedMat;
        ToggleUnavailableCells();
    }

    private void ToggleUnavailableCells()
    {
        // Only need to toggle availability of alive and closed cells
        foreach (var neighbour in neighbours.Where(neighbour => !neighbour.isDead && !neighbour.isOpen))
            if (neighbour.ToggleWouldInduceOpenLoop())
            {
                neighbour.isUnavailable = true;
                neighbour.meshRenderer.material = unavailableMat;
            }
            else
            {
                neighbour.isUnavailable = false;
                neighbour.meshRenderer.material = closedMat;
            }
    }

    private bool ToggleWouldInduceOpenLoop()
    {
        var openNeighbours = neighbours.FindAll(n => n.isOpen);
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var openNeighbour in openNeighbours)
        {
            var openNeighboursNeighbours = openNeighbour.neighbours.FindAll(onn => onn.isOpen);
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (var openNeighboursNeighbour in openNeighboursNeighbours)
                if (openNeighbours.Contains(openNeighboursNeighbour))
                    return true;
        }

        return false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        _currentFat += damage;
        if (_currentFat > _maxFat)
        {
            _damageTaken += _currentFat - _maxFat;
            _currentFat = _maxFat;
        }

        UpdateIndicator(_fatIndicator, _currentFat, _maxFat);
        UpdateIndicator(_damageIndicator, _damageTaken, _health);

        if (_damageTaken < _health) return;
        isDead = true;
        isOpen = false;

        meshRenderer.material = deadMat;
        Destroy(_damageIndicator);
        Destroy(_fatIndicator);
        ONDeath?.Invoke();
        ToggleUnavailableCells();
    }

    private void UpdateIndicator(GameObject indicator, int currentValue, int maxValue)
    {
        var scaleFactor = 0.8f / maxValue * currentValue + 0.05f;
        indicator.transform.localScale = new Vector3(scaleFactor, scaleFactor, transform.localScale.z);
    }
}