using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Animator anim;

    public AudioMixer audioMixer;
    public AudioSource music;
    public AudioSource click;

    public Slider musicSlider;
    public Slider sfxSlider;


    [HideInInspector]
    public bool hoverBoard = false;
    [HideInInspector]
    public bool hoverTournament = false;
    [HideInInspector]
    public bool hoverFreeplay = false;

    [HideInInspector]
    public bool hoveringOverSomething = false;

    [HideInInspector]
    public bool pressSpace = false;


    public GameObject boardModeInfo;
    public GameObject tournamentModeInfo;
    public GameObject freeplayModeInfo;

    public GameObject hoverGuideText;


    Resolution[] resolutions;
    public Dropdown resolutionDropdown;


    public void Start()
    {
        anim.SetBool("goToSettings", false);
        anim.SetBool("goToCredits", false);
        anim.SetBool("goToModes", false);
        anim.SetBool("startScreen", true);


        hoverBoard = false;
        hoverTournament = false;
        hoverFreeplay = false;

        boardModeInfo.SetActive(false);
        tournamentModeInfo.SetActive(false);
        freeplayModeInfo.SetActive(false);
        hoverGuideText.SetActive(false);

        musicSlider.value = -15f;
        sfxSlider.value = -15f;


        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && 
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = 1;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void Update()
    {
        if (pressSpace == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("startScreen", false);
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


    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("Quality Level: " + qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Is fullscreen: " + isFullscreen);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Resolution: " + resolution);
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

    public void OnModeSelected()
    {
        click.Play();
        anim.SetBool("goToMinigames", true);
        Debug.Log("Minigame Select");
    }

    public void OnBackToModes()
    {
        click.Play();
        anim.SetBool("goToMinigames", false);
        Debug.Log("Mode Select");
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
