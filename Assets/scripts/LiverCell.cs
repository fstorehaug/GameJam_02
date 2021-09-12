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
        if (currentFat > 0)
        {
            currentFat -= damage;
            if(currentFat < 0)
            {
                currentHealth -= currentFat;
                currentFat = 0;
            }
        }
        if (currentHealth < 0)
        {
            isDead = true;
            isOpen = true;

            meshRenderer.material = _deadMat;
        }
    }

}