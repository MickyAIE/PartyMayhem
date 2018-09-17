using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#region External
[System.Serializable]
public class Main
{
    public GameObject startButton;
    public GameObject settingsButton;
    public GameObject creditsButton;
    public GameObject quitButton;
}
[System.Serializable]
public class Credits
{
    public GameObject backButton;
    public GameObject scrollBar;
}
[System.Serializable]
public class Settings
{
    public GameObject musicSlider;
    public GameObject effectsSlider;
    public GameObject resolutionDropDown;
    public GameObject GraphicsDropDown;
    public GameObject fullscreenToggle;
}
[System.Serializable]
public class Modes
{
    public GameObject tournamentButton;
    public GameObject freeplayButton;
}
[System.Serializable]
public class Lists
{
    public GameObject MissileButton;
    public GameObject RacingButton;
    public GameObject GeoButton;
    public GameObject DodgeButton;
    public GameObject RhythmButton;
}
[System.Serializable]
public class Profile
{
    public GameObject selectButton;
}

[System.Serializable]
public class Bbools
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
#endregion

public class MenuManager : MonoBehaviour {

    private GameManager gameManager;

    public Bbools bools;
    public Main main;
    public Credits credits;
    public Settings settings;
    public Modes modes;
    public Lists list;
    public Profile profile;

    public Animator anim;

    public AudioMixer audioMixer;
    public AudioSource music;
    public AudioSource sfx;
    public AudioClip click;
    public AudioClip specialClick;
    public AudioClip backClick;

    public Slider musicSlider;
    public Slider sfxSlider;

    public Text modeText;
    public Text minigameName;

    public Image minigamePreview;
    public Sprite missilePreview;
    public Sprite dodgeballPreview;
    //public Sprite geoPreview;
    public Sprite racingPreview;
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
    public GameObject racingInfo;
    public GameObject rhythmInfo;
    public GameObject geoInfo;

    public GameObject hoverGuideText;
    public GameObject notSavedPopUp;
    public GameObject controlsPopUp;
    public GameObject controlsPopUp2;
    public GameObject controlsPopUp3;

    public GameObject playerRanksButton;
    public GameObject playerRanksButton2;
    public GameObject playerRanksPage;
    public GameObject playerRanksPage2;

    public GameObject firstRankSpot;
    public GameObject secondRankSpot;
    public GameObject thirdRankSpot;
    public GameObject fourthRankSpot;
    public Text firstRankScore;
    public Text secondRankScore;
    public Text thirdRankScore;
    public Text fourthRankScore;
    public GameObject firstRankSpot2;
    public GameObject secondRankSpot2;
    public GameObject thirdRankSpot2;
    public GameObject fourthRankSpot2;
    public Text firstRankScore2;
    public Text secondRankScore2;
    public Text thirdRankScore2;
    public Text fourthRankScore2;

    public GameObject roundText;

    public GameObject selectButton;
    public GameObject controlButton;
    public GameObject datPopUp;

    public Dropdown lapsDropdown;
    public Dropdown timerDropdown;
    public Toggle trackA;
    public Toggle trackB;
    public Toggle easy;
    public Toggle normal;
    public Toggle hard;

    Resolution[] resolutions;

    public GameManager.Mode mode;

    public GameObject Fade;

    public int rounds;
    public int currentRound;

    public void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //PlayerPrefs.SetInt("activePlayers", 0);

        if (gameManager.returningToMenus == false)
        {
            anim.SetBool("goToSettings", false);
            anim.SetBool("goToCredits", false);
            anim.SetBool("goToModes", false);
            anim.SetBool("goToMinigames", false);
            anim.SetBool("goToMinigameInfo", false);
            anim.SetBool("startScreen", true);
            Fade.SetActive(false);
        }
        else
        {
            anim.SetBool("goToSettings", false);
            anim.SetBool("goToCredits", false);
            anim.SetBool("goToMinigameInfo", false);
            anim.SetBool("goToModes", true);
            anim.SetBool("goToMinigames", true);
            anim.SetBool("startScreen", false);
            Fade.SetActive(true);
        }


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

        datPopUp.SetActive(false);

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

