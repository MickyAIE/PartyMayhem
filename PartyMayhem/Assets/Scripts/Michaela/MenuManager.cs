using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public Animator anim;

    public void Start()
    {
        anim.SetBool("goToSettings", false);
    }


    public void OnStartButtonPress()
    {
        Debug.Log("Start");
    }

    public void OnSettingsButtonPress()
    {
        anim.SetBool("goToSettings", true);
        Debug.Log("Settings");
    }

    public void OnSettingsBackPress()
    {
        anim.SetBool("goToSettings", false);
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
