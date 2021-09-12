using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapgen : MonoBehaviour
{
    public LiverCell liverCell;
    private LiverCell liverCellScript;

    private void Start()
    {
        liverCellScript = GameObject.Instantiate(liverCell);
        

    }

}
