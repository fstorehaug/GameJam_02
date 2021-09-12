using System.Collections.Generic;
using UnityEngine;

public class LiverCell : MonoBehaviour
{
    public List<LiverCell> neighbours = new List<LiverCell>();

    public bool isOpen = false;
    public bool isDead = false;

    public int currentHealth = 100;
    public int maxHealth = 100;

    public int currentFat = 100;
    public int maxFat = 100;

    public void OnMouseDown()
    {
        if (isDead)
        {
            return;
        }
        isOpen = !isOpen;
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
        }
    }

}