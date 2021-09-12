using System.Collections.Generic;
using UnityEngine;

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

    public bool isOpen = false;
    public bool isDead = false;

    public int currentHealth = 100;
    public int maxHealth = 100;

    public int currentFat = 100;
    public int maxFat = 100;

    public void ToogleCell()
    {
        Debug.Log("cell Pressed");
        if (isDead)
        {
            return;
        }
        isOpen = toggleWouldInduceOpenLoop() ? isOpen : !isOpen;
        meshRenderer.material = isOpen ? _openMat : _closedMat;
    }

    private bool toggleWouldInduceOpenLoop()
    {
        var openNeighbours = neighbours.FindAll(n => n.isOpen);
        foreach (var openNeighbour in openNeighbours)
        {
            var openNeighboursNeighbours = openNeighbour.neighbours.FindAll(onn => onn.isOpen);
            foreach (var openNeighboursNeighbour in openNeighboursNeighbours)
            {
                if (openNeighbours.Contains(openNeighboursNeighbour))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void takeDamage(int damage)
    {
       
        currentFat -= damage;
        if(currentFat < 0)
        {
            currentHealth -= currentFat;
            currentFat = 0;
        }
        
        if (currentHealth < 0)
        {
            isDead = true;
            isOpen = true;

            meshRenderer.material = _deadMat;
        }
    }

}