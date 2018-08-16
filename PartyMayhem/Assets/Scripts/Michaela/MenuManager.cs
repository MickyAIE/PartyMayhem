using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour {

    public Animator anim;
    public bool pressSpace = false;

    public AudioSource music;
    public float musicVolume;
    public AudioSource click;

    public bool hoverBoard = false;
    public bool hoverTournament = false;
    public bool hoverFreeplay = false;

    public GameObject boardModeInfo;
    public GameObject tournamentModeInfo;
    public GameObject freeplayModeInfo;
    public GameObject hoverGuideText;

    public bool hoveringOverSomething = false;

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

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

    }

    public void OnStartButtonPress()
    {
        click.Play();
        anim.SetBool("goToModes", true);
        Debug.Log("Start");
    }

    public void OnModesBackPress()
    {
        click.Play();
        anim.SetBool("goToModes", false);
        Debug.Log("Back");
    }

    public void OnSettingsButtonPress()
    {
        click.Play();
        anim.SetBool("goToSettings", true);
        Debug.Log("Settings");
    }

    public void OnSettingsBackPress()
    {
        click.Play();
        anim.SetBool("goToSettings", false);
        Debug.Log("Back");
    }

    public void OnCreditsButtonPress()
    {
        click.Play();
        anim.SetBool("goToCredits", true);
        Debug.Log("Credits");
    }

    public void OnCreditsBackPress()
    {
        click.Play();
        anim.SetBool("goToCredits", false);
        Debug.Log("Back");
    }

    public void OnQuitButtonPress()
    {
        click.Play();
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
