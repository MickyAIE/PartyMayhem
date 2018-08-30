using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class bools
{
    public bool hoverBoard = false;
    public bool hoverTournament = false;
    public bool hoverFreeplay = false;

    public bool hoveringOverSomething = false;

    public bool pressSpace = false;

    public bool boardMode;
    public bool tournamentMode;
    public bool freeplayMode;

    public bool selectedMissile = false;
    public bool selectedDodgeball = false;
    public bool selectedRacing = false;
    public bool selectedGeo = false;
    public bool selectedRhythm = false;
}

public class MenuManager : MonoBehaviour {

    public bools bools;

    public Animator anim;

    public AudioMixer audioMixer;
    public AudioSource music;
    public AudioSource click;

    public Slider musicSlider;
    public Slider sfxSlider;

    public Text modeText;
    public Text minigameName;

    public Image minigamePreview;
    public Sprite missilePreview;
    public Sprite dodgeballPreview;
    //public Sprite geoPreview;
    //public Sprite racingPreview;
    //public Sprite rhythmPreview;
    public Sprite noPreview;

    public Dropdown graphicsDropdown;
    public Dropdown resolutionDropdown;

    public GameObject boardModeInfo;
    public GameObject tournamentModeInfo;
    public GameObject freeplayModeInfo;

    public GameObject missileInfo;
    public GameObject dodgeballInfo;

    public GameObject hoverGuideText;

    Resolution[] resolutions;

    public enum Mode
    {
        Board,
        Tournament,
        Freeplay,
        NotChosen
    };
    private Mode mode;

