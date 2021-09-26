using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField spawnRate;
    [SerializeField]
    private TMP_InputField mapWidth;
    [SerializeField]
    private TMP_InputField mapHeight;

    [SerializeField]
    private UnityEngine.UI.Button startButton;

    public void Start()
    {
        spawnRate.text = SettingsScriptableObject.GetSpawnRate().ToString();
        mapWidth.text = SettingsScriptableObject.GetMapWidht().ToString();
        mapHeight.text = SettingsScriptableObject.GetMapHeight().ToString();
    }


    public void PlayButton()
    {
        if (!ParseToInt(spawnRate, out var spawnRateOut))
        {
            return;
        }
        
        if (!ParseToInt(mapWidth, out var mapWidthOut))
        {
            return;
        }

        if (!ParseToInt(mapHeight, out var mapHeightOut))
        {
            return;
        }

        startButton.enabled = false;

        SettingsScriptableObject.SetMapDimentions(mapWidthOut, mapHeightOut);
        SettingsScriptableObject.SetSpawnRate(spawnRateOut);

        StartGame();
    }

    private static void StartGame()
    {
        //Consider changing this to use the name of the scene. 
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private static bool ParseToInt(TMP_InputField toParse, out int value)
    {
        if (!int.TryParse(toParse.text, out var output))
        {
            toParse.text = "invalid";
            value = -1;
            return false;
        }
        value = output;
        return true; ;
    }

}
