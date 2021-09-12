using System.Collections.Generic;
using UnityEngine;

public class LiverCell : MonoBehaviour
{
    public List<LiverCell> neighbours = new List<LiverCell>();

    public bool isOpen;

    public int currentHealth = 100;
    public int maxHealth = 100;

    public int currentFat = 100;
    public int maxFat = 100;
}