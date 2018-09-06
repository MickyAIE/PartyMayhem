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

    public bool boardMode;
    public bool tournamentMode;
    public bool freeplayMode;

    public bool selectedMissile = false;
    public bool selectedDodgeball = false;
    public bool selectedRacing = false;
    public bool selectedGeo = false;
    public bool selectedRhythm = false;

    public bool hasSaved = false;
    public bool changesMade = false;

    public bool pressSpace = false;
}

public class MenuManager : MonoBehaviour {

    private GameManager gameManager;

    public bools bools;

    public Animator anim;

    public AudioMixer audioMixer;
    public AudioSource music;
    public AudioSource sfx;
    public AudioClip click;
    public AudioClip error;

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
    public Toggle fullScreenToggle;

    public GameObject boardModeInfo;
    public GameObject tournamentModeInfo;
    public GameObject freeplayModeInfo;

    public GameObject missileInfo;
    public GameObject dodgeballInfo;

    public GameObject hoverGuideText;
    public GameObject notSavedPopUp;
    public GameObject controlsPopUp;
    public GameObject controlsPopUp2;
    public GameObject controlsPopUp3;

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
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

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

        bools.hasSaved = false;
        bools.changesMade = false;

        boardModeInfo.SetActive(false);
        tournamentModeInfo.SetActive(false);
        freeplayModeInfo.SetActive(false);
        hoverGuideText.SetActive(false);

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(false);

        notSavedPopUp.SetActive(false);

        if(PlayerPrefs.GetInt("controls", 0) == 0)
        {
            controlsPopUp.SetActive(true);
        }
        else
        {
            controlsPopUp.SetActive(false);
        }
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);

        graphicsDropdown.value = PlayerPrefs.GetInt("graphics", 3);
        sfxSlider.value = PlayerPrefs.GetFloat("sVolume", -15f);
        musicSlider.value = PlayerPrefs.GetFloat("mVolume", -15f);

        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("mVolume", -15f));
        audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sVolume", -15f));

        if (Screen.fullScreen == true)
        {
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }

        sfx.clip = click;

        mode = Mode.NotChosen;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
            
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution", currentResolutionIndex);
    }

    float tempTimer = 1.1f;
    public void Update()
    {
        if (tempTimer > 1)
        {
            bools.changesMade = false;
        }
        tempTimer -= Time.deltaTime;

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
        bools.changesMade = true;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
        bools.changesMade = true;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("Quality Level: " + qualityIndex);

        bools.changesMade = true;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Is fullscreen: " + isFullscreen);

        bools.changesMade = true;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Resolution: " + resolution);

        bools.changesMade = true;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("sVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("mVolume", musicSlider.value);

        PlayerPrefs.SetInt("graphics", graphicsDropdown.value);
        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);

        bools.hasSaved = true;
        bools.changesMade = false;

        Debug.Log("Saved");
    }

    public void ControlsPopUp()
    {
        controlsPopUp.SetActive(true);
        controlsPopUp2.SetActive(true);
        controlsPopUp3.SetActive(true);
    }

    public void ControlsBack()
    {
        PlayerPrefs.SetInt("controls", 1);
        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);
    }

    public void OnStartButtonPress()
    {
        sfx.clip = click;
        sfx.Play();

        anim.SetBool("goToModes", true);
    }

    public void OnModesBackPress()
    {
        sfx.clip = click;
        sfx.Play();

        anim.SetBool("goToModes", false);
    }

    public void OnSettingsButtonPress()
    {
        sfx.clip = click;
        sfx.Play();

        anim.SetBool("goToSettings", true);
    }

    public void OnSettingsBackPress()
    {
        sfx.clip = click;
        sfx.Play();
        sfx.clip = click;
        sfx.Play();
        if (bools.hasSaved == true)
        {
            anim.SetBool("goToSettings", false);
            notSavedPopUp.SetActive(false);
            bools.hasSaved = false;
            bools.changesMade = false;
        }
        else if(bools.hasSaved == false && bools.changesMade == true)
        {
            notSavedPopUp.SetActive(true);
            sfx.clip = error;
            sfx.Play();
        }
        else
        {
            anim.SetBool("goToSettings", false);
            notSavedPopUp.SetActive(false);
            bools.hasSaved = false;
            bools.changesMade = false;
        }
    }

    public void SettingsPopUpCancel()
    {
        sfx.clip = click;
        sfx.Play();

        notSavedPopUp.SetActive(false);
    }

    public void GoBackAnyway()
    {
        sfx.clip = click;
        sfx.Play();

        bools.changesMade = false;
        bools.hasSaved = false;

        anim.SetBool("goToSettings", false);
        notSavedPopUp.SetActive(false);
    }
    public void SaveAndGoBack()
    {
        sfx.clip = click;
        sfx.Play();
        Save();

        bools.changesMade = false;
        bools.hasSaved = false;

        anim.SetBool("goToSettings", false);
        notSavedPopUp.SetActive(false);
    }

    public void OnCreditsButtonPress()
    {
        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToCredits", true);
    }

    public void OnCreditsBackPress()
    {
        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToCredits", false);
    }

    public void OnQuitButtonPress()
    {
        sfx.clip = click;
        sfx.Play();
        Application.Quit();
    }

    public void OnBoardModeSelected()
    {
        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToMinigames", true);

        bools.boardMode = true;
    }

    public void OnTournamentModeSelected()
    {
        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToMinigames", true);

        bools.tournamentMode = true;
    }

    public void OnFreeplayModeSelected()
    {
        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToMinigames", true);

        bools.freeplayMode = true;
    }

    public void OnBackToModes()
    {
        bools.boardMode = false;
        bools.tournamentMode = false;
        bools.freeplayMode = false;

        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToMinigames", false);
    }

    public void OnMinigameMissilePressed()
    {
        sfx.clip = click;
        sfx.Play();

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
        sfx.clip = click;
        sfx.Play();

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
        sfx.clip = click;
        sfx.Play();

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
        sfx.clip = click;
        sfx.Play();

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
        sfx.clip = click;
        sfx.Play();

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
            gameManager.minigameToLoad = "MissileMadness";
            SceneManager.LoadScene("Char Select Menu");
        }
        if (bools.selectedMissile == false && (bools.selectedGeo == false && (bools.selectedDodgeball == true && (bools.selectedRacing == false && bools.selectedRhythm == false))))
        {
            gameManager.minigameToLoad = "DodgeballDojo";
            SceneManager.LoadScene("Char Select Menu");
        }
        if (bools.selectedMissile == false && (bools.selectedGeo == false && (bools.selectedDodgeball == false && (bools.selectedRacing == true && bools.selectedRhythm == false))))
        {
            gameManager.minigameToLoad = "Racing_Minigame";
            SceneManager.LoadScene("Char Select Menu");
        }
        if (bools.selectedMissile == false && (bools.selectedGeo == true && (bools.selectedDodgeball == false && (bools.selectedRacing == false && bools.selectedRhythm == false))))
        {
            gameManager.minigameToLoad = "Pacman_Minigame";
            SceneManager.LoadScene("Char Select Menu");
        }
        if (bools.selectedMissile == false && (bools.selectedGeo == false && (bools.selectedDodgeball == false && (bools.selectedRacing == false && bools.selectedRhythm == true))))
        {
            gameManager.minigameToLoad = "RhythmBlitz";
            SceneManager.LoadScene("Char Select Menu");
        }
    }

    public void OnBackToMinigames()
    {
        sfx.clip = click;
        sfx.Play();
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
