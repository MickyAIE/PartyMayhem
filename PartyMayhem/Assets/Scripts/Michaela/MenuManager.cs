using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetBool("goDown", false);
    }

    public void OnStartButtonPress()
    {
        anim.SetBool("goDown", true);
        Debug.Log("Start");
    }

    public void OnSettingsButtonPress()
    {
        anim.SetBool("goDown", false);
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
