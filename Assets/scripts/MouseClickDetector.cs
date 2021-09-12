using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickDetector : MonoBehaviour
{
    [SerializeField]
    private LiverCell liverCell;

    private void OnMouseDown()
    {
        liverCell.ToogleCell();
    }
}