    private bool track1 = true;
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

        if(bools.selectedRacing == true)
        {
            lapsDropdown.gameObject.SetActive(true);
            timerDropdown.gameObject.SetActive(false);
            trackA.gameObject.SetActive(true);
            trackB.gameObject.SetActive(true);
        }
        else
        {
            lapsDropdown.gameObject.SetActive(false);
            timerDropdown.gameObject.SetActive(true);
            trackA.gameObject.SetActive(false);
            trackB.gameObject.SetActive(false);
        }

        if(timerDropdown.value == 0)  gameManager.gameTimer = 60;
        if (timerDropdown.value == 1) gameManager.gameTimer = 120;
        if (timerDropdown.value == 2) gameManager.gameTimer = 180;
        if (timerDropdown.value == 3) gameManager.gameTimer = 240;
        if (timerDropdown.value == 4) gameManager.gameTimer = 300;

        if (lapsDropdown.value == 0) gameManager.gameLaps = 3;
        if (lapsDropdown.value == 1) gameManager.gameLaps = 4;
        if (lapsDropdown.value == 2) gameManager.gameLaps = 6;
        if (lapsDropdown.value == 3) gameManager.gameLaps = 8;

        if (trackA.isOn == true && trackB.isOn == false) track1 = true;
        if (trackA.isOn == false && trackB.isOn == true) track1 = false;

        if (easy.isOn == true && (normal.isOn == false && hard.isOn == false)) gameManager.difficultyIndex = 1;
        if (easy.isOn == false && (normal.isOn == true && hard.isOn == false)) gameManager.difficultyIndex = 2;
        if (easy.isOn == false && (normal.isOn == false && hard.isOn == true)) gameManager.difficultyIndex = 3;

        if (tournamentModeInfo.activeInHierarchy == false && (boardModeInfo.activeInHierarchy == false && freeplayModeInfo.activeInHierarchy == false))
        {
            hoverGuideText.SetActive(true);
        }
        else hoverGuideText.SetActive(false);

        RankPlayers();

        if (PlayerPrefs.GetInt("Mode") == 1) mode = GameManager.Mode.Board;
        else if (PlayerPrefs.GetInt("Mode") == 2) mode = GameManager.Mode.Tournament;
        else if (PlayerPrefs.GetInt("Mode") == 3) mode = GameManager.Mode.Freeplay;

        if (bools.boardMode == true && (bools.tournamentMode == false && bools.freeplayMode == false)) mode = GameManager.Mode.Board;        
        if (bools.tournamentMode == true && (bools.boardMode == false && bools.freeplayMode == false))  mode = GameManager.Mode.Tournament;       
        if (bools.freeplayMode == true && (bools.tournamentMode == false && bools.boardMode == false)) mode = GameManager.Mode.Freeplay;
        
        switch (mode)
        {
            case GameManager.Mode.Board:

                break;

            case GameManager.Mode.Tournament:
                roundText.SetActive(true);
                playerRanksButton.SetActive(true);
                playerRanksButton2.SetActive(true);
                break;

            case GameManager.Mode.Freeplay:
                roundText.SetActive(false);
                playerRanksButton.SetActive(false);
                playerRanksButton2.SetActive(false);

                break;

            default:
                Debug.Log("C");

                break;
        }

        modeText.text = (mode.ToString());

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.SetInt("activePlayers", 0);
        }
    }

#region Hover Functions
    public void ShowBoardInfo()
    {
        boardModeInfo.SetActive(true);
        bools.hoverBoard = true;
    }
    public void HideBoardInfo()
    {
        boardModeInfo.SetActive(false);
        bools.hoverBoard = false;
    }
    public void ShowTournamentInfo()
    {
        tournamentModeInfo.SetActive(true);
        bools.hoverTournament = true;
    }
    public void HideTournamentInfo()
    {
        tournamentModeInfo.SetActive(false);
        bools.hoverTournament = false;
    }
    public void ShowFreeplayInfo()
    {
        freeplayModeInfo.SetActive(true);
        bools.hoverFreeplay = true;
    }
    public void HideFreeplayInfo()
    {
        freeplayModeInfo.SetActive(false);
        bools.hoverFreeplay = false;
    }
