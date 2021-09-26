using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCells : MonoBehaviour
{
    private static readonly List<LiverCell> AliveCells = new List<LiverCell>();

    public static List<LiverCell> GETLiveCells()
    {
        return new List<LiverCell>(AliveCells);
    }

    public static void RegisterLiveCell(LiverCell cell)
    {
        AliveCells.Add(cell);
    }

    public static void RegisterDeadCell(LiverCell cell)
    {
        AliveCells.Remove(cell);
    }
}
