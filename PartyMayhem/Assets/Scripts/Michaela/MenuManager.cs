using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public Animator anim;
    public bool pressSpace = false;

    public AudioSource music;
    public float musicVolume;

    /*
    public GameObject _Main;
    public GameObject _Settings;
    public GameObject _Credits;
    public GameObject _Modes;*/

    public bool hoverBoard = false;
    public bool hoverTournament = false;
    public bool hoverFreeplay = false;

    public GameObject boardModeInfo;
    public GameObject tournamentModeInfo;
    public GameObject freeplayModeInfo;
    public GameObject hoverGuideText;

    public bool hoveringOverSomething = false;

    public void Awake()
    {
        anim.SetBool("goToSettings", false);
        anim.SetBool("goToCredits", false);
        anim.SetBool("goToModes", false);
        anim.SetBool("startScreen", true);

        musicVolume = 1f;

        hoverBoard = false;
        hoverTournament = false;
        hoverFreeplay = false;

        boardModeInfo.SetActive(false);
        tournamentModeInfo.SetActive(false);
        freeplayModeInfo.SetActive(false);
        hoverGuideText.SetActive(false);
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

        if((hoverBoard == false && hoverTournament == false) && hoverFreeplay == false)
        {
            hoverGuideText.SetActive(true);
        }
        else
        {
            hoverGuideText.SetActive(false);
        }

        /*
        if (hoveringOverSomething == false)
        {
            Debug.Log("NOT HOVERING");
            hoverGuideText.SetActive(true);
        }
        else
        {
            Debug.Log("HOVERING");
            hoverGuideText.SetActive(false);
        }*/


        if(hoverBoard == true && (hoverTournament == false && hoverFreeplay == false))
        {
            boardModeInfo.SetActive(true);
        }
        else if (hoverBoard == false)
        {
            boardModeInfo.SetActive(false);
        }

        if (hoverTournament == true && (hoverBoard == false && hoverFreeplay == false))
        {
            tournamentModeInfo.SetActive(true);
        }
        else if(hoverTournament == false)
        {
            tournamentModeInfo.SetActive(false);
        }

        if (hoverFreeplay == true && (hoverBoard == false && hoverTournament == false))
        {
            freeplayModeInfo.SetActive(true);
        }
        else if (hoverFreeplay == false)
        {
            freeplayModeInfo.SetActive(false);
        }

       /* if (hoverBoard == true)
        {
            Debug.Log("Board");
            hoveringOverSomething = true;
            boardModeInfo.SetActive(true);
        }
        else
        {
            Debug.Log("NOT Board");
            hoveringOverSomething = false;
            boardModeInfo.SetActive(false);
        }

        if (hoverTournament == true)
        {
            Debug.Log("Tournament");
            hoveringOverSomething = true;
            tournamentModeInfo.SetActive(true);
        }
        else
        {
            Debug.Log("NOT Tournament");
            hoveringOverSomething = false;
            tournamentModeInfo.SetActive(false);
        }

        if (hoverFreeplay == true)
        {
            Debug.Log("Freeplay");
            hoveringOverSomething = true;
            freeplayModeInfo.SetActive(true);
        }
        else
        {
            Debug.Log("NOT Freeplay");
            hoveringOverSomething = false;
            freeplayModeInfo.SetActive(false);
        }*/
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