#endregion

#region Settings Functions
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

        sfx.clip = specialClick;
        sfx.Play();

        Debug.Log("Saved");
    }
    #endregion

#region ModeSelect Functions
    public void OnBoardModeSelected()
    {
        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToMinigames", true);

        PlayerPrefs.SetInt("Mode", 1);
        bools.boardMode = true;
    }

    public void OnTournamentModeSelected()
    {
        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToMinigames", true);

        PlayerPrefs.SetInt("Mode", 2);
        bools.tournamentMode = true;
    }

    public void OnFreeplayModeSelected()
    {
        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToMinigames", true);

        PlayerPrefs.SetInt("activePlayers", 0);
        PlayerPrefs.SetInt("Mode", 3);
        bools.freeplayMode = true;
    }
    #endregion

#region On(button)Press Functions
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

        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);

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

        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);

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
            sfx.clip = backClick;
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
        sfx.clip = backClick;
        sfx.Play();

        bools.changesMade = false;
        bools.hasSaved = false;

        anim.SetBool("goToSettings", false);
        notSavedPopUp.SetActive(false);
    }
    public void SaveAndGoBack()
    {
        sfx.clip = specialClick;
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

        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);

        anim.SetBool("goToCredits", false);
    }

    public void OnQuitButtonPress()
    {
        sfx.clip = backClick;
        sfx.Play();
        PlayerPrefs.SetInt("activePlayers", 0);
        Application.Quit();
    }

    public void OnBackToModes()
    {
        bools.boardMode = false;
        bools.tournamentMode = false;
        bools.freeplayMode = false;

        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);

        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToMinigames", false);
    }

    public void OnBackToMinigames()
    {
        sfx.clip = click;
        sfx.Play();

        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);
        datPopUp.SetActive(false);
        selectButton.GetComponent<Button>().enabled = true;
        controlButton.GetComponent<Button>().enabled = true;
        playerRanksButton2.GetComponent<Button>().enabled = true;

        anim.SetBool("goToMinigameInfo", false);
    }

    public void ControlsPopUp()
    {
        sfx.clip = click;
        sfx.Play();

        controlsPopUp.SetActive(true);
        controlsPopUp2.SetActive(true);
        controlsPopUp3.SetActive(true);

        playerRanksPage.SetActive(false);
        playerRanksPage2.SetActive(false);
    }

    public void ControlsBack()
    {
        sfx.clip = backClick;
        sfx.Play();

        PlayerPrefs.SetInt("controls", 1);
        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);
    }

    public void OnRanksPressed()
    {
        sfx.clip = click;
        sfx.Play();

        playerRanksPage.SetActive(true);
        playerRanksPage2.SetActive(true);

        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);
    }
    public void OnRanksBack()
    {
        sfx.clip = backClick;
        sfx.Play();

        playerRanksPage.SetActive(false);
        playerRanksPage2.SetActive(false);
    }
#endregion

#region MinigamePressed Functions

    public void OnMinigameMissilePressed()
    {
        sfx.clip = click;
        sfx.Play();

        bools.selectedDodgeball = false;
        bools.selectedMissile = true;
        bools.selectedGeo = false;
        bools.selectedRhythm = false;
        bools.selectedRacing = false;

        minigameName.text = "Missile Madness";
        minigamePreview.sprite = missilePreview;

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(true);
        racingInfo.SetActive(false);
        geoInfo.SetActive(false);
        rhythmInfo.SetActive(false);

        anim.SetBool("goToMinigameInfo", true);
    }

    public void OnMinigameDodgeballPressed()
    {
        sfx.clip = click;
        sfx.Play();

        bools.selectedDodgeball = true;
        bools.selectedMissile = false;
        bools.selectedGeo = false;
        bools.selectedRhythm = false;
        bools.selectedRacing = false;

        minigameName.text = "Dodgeball";
        minigamePreview.sprite = dodgeballPreview;

        dodgeballInfo.SetActive(true);
        missileInfo.SetActive(false);
        racingInfo.SetActive(false);
        geoInfo.SetActive(false);
        rhythmInfo.SetActive(false);

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
        minigamePreview.sprite = racingPreview;

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(false);
        racingInfo.SetActive(true);
        geoInfo.SetActive(false);
        rhythmInfo.SetActive(false);

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
        racingInfo.SetActive(false);
        geoInfo.SetActive(true);
        rhythmInfo.SetActive(false);

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
        racingInfo.SetActive(false);
        geoInfo.SetActive(false);
        rhythmInfo.SetActive(true);

        anim.SetBool("goToMinigameInfo", true);
    }
