using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LiverCell : MonoBehaviour, GraphNode<LiverCell>
{
    public List<LiverCell> neighbours = new List<LiverCell>();
    [SerializeField]
    private Material _openMat;
    [SerializeField]
    private Material _closedMat;
    [SerializeField]
    private Material _deadMat;
    [SerializeField]
    public MeshRenderer meshRenderer;

    [SerializeField]
    private GameObject DamageIndicator;
    [SerializeField]
    public GameObject FatIndicator;

    public UnityAction onDeath;

    public bool isOpen = false;
    public bool isDead = false;
    public bool isSpawner = false;

    private int _damageTaken = 0;
    private int _health = 100;

    private int currentFat = 0;
    private int _maxFat = 100;

    private float _timeSinceHeal = 0f;
    private float _healingInterval = 1;
    private int _fatHealingValue = 1;

    private void Awake()
    {
        LivingCells.RegisterLiveCell(this);
        UpdateIndicator(FatIndicator, currentFat, _maxFat);
        UpdateIndicator(DamageIndicator, _damageTaken, _health);
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        _timeSinceHeal += Time.deltaTime;
        if (_timeSinceHeal > _healingInterval)
        {
            currentFat = Mathf.Clamp(currentFat - _fatHealingValue, _damageTaken, _maxFat);
            _timeSinceHeal = 0;
            UpdateIndicator(FatIndicator, currentFat, _maxFat);
            UpdateIndicator(DamageIndicator, _damageTaken, _health);
        }
    }

    public void ToggleCell()
    {
        if (isDead || isSpawner)
        {
            return;
        }
        isOpen = OpeningWouldInduceLoop() ? isOpen : !isOpen;
        meshRenderer.material = isOpen ? _openMat : _closedMat;
    }

    private bool OpeningWouldInduceLoop()
    {
        bool wasOpen = isOpen;
        isOpen = true;
        Graph<LiverCell> graph = new Graph<LiverCell>(new List<LiverCell> { this });
        bool containsCycle = graph.ContainsCycle();
        isOpen = wasOpen;
        return containsCycle;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        } 

        currentFat += damage;
        if(currentFat > _maxFat)
        {
            _damageTaken += currentFat- _maxFat;
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
    }

    private void OnDestroy()
    {
        LivingCells.RegisterDeadCell(this);
    }

    private void UpdateIndicator(GameObject indicator, int currentValue, int maxValue)
    {
        var scaleFactor = ((0.8f / maxValue) * currentValue) + 0.05f;
        indicator.transform.localScale = new Vector3(scaleFactor, scaleFactor, transform.localScale.z);
    }

    public List<LiverCell> GetNeighbours()
    {
        return neighbours.FindAll(node => node.isOpen);
    }
}