using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _spawnrate;
    [SerializeField]
    private TMP_InputField _mapWidth;
    [SerializeField]
    private TMP_InputField _mapHeight;

    [SerializeField]
    private UnityEngine.UI.Button StartButton;

    public void Start()
    {
        _spawnrate.text = SettingsScriptableObject.GetSpawnRate().ToString();
        _mapWidth.text = SettingsScriptableObject.GetMapWidht().ToString();
        _mapHeight.text = SettingsScriptableObject.GetMapHeight().ToString();
    }


    public void PlayButton()
    {
        int spawnrate;
        int mapWidht;
        int mapHeight;

        if (!ParceToInt(_spawnrate, out spawnrate))
        {
            return;
        }
        
        if (!ParceToInt(_mapWidth, out mapWidht))
        {
            return;
        }

        if (!ParceToInt(_mapHeight, out mapHeight))
        {
            return;
        }

        StartButton.enabled = false;

        SettingsScriptableObject.SetMapDimentions(mapWidht, mapHeight);
        SettingsScriptableObject.SetSpawnRate(spawnrate);

        StartGame();
    }

    private void StartGame()
    {
        //Consider changing this to use the name of the seene. 
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private bool ParceToInt(TMP_InputField toParce, out int value)
    {
        int output;

        if (!int.TryParse(toParce.text, out output))
        {
            toParce.text = "invalid";
            value = -1;
            return false;
        }
        value = output;
        return true; ;
    }

}
