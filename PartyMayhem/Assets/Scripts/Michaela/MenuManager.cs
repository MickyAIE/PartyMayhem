using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Animator anim;

    public AudioMixer audioMixer;
    public AudioSource music;
    public AudioSource click;

    public Slider musicSlider;
    public Slider sfxSlider;

    public Text modeText;


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

    [HideInInspector]
    public bool boardMode;
    [HideInInspector]
    public bool tournamentMode;
    [HideInInspector]
    public bool freeplayMode;

    public Text minigameName;
    public bool selectedMissile = false;
    public bool selectedDodgeball = false;

    public Image minigamePreview;
    public Sprite missilePreview;
    public Sprite dodgeballPreview;
    

    public enum Mode
    {
        Board,
        Tournament,
        Freeplay,
        NotChosen
    };

    private Mode mode;

    public GameObject boardModeInfo;
    public GameObject tournamentModeInfo;
    public GameObject freeplayModeInfo;

    public GameObject hoverGuideText;


    Resolution[] resolutions;
    public Dropdown resolutionDropdown;


    public GameObject missileInfo;
    public GameObject dodgeballInfo;

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

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(false);

        musicSlider.value = -15f;
        sfxSlider.value = -15f;


        boardMode = false;
        tournamentMode = false;
        freeplayMode = false;

        mode = Mode.NotChosen;


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
                currentResolutionIndex = i;
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
            if (Input.anyKeyDown)
            {
                anim.SetBool("startScreen", false);
                pressSpace = true;
            }
        }


        if((boardMode == false && tournamentMode == false) && freeplayMode == false)
        {
            mode = Mode.NotChosen;
        }

        if(boardMode == true && (tournamentMode == false && freeplayMode == false))
        {
            mode = Mode.Board;
        }

        if (tournamentMode == true && (boardMode == false && freeplayMode == false))
        {
            mode = Mode.Tournament;
        }

        if (freeplayMode == true && (tournamentMode == false && boardMode == false))
        {
            mode = Mode.Freeplay;
        }


        if ((hoverBoard == false && hoverTournament == false) && hoverFreeplay == false)
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


        switch (mode)
        {
            case Mode.Board:

                break;

            case Mode.Tournament:

                break;

            case Mode.Freeplay:

                break;

            case Mode.NotChosen:

                break;
        }

        modeText.text = ("Mode: " + mode.ToString());
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
        //Debug.Log("Start");
    }

    public void OnModesBackPress()
    {
        click.Play();
        anim.SetBool("goToModes", false);
        //Debug.Log("Back");
    }

    public void OnSettingsButtonPress()
    {
        click.Play();
        anim.SetBool("goToSettings", true);
       // Debug.Log("Settings");
    }

    public void OnSettingsBackPress()
    {
        click.Play();
        anim.SetBool("goToSettings", false);
        //Debug.Log("Back");
    }

    public void OnCreditsButtonPress()
    {
        click.Play();
        anim.SetBool("goToCredits", true);
        //Debug.Log("Credits");
    }

    public void OnCreditsBackPress()
    {
        click.Play();
        anim.SetBool("goToCredits", false);
        //Debug.Log("Back");
    }

    public void OnQuitButtonPress()
    {
        click.Play();
        Application.Quit();
        //Debug.Log("Quit");
    }

    public void OnBoardModeSelected()
    {
        click.Play();
        anim.SetBool("goToMinigames", true);

        boardMode = true;

        //Debug.Log("Minigame Select (board)");
    }

    public void OnTournamentModeSelected()
    {
        click.Play();
        anim.SetBool("goToMinigames", true);

        tournamentMode = true;

        //Debug.Log("Minigame Select (tournament)");
    }

    public void OnFreeplayModeSelected()
    {
        click.Play();
        anim.SetBool("goToMinigames", true);

        freeplayMode = true;

        //Debug.Log("Minigame Select (freeplay)");
    }

    public void OnBackToModes()
    {
        boardMode = false;
        tournamentMode = false;
        freeplayMode = false;
        click.Play();
        anim.SetBool("goToMinigames", false);
        //Debug.Log("Mode Select");
    }

    public void OnMinigameMissilePressed()
    {
        click.Play();

        selectedDodgeball = false;
        selectedMissile = true;

        minigameName.text = "Missile Madness";
        minigamePreview.sprite = missilePreview;

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(true);

        anim.SetBool("goToMinigameInfo", true);
    }

    public void OnMinigameDodgeballPressed()
    {
        click.Play();

        selectedDodgeball = true;
        selectedMissile = false;

        minigameName.text = "Dodgeball";
        minigamePreview.sprite = dodgeballPreview;

        dodgeballInfo.SetActive(true);
        missileInfo.SetActive(false);

        anim.SetBool("goToMinigameInfo", true);
    }

    public void OnBackToMinigames()
    {
        click.Play();
        anim.SetBool("goToMinigameInfo", false);
    }

    public void PlayMinigame()
    {
        if(selectedDodgeball == false && selectedMissile == true)
        {
            SceneManager.LoadScene("MissileMadness");
        }

        if (selectedDodgeball == true && selectedMissile == false)
        {
            SceneManager.LoadScene("DodgeballDojo");
        }
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
