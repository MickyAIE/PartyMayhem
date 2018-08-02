using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public void OnStartButtonPress()
    {
        Debug.Log("Start");
    }

    public void OnSettingsButtonPress()
    {
        Debug.Log("Settings");
    }

    public void OnCreditsButtonPress()
    {
        Debug.Log("Credits");
    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
