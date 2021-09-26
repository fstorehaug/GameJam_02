using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Start()
    {
        SetPosition(SettingsScriptableObject.GetMapWidht(), SettingsScriptableObject.GetMapHeight());    
    }
    private void SetPosition(float width, float height) 
    {
        transform.position = new Vector3((width / 2) - 0.5f, (height - width * 0.5f) / 2, - (Mathf.Max(width, height)*(Mathf.Atan(Camera.main.fieldOfView/360))));   
    }
}
