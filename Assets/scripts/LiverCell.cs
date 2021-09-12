using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiverCell : MonoBehaviour
{
    public List<LiverCell> neighbours = new List<LiverCell>();
    [SerializeField]
    private Material _openMat;
    [SerializeField]
    private Material _closedMat;
    [SerializeField]
    private Material _deadMat;
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private GameObject DamageIndicator;
    [SerializeField]
    public GameObject FatIndicator;

    public UnityAction onDeath;


    public bool isOpen = false;
    public bool isDead = false;

    private int DamageTaken = 0;
    private int Health = 100;

    private int currentFat = 0;
    private int maxFat = 100;

    private float TimeSinceHeal = 0f;
    private float HealingInterval = 1;
    private int FatHealingValue = 1;

    private void Awake()
    {
        LivingCells.RegisterLiveCell(this);
        UpdateIndicator(FatIndicator, currentFat, maxFat);
        UpdateIndicator(DamageIndicator, DamageTaken, Health);
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        TimeSinceHeal += Time.deltaTime;
        if (TimeSinceHeal > HealingInterval)
        {
            currentFat = Mathf.Clamp(currentFat - FatHealingValue, DamageTaken, maxFat);
            TimeSinceHeal = 0;
            UpdateIndicator(FatIndicator, currentFat, maxFat);
            UpdateIndicator(DamageIndicator, DamageTaken, Health);
        }
    }

    public void ToogleCell()
    {
        if (isDead)
        {
            return;
        }
        isOpen = !isOpen;
        if (isOpen)
        {
            meshRenderer.material = _openMat;
        } else
        {
            meshRenderer.material = _closedMat;
        }
    }

    public void takeDamage(int damage)
    {
        if (isDead)
        {
            return;
        } 

        currentFat += damage;
        if(currentFat > maxFat)
        {
            DamageTaken += currentFat- maxFat;
            currentFat = maxFat;
        }

        UpdateIndicator(FatIndicator, currentFat, maxFat);
        UpdateIndicator(DamageIndicator, DamageTaken, Health);

        
        if (DamageTaken >= Health)
        {
            isDead = true;
            isOpen = false;

            meshRenderer.material = _deadMat;
            Destroy(DamageIndicator);
            Destroy(FatIndicator);
            onDeath?.Invoke();

        }
    }

    private void OnDestroy()
    {
        LivingCells.RegisterDeadCell(this);
    }

    private void UpdateIndicator(GameObject indicator, int currentValue, int maxValue)
    {
        float scaleFactor = ((0.8f / maxValue) * currentValue) + 0.05f;
        indicator.transform.localScale = new Vector3(scaleFactor, scaleFactor, transform.localScale.z);
    }

}