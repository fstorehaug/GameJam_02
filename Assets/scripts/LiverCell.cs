using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LiverCell : MonoBehaviour
{
    public List<LiverCell> neighbours = new List<LiverCell>();

    [SerializeField] private Material _openMat;

    [SerializeField] private Material _closedMat;

    [SerializeField] private Material _deadMat;

    [SerializeField] private Material _unavailableMat;

    [SerializeField] public MeshRenderer meshRenderer;

    [SerializeField] private GameObject DamageIndicator;

    [SerializeField] public GameObject FatIndicator;

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

    private int currentFat;

    public UnityAction onDeath;

    private void Awake()
    {
        LivingCells.RegisterLiveCell(this);
        UpdateIndicator(FatIndicator, currentFat, _maxFat);
        UpdateIndicator(DamageIndicator, _damageTaken, _health);
    }

    private void Update()
    {
        if (isDead) return;

        _timeSinceHeal += Time.deltaTime;
        if (_timeSinceHeal <= _healingInterval) return;

        currentFat = Mathf.Clamp(currentFat - _fatHealingValue, _damageTaken, _maxFat);
        _timeSinceHeal = 0;
        UpdateIndicator(FatIndicator, currentFat, _maxFat);
        UpdateIndicator(DamageIndicator, _damageTaken, _health);
    }

    private void OnDestroy()
    {
        LivingCells.RegisterDeadCell(this);
    }

    public void ToggleCell()
    {
        if (isDead || isSpawner || isUnavailable) return;
        isOpen = ToggleWouldInduceOpenLoop() ? isOpen : !isOpen;
        meshRenderer.material = isOpen ? _openMat : _closedMat;
        ToggleUnavailableCells();
    }

    private void ToggleUnavailableCells()
    {
        // Only need to toggle availability of alive and closed cells
        foreach (var neighbour in neighbours.Where(neighbour => !neighbour.isDead && !neighbour.isOpen))
            if (neighbour.ToggleWouldInduceOpenLoop())
            {
                neighbour.isUnavailable = true;
                neighbour.meshRenderer.material = _unavailableMat;
            }
            else
            {
                neighbour.isUnavailable = false;
                neighbour.meshRenderer.material = _closedMat;
            }
    }

    private bool ToggleWouldInduceOpenLoop()
    {
        var openNeighbours = neighbours.FindAll(n => n.isOpen);
        foreach (var openNeighbour in openNeighbours)
        {
            var openNeighboursNeighbours = openNeighbour.neighbours.FindAll(onn => onn.isOpen);
            foreach (var openNeighboursNeighbour in openNeighboursNeighbours)
                if (openNeighbours.Contains(openNeighboursNeighbour))
                    return true;
        }

        return false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentFat += damage;
        if (currentFat > _maxFat)
        {
            _damageTaken += currentFat - _maxFat;
            currentFat = _maxFat;
        }

        UpdateIndicator(FatIndicator, currentFat, _maxFat);
        UpdateIndicator(DamageIndicator, _damageTaken, _health);

        if (_damageTaken < _health) return;
        isDead = true;
        isOpen = false;

        meshRenderer.material = _deadMat;
        Destroy(DamageIndicator);
        Destroy(FatIndicator);
        onDeath?.Invoke();
        ToggleUnavailableCells();
    }

    private void UpdateIndicator(GameObject indicator, int currentValue, int maxValue)
    {
        var scaleFactor = 0.8f / maxValue * currentValue + 0.05f;
        indicator.transform.localScale = new Vector3(scaleFactor, scaleFactor, transform.localScale.z);
    }
}