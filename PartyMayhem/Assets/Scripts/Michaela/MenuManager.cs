using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public Animator anim;
    public bool pressSpace = false;

    public AudioSource music;
    public float musicVolume;

    public void Start()
    {
        anim.SetBool("goToSettings", false);
        anim.SetBool("goToCredits", false);
        anim.SetBool("startScreen", true);

        musicVolume = 1f;
    }

    public void Update()
    {
        music.volume = musicVolume;
        if (pressSpace == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("startScreen", false);
                musicVolume = 0.3f;
                pressSpace = true;
            }
        }
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
        Debug.Log("Back");
    }

    public void OnCreditsButtonPress()
    {
        anim.SetBool("goToCredits", true);
        Debug.Log("Credits");
    }

    public void OnCreditsBackPress()
    {
        anim.SetBool("goToCredits", false);
        Debug.Log("Back");
    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
