using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        SetPosition(SettingsScriptableObject.GetMapWidht(), SettingsScriptableObject.GetMapHeight());    
    }
    private void SetPosition(float widht, float height) 
    {
        transform.position = new Vector3((widht / 2) - 0.5f, (height - widht * 0.5f) / 2, - (Mathf.Max(widht, height)*(Mathf.Atan(Camera.main.fieldOfView/360))));   
    }
}