#endregion

    public void PrePlayMinigame()
    {
        sfx.clip = specialClick;
        sfx.Play();

        datPopUp.SetActive(true);
        selectButton.GetComponent<Button>().enabled = false;
        controlButton.GetComponent<Button>().enabled = false;
        playerRanksButton2.GetComponent<Button>().enabled = false;
    }
    public void ExitPrePlayMinigame()
    {
        sfx.clip = backClick;
        sfx.Play();

        datPopUp.SetActive(false);
        selectButton.GetComponent<Button>().enabled = true;
        controlButton.GetComponent<Button>().enabled = true;
        playerRanksButton2.GetComponent<Button>().enabled = true;
    }

    public void PlayMinigame()
    {
        sfx.clip = specialClick;
        sfx.Play();

        if (bools.selectedMissile == true && (bools.selectedGeo == false && (bools.selectedDodgeball == false && (bools.selectedRacing == false && bools.selectedRhythm == false))))
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
            if(track1 == true)
                gameManager.minigameToLoad = "Racing_Minigame";
            else
                gameManager.minigameToLoad = "RacingMinigame2";
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

    public void RankPlayers()
    {

        if(PlayerPrefs.GetInt("activePlayers") != 0)
        {
            if (PlayerPrefs.GetInt("activePlayers") == 1)
            {
                firstRankSpot.SetActive(true);
                secondRankSpot.SetActive(false);
                thirdRankSpot.SetActive(false);
                fourthRankSpot.SetActive(false);

                firstRankSpot2.SetActive(true);
                secondRankSpot2.SetActive(false);
                thirdRankSpot2.SetActive(false);
                fourthRankSpot2.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("activePlayers") == 2)
            {
                firstRankSpot.SetActive(true);
                secondRankSpot.SetActive(true);
                thirdRankSpot.SetActive(false);
                fourthRankSpot.SetActive(false);

                firstRankSpot2.SetActive(true);
                secondRankSpot2.SetActive(true);
                thirdRankSpot2.SetActive(false);
                fourthRankSpot2.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("activePlayers") == 3)
            {
                firstRankSpot.SetActive(true);
                secondRankSpot.SetActive(true);
                thirdRankSpot.SetActive(true);
                fourthRankSpot.SetActive(false);

                firstRankSpot2.SetActive(true);
                secondRankSpot2.SetActive(true);
                thirdRankSpot2.SetActive(true);
                fourthRankSpot2.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("activePlayers") == 4)
            {
                firstRankSpot.SetActive(true);
                secondRankSpot.SetActive(true);
                thirdRankSpot.SetActive(true);
                fourthRankSpot.SetActive(true);

                firstRankSpot2.SetActive(true);
                secondRankSpot2.SetActive(true);
                thirdRankSpot2.SetActive(true);
                fourthRankSpot2.SetActive(true);
            }

            CheckPlayerOrder();
        }
        else
        {
            firstRankSpot.SetActive(true);
            secondRankSpot.SetActive(true);
            thirdRankSpot.SetActive(true);
            fourthRankSpot.SetActive(true);

            firstRankSpot2.SetActive(true);
            secondRankSpot2.SetActive(true);
            thirdRankSpot2.SetActive(true);
            fourthRankSpot2.SetActive(true);

            firstRankScore.text = "---";
            secondRankScore.text = "---";
            thirdRankScore.text = "---";
            fourthRankScore.text = "---";
            firstRankScore2.text = "---";
            secondRankScore2.text = "---";
            thirdRankScore2.text = "---";
            fourthRankScore2.text = "---";
        }
    }

    public void CheckPlayerOrder()
    {
        //Player one: First Place
        if(gameManager.player1Score > gameManager.player2Score && ((gameManager.player1Score > gameManager.player3Score) && (gameManager.player1Score > gameManager.player4Score)))
        {
            firstRankSpot.GetComponent<Text>().text = "Player 1";
            firstRankSpot2.GetComponent<Text>().text = "Player 1";
            firstRankScore.text = " -  " + gameManager.player1Score.ToString();
            firstRankScore2.text = " -  " + gameManager.player1Score.ToString();
        }
        //Player one: Second Place
        else if (gameManager.player1Score < gameManager.player2Score && ((gameManager.player1Score > gameManager.player3Score) && (gameManager.player1Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 1";
            secondRankSpot2.GetComponent<Text>().text = "Player 1";
            secondRankScore.text = " -  " + gameManager.player1Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player1Score.ToString();
        }
        else if(gameManager.player1Score > gameManager.player3Score && ((gameManager.player1Score > gameManager.player2Score) && (gameManager.player1Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 1";
            secondRankSpot2.GetComponent<Text>().text = "Player 1";
            secondRankScore.text = " -  " + gameManager.player1Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player1Score.ToString();
        }
        else if(gameManager.player1Score > gameManager.player4Score && ((gameManager.player1Score > gameManager.player2Score) && (gameManager.player1Score > gameManager.player3Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 1";
            secondRankSpot2.GetComponent<Text>().text = "Player 1";
            secondRankScore.text = " -  " + gameManager.player1Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player1Score.ToString();
        }
        //Player one: Third Place
        else if(gameManager.player1Score < gameManager.player2Score && ((gameManager.player1Score < gameManager.player3Score) && (gameManager.player1Score > gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 1";
            thirdRankSpot2.GetComponent<Text>().text = "Player 1";
            thirdRankScore.text = " -  " + gameManager.player1Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player1Score.ToString();
        }
        else if(gameManager.player1Score < gameManager.player2Score && ((gameManager.player1Score > gameManager.player3Score) && (gameManager.player1Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 1";
            thirdRankSpot2.GetComponent<Text>().text = "Player 1";
            thirdRankScore.text = " -  " + gameManager.player1Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player1Score.ToString();
        }
        else if(gameManager.player1Score > gameManager.player2Score && ((gameManager.player1Score < gameManager.player3Score) && (gameManager.player1Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 1";
            thirdRankSpot2.GetComponent<Text>().text = "Player 1";
            thirdRankScore.text = " -  " + gameManager.player1Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player1Score.ToString();
        }
        //Player one: Fourth Place
        else if(gameManager.player2Score < gameManager.player1Score && ((gameManager.player1Score < gameManager.player3Score) && (gameManager.player1Score < gameManager.player4Score)))
        {
            fourthRankSpot.GetComponent<Text>().text = "Player 1";
            fourthRankSpot2.GetComponent<Text>().text = "Player 1";
            fourthRankScore.text = " -  " + gameManager.player1Score.ToString();
            fourthRankScore2.text = " -  " + gameManager.player1Score.ToString();
        }


        //Player two: First Place
        if (gameManager.player2Score > gameManager.player1Score && ((gameManager.player2Score > gameManager.player3Score) && (gameManager.player2Score > gameManager.player4Score)))
        {
            firstRankSpot.GetComponent<Text>().text = "Player 2";
            firstRankSpot2.GetComponent<Text>().text = "Player 2";
            firstRankScore.text = " -  " + gameManager.player2Score.ToString();
            firstRankScore2.text = " -  " + gameManager.player2Score.ToString();
        }
        //Player two: Second Place
        else if (gameManager.player2Score < gameManager.player1Score && ((gameManager.player2Score > gameManager.player3Score) && (gameManager.player2Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 2";
            secondRankSpot2.GetComponent<Text>().text = "Player 2";
            secondRankScore.text = " -  " + gameManager.player2Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player2Score.ToString();
        }
        else if (gameManager.player2Score > gameManager.player3Score && ((gameManager.player2Score > gameManager.player1Score) && (gameManager.player2Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 2";
            secondRankSpot2.GetComponent<Text>().text = "Player 2";
            secondRankScore.text = " -  " + gameManager.player2Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player2Score.ToString();
        }
        else if (gameManager.player2Score > gameManager.player4Score && ((gameManager.player2Score > gameManager.player1Score) && (gameManager.player2Score > gameManager.player3Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 2";
            secondRankSpot2.GetComponent<Text>().text = "Player 2";
            secondRankScore.text = " -  " + gameManager.player2Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player2Score.ToString();
        }
        //Player two: Third Place
        else if (gameManager.player2Score < gameManager.player1Score && ((gameManager.player2Score < gameManager.player3Score) && (gameManager.player2Score > gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 2";
            thirdRankSpot2.GetComponent<Text>().text = "Player 2";
            thirdRankScore.text = " -  " + gameManager.player2Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player2Score.ToString();
        }
        else if (gameManager.player2Score < gameManager.player1Score && ((gameManager.player2Score > gameManager.player3Score) && (gameManager.player2Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 2";
            thirdRankSpot2.GetComponent<Text>().text = "Player 2";
            thirdRankScore.text = " -  " + gameManager.player2Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player2Score.ToString();
        }
        else if (gameManager.player2Score > gameManager.player1Score && ((gameManager.player2Score < gameManager.player3Score) && (gameManager.player2Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 2";
            thirdRankSpot2.GetComponent<Text>().text = "Player 2";
            thirdRankScore.text = " -  " + gameManager.player2Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player2Score.ToString();
        }
        //Player two: Fourth Place
        else if (gameManager.player2Score < gameManager.player1Score && ((gameManager.player2Score < gameManager.player3Score) && (gameManager.player2Score < gameManager.player4Score)))
        {
            fourthRankSpot.GetComponent<Text>().text = "Player 2";
            fourthRankSpot2.GetComponent<Text>().text = "Player 2";
            fourthRankScore.text = " -  " + gameManager.player2Score.ToString();
            fourthRankScore2.text = " -  " + gameManager.player2Score.ToString();
        }


        //Player three: First Place
        if (gameManager.player3Score > gameManager.player1Score && ((gameManager.player3Score > gameManager.player2Score) && (gameManager.player3Score > gameManager.player4Score)))
        {
            firstRankSpot.GetComponent<Text>().text = "Player 3";
            firstRankSpot2.GetComponent<Text>().text = "Player 3";
            firstRankScore.text = " -  " + gameManager.player3Score.ToString();
            firstRankScore2.text = " -  " + gameManager.player3Score.ToString();
        }
        //Player three: Second Place
        else if (gameManager.player3Score < gameManager.player1Score && ((gameManager.player3Score > gameManager.player2Score) && (gameManager.player3Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 3";
            secondRankSpot2.GetComponent<Text>().text = "Player 3";
            secondRankScore.text = " -  " + gameManager.player3Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player3Score.ToString();
        }
        else if (gameManager.player3Score > gameManager.player2Score && ((gameManager.player3Score > gameManager.player1Score) && (gameManager.player3Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 3";
            secondRankSpot2.GetComponent<Text>().text = "Player 3";
            secondRankScore.text = " -  " + gameManager.player3Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player3Score.ToString();
        }
        else if (gameManager.player3Score > gameManager.player4Score && ((gameManager.player3Score > gameManager.player1Score) && (gameManager.player3Score > gameManager.player2Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 3";
            secondRankSpot2.GetComponent<Text>().text = "Player 3";
            secondRankScore.text = " -  " + gameManager.player3Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player3Score.ToString();
        }
        //Player three: Third Place
        else if (gameManager.player3Score < gameManager.player1Score && ((gameManager.player3Score < gameManager.player2Score) && (gameManager.player3Score > gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 3";
            thirdRankSpot2.GetComponent<Text>().text = "Player 3";
            thirdRankScore.text = " -  " + gameManager.player3Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player3Score.ToString();
        }
        else if (gameManager.player3Score < gameManager.player1Score && ((gameManager.player3Score > gameManager.player2Score) && (gameManager.player3Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 3";
            thirdRankSpot2.GetComponent<Text>().text = "Player 3";
            thirdRankScore.text = " -  " + gameManager.player3Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player3Score.ToString();
        }
        else if (gameManager.player3Score > gameManager.player1Score && ((gameManager.player3Score < gameManager.player2Score) && (gameManager.player3Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 3";
            thirdRankSpot2.GetComponent<Text>().text = "Player 3";
            thirdRankScore.text = " -  " + gameManager.player3Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player3Score.ToString();
        }
        //Player three: Fourth Place
        else if (gameManager.player3Score < gameManager.player1Score && ((gameManager.player3Score < gameManager.player2Score) && (gameManager.player3Score < gameManager.player4Score)))
        {
            fourthRankSpot.GetComponent<Text>().text = "Player 3";
            fourthRankSpot2.GetComponent<Text>().text = "Player 3";
            fourthRankScore.text = " -  " + gameManager.player3Score.ToString();
            fourthRankScore2.text = " -  " + gameManager.player3Score.ToString();
        }


        //Player fourth: First Place
        if (gameManager.player4Score > gameManager.player1Score && ((gameManager.player4Score > gameManager.player2Score) && (gameManager.player4Score > gameManager.player3Score)))
        {
            firstRankSpot.GetComponent<Text>().text = "Player 4";
            firstRankSpot2.GetComponent<Text>().text = "Player 4";
            firstRankScore.text = " -  " + gameManager.player4Score.ToString();
            firstRankScore2.text = " -  " + gameManager.player4Score.ToString();
        }
        //Player fourth: Second Place
        else if (gameManager.player4Score < gameManager.player1Score && ((gameManager.player4Score > gameManager.player2Score) && (gameManager.player4Score > gameManager.player3Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 4";
            secondRankSpot2.GetComponent<Text>().text = "Player 4";
            secondRankScore.text = " -  " + gameManager.player4Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player4Score.ToString();
        }   
        else if (gameManager.player4Score > gameManager.player2Score && ((gameManager.player4Score > gameManager.player1Score) && (gameManager.player4Score > gameManager.player3Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 4";
            secondRankSpot2.GetComponent<Text>().text = "Player 4";
            secondRankScore.text = " -  " + gameManager.player4Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player4Score.ToString();
        }   
        else if (gameManager.player4Score > gameManager.player3Score && ((gameManager.player4Score > gameManager.player1Score) && (gameManager.player4Score > gameManager.player2Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 4";
            secondRankSpot2.GetComponent<Text>().text = "Player 4";
            secondRankScore.text = " -  " + gameManager.player4Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player4Score.ToString();
        }
        //Player fourth: Third Place
        else if (gameManager.player4Score < gameManager.player1Score && ((gameManager.player4Score < gameManager.player2Score) && (gameManager.player4Score > gameManager.player3Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 4";
            thirdRankSpot2.GetComponent<Text>().text = "Player 4";
            thirdRankScore.text = " -  " + gameManager.player4Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player4Score.ToString();
        }    
        else if (gameManager.player4Score < gameManager.player1Score && ((gameManager.player4Score > gameManager.player2Score) && (gameManager.player4Score < gameManager.player3Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 4";
            thirdRankSpot2.GetComponent<Text>().text = "Player 4";
            thirdRankScore.text = " -  " + gameManager.player4Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player4Score.ToString();
        } 
        else if (gameManager.player4Score > gameManager.player1Score && ((gameManager.player4Score < gameManager.player2Score) && (gameManager.player4Score < gameManager.player3Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 4";
            thirdRankSpot2.GetComponent<Text>().text = "Player 4";
            thirdRankScore.text = " -  " + gameManager.player4Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player4Score.ToString();
        }
        //Player fourth: Fourth Place
        else if (gameManager.player4Score < gameManager.player1Score && ((gameManager.player4Score < gameManager.player2Score) && (gameManager.player4Score < gameManager.player3Score)))
        {
            fourthRankSpot.GetComponent<Text>().text = "Player 4";
            fourthRankSpot2.GetComponent<Text>().text = "Player 4";
            fourthRankScore.text = " -  " + gameManager.player4Score.ToString();
            fourthRankScore2.text = " -  " + gameManager.player4Score.ToString();
        }
    }
}