    public void Start()
    {
        anim.SetBool("goToSettings", false);
        anim.SetBool("goToCredits", false);
        anim.SetBool("goToModes", false);
        anim.SetBool("startScreen", true);


        bools.hoverBoard = false;
        bools.hoverTournament = false;
        bools.hoverFreeplay = false;

        bools.boardMode = false;
        bools.tournamentMode = false;
        bools.freeplayMode = false;

        boardModeInfo.SetActive(false);
        tournamentModeInfo.SetActive(false);
        freeplayModeInfo.SetActive(false);
        hoverGuideText.SetActive(false);

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(false);

        graphicsDropdown.value = 4;

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
        if (bools.pressSpace == false)
        {
            if (Input.anyKeyDown)
            {
                anim.SetBool("startScreen", false);
                bools.pressSpace = true;
            }
        }

        Hover();

        if ((bools.boardMode == false && bools.tournamentMode == false) && bools.freeplayMode == false)
        {
            mode = Mode.NotChosen;
        }

        if(bools.boardMode == true && (bools.tournamentMode == false && bools.freeplayMode == false))
        {
            mode = Mode.Board;
        }

        if (bools.tournamentMode == true && (bools.boardMode == false && bools.freeplayMode == false))
        {
            mode = Mode.Tournament;
        }

        if (bools.freeplayMode == true && (bools.tournamentMode == false && bools.boardMode == false))
        {
            mode = Mode.Freeplay;
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

    public void Hover()
    {
        if ((bools.hoverBoard == false && bools.hoverTournament == false) && bools.hoverFreeplay == false)
        {
            hoverGuideText.SetActive(true);
        }
        else
        {
            hoverGuideText.SetActive(false);
        }


        if (bools.hoverBoard == true && (bools.hoverTournament == false && bools.hoverFreeplay == false))
        {
            boardModeInfo.SetActive(true);
        }
        else if (bools.hoverBoard == false)
        {
            boardModeInfo.SetActive(false);
        }

        if (bools.hoverTournament == true && (bools.hoverBoard == false && bools.hoverFreeplay == false))
        {
            tournamentModeInfo.SetActive(true);
        }
        else if (bools.hoverTournament == false)
        {
            tournamentModeInfo.SetActive(false);
        }

        if (bools.hoverFreeplay == true && (bools.hoverBoard == false && bools.hoverTournament == false))
        {
            freeplayModeInfo.SetActive(true);
        }
        else if (bools.hoverFreeplay == false)
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
    }

    public void OnModesBackPress()
    {
        click.Play();
        anim.SetBool("goToModes", false);
    }

    public void OnSettingsButtonPress()
    {
        click.Play();
        anim.SetBool("goToSettings", true);
    }

    public void OnSettingsBackPress()
    {
        click.Play();
        anim.SetBool("goToSettings", false);
    }

    public void OnCreditsButtonPress()
    {
        click.Play();
        anim.SetBool("goToCredits", true);
    }

    public void OnCreditsBackPress()
    {
        click.Play();
        anim.SetBool("goToCredits", false);
    }

    public void OnQuitButtonPress()
    {
        click.Play();
        Application.Quit();
    }

    public void OnBoardModeSelected()
    {
        click.Play();
        anim.SetBool("goToMinigames", true);

        bools.boardMode = true;
    }

    public void OnTournamentModeSelected()
    {
        click.Play();
        anim.SetBool("goToMinigames", true);

        bools.tournamentMode = true;
    }

    public void OnFreeplayModeSelected()
    {
        click.Play();
        anim.SetBool("goToMinigames", true);

        bools.freeplayMode = true;
    }

    public void OnBackToModes()
    {
        bools.boardMode = false;
        bools.tournamentMode = false;
        bools.freeplayMode = false;

        click.Play();
        anim.SetBool("goToMinigames", false);
    }

    public void OnMinigameMissilePressed()
    {
        click.Play();

        bools.selectedDodgeball = false;
        bools.selectedMissile = true;

        minigameName.text = "Missile Madness";
        minigamePreview.sprite = missilePreview;

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(true);

        anim.SetBool("goToMinigameInfo", true);
    }

    public void OnMinigameDodgeballPressed()
    {
        click.Play();

        bools.selectedDodgeball = true;
        bools.selectedMissile = false;

        minigameName.text = "Dodgeball";
        minigamePreview.sprite = dodgeballPreview;

        dodgeballInfo.SetActive(true);
        missileInfo.SetActive(false);

        anim.SetBool("goToMinigameInfo", true);
    }

    public void OnMinigameRacingPressed()
    {
        click.Play();

        bools.selectedDodgeball = false;
        bools.selectedMissile = false;
        bools.selectedGeo = false;
        bools.selectedRhythm = false;
        bools.selectedRacing = true;

        minigameName.text = "Racing";
        minigamePreview.sprite = noPreview;

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(false);

        anim.SetBool("goToMinigameInfo", true);
    }

    public void OnMinigameGeoPressed()
    {
        click.Play();

        bools.selectedDodgeball = false;
        bools.selectedMissile = false;
        bools.selectedGeo = true;
        bools.selectedRhythm = false;
        bools.selectedRacing = false;

        minigameName.text = "Geochase";
        minigamePreview.sprite = noPreview;

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(false);

        anim.SetBool("goToMinigameInfo", true);
    }

    public void OnMinigameRhythmPressed()
    {
        click.Play();

        bools.selectedDodgeball = false;
        bools.selectedMissile = false;
        bools.selectedGeo = false;
        bools.selectedRhythm = true;
        bools.selectedRacing = false;

        minigameName.text = "Rhythm Blitz";
        minigamePreview.sprite = noPreview;

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(false);

        anim.SetBool("goToMinigameInfo", true);
    }

    public void PlayMinigame()
    {
        if(bools.selectedMissile == true && (bools.selectedGeo == false && (bools.selectedDodgeball == false && (bools.selectedRacing == false && bools.selectedRhythm == false))))
        {
            SceneManager.LoadScene("MissileMadness");
        }
        if (bools.selectedMissile == false && (bools.selectedGeo == false && (bools.selectedDodgeball == true && (bools.selectedRacing == false && bools.selectedRhythm == false))))
        {
            SceneManager.LoadScene("DodgeballDojo");
        }
        if (bools.selectedMissile == false && (bools.selectedGeo == false && (bools.selectedDodgeball == false && (bools.selectedRacing == true && bools.selectedRhythm == false))))
        {
            SceneManager.LoadScene("Racing_Minigame");
        }
        if (bools.selectedMissile == false && (bools.selectedGeo == true && (bools.selectedDodgeball == false && (bools.selectedRacing == false && bools.selectedRhythm == false))))
        {
            SceneManager.LoadScene("Pacman_Minigame");
        }
        if (bools.selectedMissile == false && (bools.selectedGeo == false && (bools.selectedDodgeball == false && (bools.selectedRacing == false && bools.selectedRhythm == true))))
        {
            SceneManager.LoadScene("RhythmBlitz");
        }
    }

    public void OnBackToMinigames()
    {
        click.Play();
        anim.SetBool("goToMinigameInfo", false);
    }


    public void HoverBoard()
    {
        bools.hoverBoard = true;
    }
    public void NotHoverBoard()
    {
        bools.hoverBoard = false;
    }
    public void HoverTournament()
    {
        bools.hoverTournament = true;
    }
    public void NotHoverTournament()
    {
        bools.hoverTournament = false;
    }
    public void HoverFreeplay()
    {
        bools.hoverFreeplay = true;
    }
    public void NotHoverFreeplay()
    {
        bools.hoverFreeplay = false;
    }
}
