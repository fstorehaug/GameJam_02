using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapgen : MonoBehaviour
{
    public LiverCelll liverCell;
    private LiverCelll liverCellScript;

    private void Start()
    {

        liverCellScript = GameObject.Instantiate(liverCell);
        

    }

}
