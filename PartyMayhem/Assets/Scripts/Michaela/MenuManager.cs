using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public Animator anim;
    public bool pressSpace = false;

    public AudioSource music;
    public float musicVolume;

    public GameObject _Main;
    public GameObject _Settings;
    public GameObject _Credits;
    public GameObject _Modes;

    public bool hoverBoard = false;
    public bool hoverTournament = false;
    public bool hoverFreeplay = false;

    public void Start()
    {
        anim.SetBool("goToSettings", false);
        anim.SetBool("goToCredits", false);
        anim.SetBool("goToModes", false);
        anim.SetBool("startScreen", true);

        musicVolume = 1f;

        hoverBoard = false;
        hoverTournament = false;
        hoverFreeplay = false;
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

        if(hoverBoard == true)
        {
            Debug.Log("Board");
        }

        if (hoverTournament == true)
        {
            Debug.Log("Tournament");
        }

        if (hoverFreeplay == true)
        {
            Debug.Log("Freeplay");
        }
    }

    public void OnStartButtonPress()
    {
        anim.SetBool("goToModes", true);
        Debug.Log("Start");
    }

    public void OnModesBackPress()
    {
        anim.SetBool("goToModes", false);
        Debug.Log("Back");
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

    public void HoverBoard()
    {
        hoverBoard = true;
    }

    public void NotHoverBoard()
    {
        hoverBoard = false;
    }

    public void HoverTournament()
    {
        hoverTournament = true;
    }

    public void NotHoverTournament()
    {
        hoverTournament = false;
    }

    public void HoverFreeplay()
    {
        hoverFreeplay = true;
    }

    public void NotHoverFreeplay()
    {
        hoverFreeplay = false;
    }
}
