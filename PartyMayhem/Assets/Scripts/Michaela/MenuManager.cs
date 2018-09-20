using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[System.Serializable]
public class Bbools
{
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
    EventSystem eventSystem;

    public Bbools bools;

    public Animator anim;

    public AudioMixer audioMixer;
    public AudioSource music;
    public AudioSource sfx;
    public AudioClip click;
    public AudioClip specialClick;
    public AudioClip backClick;
    public AudioClip menuMusic;
    public AudioClip tournamentWin;

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

    public GameObject tournamentAftermath;
    public Text firstPlace;
    public Text secondPlace;
    public Text thirdPlace;
    public Text fourthPlace;
    public Text firstPlaceScore;
    public Text secondPlaceScore;
    public Text thirdPlaceScore;
    public Text fourthPlaceScore;

    public GameObject modeWarning;

    public GameObject backToMenuButton;

    public Text roundText;
    public GameObject roundSelect;
    public Dropdown roundsDropdown;

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

    public GameObject startButton;
    public GameObject freeplayButton;
    public GameObject missileMadnessButton;
    public GameObject saveButton;
    public GameObject confirmButton;
    public GameObject playButton;
    public GameObject selButton;
    public GameObject creditsBack;
    public GameObject proceedButton;
    public GameObject controlsBack;
    public GameObject controls2Back;
    public GameObject controls3Back;
    public GameObject rankBack;
    public GameObject rank2Back;
    public GameObject afterBack;

    Resolution[] resolutions;

    public GameManager.Mode mode;

    public GameObject Fade;

    public int rounds;
    public int currentRound;

    bool aftermathIsOn = false;

    bool inModes = false;
    bool inMinigames = false;
    bool inMinigameInfo = false;

    bool controlsOpen = false;
    bool ranksOpen = false;

    public void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        eventSystem = EventSystem.current;

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

            eventSystem.SetSelectedGameObject(startButton);
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

            eventSystem.SetSelectedGameObject(missileMadnessButton);
        }

        bools.hasSaved = false;
        bools.changesMade = false;

        boardModeInfo.SetActive(false);
        tournamentModeInfo.SetActive(false);
        freeplayModeInfo.SetActive(false);
        hoverGuideText.SetActive(false);

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(false);

        notSavedPopUp.SetActive(false);
        roundSelect.SetActive(false);

        aftermathIsOn = false;

        if (PlayerPrefs.GetInt("controls", 0) == 0)
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

        tournamentAftermath.SetActive(false);
        modeWarning.SetActive(false);

        graphicsDropdown.value = PlayerPrefs.GetInt("graphics", 3);
        sfxSlider.value = PlayerPrefs.GetFloat("sVolume", -15f);
        musicSlider.value = PlayerPrefs.GetFloat("mVolume", -15f);

        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("mVolume", -15f));
        audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sVolume", -15f));

        music.clip = menuMusic;
        music.loop = true;

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

        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            mode = GameManager.Mode.Board;
            gameManager.tournamentMode = false;
        }
        else if (PlayerPrefs.GetInt("Mode") == 2)
        {
            mode = GameManager.Mode.Tournament;
            gameManager.tournamentMode = true;
        }
        else if (PlayerPrefs.GetInt("Mode") == 3)
        {
            mode = GameManager.Mode.Freeplay;
            gameManager.tournamentMode = false;
        }

        switch (mode)
        {
            case GameManager.Mode.Board:

                break;

            case GameManager.Mode.Tournament:
                roundText.gameObject.SetActive(true);
                playerRanksButton.SetActive(true);
                playerRanksButton2.SetActive(true);

                if(gameManager.currentRound == gameManager.rounds)
                {
                    music.loop = false;
                    music.clip = tournamentWin;

                    if (aftermathIsOn == false)
                    {
                        tournamentAftermath.SetActive(true);
                        eventSystem.SetSelectedGameObject(afterBack);
                        aftermathIsOn = true;
                    }
                }

                break;

            case GameManager.Mode.Freeplay:
                roundText.gameObject.SetActive(false);
                playerRanksButton.SetActive(false);
                playerRanksButton2.SetActive(false);

                break;

            default:
                Debug.Log("C");

                break;
        }

        roundText.text = "Round: " + (gameManager.currentRound + 1).ToString();
        modeText.text = mode.ToString();
    }

#region Hover Functions
    public void ShowBoardInfo()
    {
        boardModeInfo.SetActive(true);
    }
    public void HideBoardInfo()
    {
        boardModeInfo.SetActive(false);
    }
    public void ShowTournamentInfo()
    {
        tournamentModeInfo.SetActive(true);
    }
    public void HideTournamentInfo()
    {
        tournamentModeInfo.SetActive(false);
    }
    public void ShowFreeplayInfo()
    {
        freeplayModeInfo.SetActive(true);
    }
    public void HideFreeplayInfo()
    {
        freeplayModeInfo.SetActive(false);
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
        //Debug.Log("Quality Level: " + qualityIndex);

        bools.changesMade = true;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        //Debug.Log("Is fullscreen: " + isFullscreen);

        bools.changesMade = true;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //Debug.Log("Resolution: " + resolution);

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

        //Debug.Log("Saved");
    }
    #endregion

#region ModeSelect Functions
    public void OnBoardModeSelected()
    {
        sfx.clip = click;
        sfx.Play();

        inModes = false;
        inMinigames = true;
        inMinigameInfo = false;

        anim.SetBool("goToMinigames", true);
        eventSystem.SetSelectedGameObject(missileMadnessButton);
        PlayerPrefs.SetInt("Mode", 1);
    }

    public void OnTournamentModeSelected()
    {
        PlayerPrefs.SetInt("activePlayers", 0);
        gameManager.player1Score = 0;
        gameManager.player2Score = 0;
        gameManager.player3Score = 0;
        gameManager.player4Score = 0;

        inModes = false;
        inMinigames = true;
        inMinigameInfo = false;

        anim.SetBool("goToMinigames", true);
        eventSystem.SetSelectedGameObject(missileMadnessButton);
        PlayerPrefs.SetInt("Mode", 2);
    }

    public void OnFreeplayModeSelected()
    {
        sfx.clip = click;
        sfx.Play();
        anim.SetBool("goToMinigames", true);

        inModes = false;
        inMinigames = true;
        inMinigameInfo = false;

        eventSystem.SetSelectedGameObject(missileMadnessButton);
        PlayerPrefs.SetInt("activePlayers", 0);
        PlayerPrefs.SetInt("Mode", 3);
    }
    #endregion

#region On(button)Press Functions
    public void OnStartButtonPress()
    {
        sfx.clip = click;
        sfx.Play();

        inModes = true;

        eventSystem.SetSelectedGameObject(freeplayButton);
        anim.SetBool("goToModes", true);
    }

    public void OnModesBackPress()
    {
        sfx.clip = click;
        sfx.Play();

        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);

        inModes = false;
        inMinigames = false;
        inMinigameInfo = false;

        eventSystem.SetSelectedGameObject(startButton);
        anim.SetBool("goToModes", false);
    }

    public void OnSettingsButtonPress()
    {
        sfx.clip = click;
        sfx.Play();

        eventSystem.SetSelectedGameObject(musicSlider.gameObject);
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
            eventSystem.SetSelectedGameObject(startButton);
        }
        else if(bools.hasSaved == false && bools.changesMade == true)
        {
            notSavedPopUp.SetActive(true);
            sfx.clip = backClick;
            sfx.Play();
            eventSystem.SetSelectedGameObject(saveButton);
        }
        else
        {
            anim.SetBool("goToSettings", false);
            notSavedPopUp.SetActive(false);
            bools.hasSaved = false;
            bools.changesMade = false;
            eventSystem.SetSelectedGameObject(startButton);
        }
    }

    public void SettingsPopUpCancel()
    {
        sfx.clip = click;
        sfx.Play();

        eventSystem.SetSelectedGameObject(musicSlider.gameObject);
        notSavedPopUp.SetActive(false);
    }

    public void GoBackAnyway()
    {
        sfx.clip = backClick;
        sfx.Play();

        bools.changesMade = false;
        bools.hasSaved = false;

        eventSystem.SetSelectedGameObject(startButton);
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

        eventSystem.SetSelectedGameObject(startButton);
        anim.SetBool("goToSettings", false);
        notSavedPopUp.SetActive(false);
    }

    public void OnCreditsButtonPress()
    {
        sfx.clip = click;
        sfx.Play();

        eventSystem.SetSelectedGameObject(creditsBack);
        anim.SetBool("goToCredits", true);
    }

    public void OnCreditsBackPress()
    {
        sfx.clip = click;
        sfx.Play();

        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);

        eventSystem.SetSelectedGameObject(startButton);
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
        PlayerPrefs.SetInt("activePlayers", 0);
        PlayerPrefs.SetInt("Mode", 3);

        gameManager.player1Score = 0;
        gameManager.player2Score = 0;
        gameManager.player3Score = 0;
        gameManager.player4Score = 0;

        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);

        inModes = true;
        inMinigames = false;
        inMinigameInfo = false;

        eventSystem.SetSelectedGameObject(freeplayButton);
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

        inModes = false;
        inMinigames = true;
        inMinigameInfo = false;

        selectButton.GetComponent<Button>().enabled = true;
        controlButton.GetComponent<Button>().enabled = true;
        playerRanksButton2.GetComponent<Button>().enabled = true;

        eventSystem.SetSelectedGameObject(missileMadnessButton);
        anim.SetBool("goToMinigameInfo", false);
    }

    public void ControlsPopUp()
    {
        sfx.clip = click;
        sfx.Play();

        controlsPopUp.SetActive(true);
        controlsPopUp2.SetActive(true);
        controlsPopUp3.SetActive(true);

        roundSelect.SetActive(false);
        playerRanksPage.SetActive(false);
        playerRanksPage2.SetActive(false);

        if(inModes == true)
        {
            eventSystem.SetSelectedGameObject(controlsBack);
        }
        else if (inMinigames == true)
        {
            eventSystem.SetSelectedGameObject(controls2Back);
        }
        else if (inMinigameInfo == true)
        {
            eventSystem.SetSelectedGameObject(controls3Back);
        }
    }

    public void ControlsBack()
    {
        sfx.clip = backClick;
        sfx.Play();

        PlayerPrefs.SetInt("controls", 1);
        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);

        if (inModes == true)
        {
            eventSystem.SetSelectedGameObject(freeplayButton);
        }
        else if (inMinigames == true)
        {
            eventSystem.SetSelectedGameObject(missileMadnessButton);
        }
        else if (inMinigameInfo == true)
        {
            eventSystem.SetSelectedGameObject(selButton);
        }
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

        if (inMinigames == true)
        {
            eventSystem.SetSelectedGameObject(rankBack);
        }
        else if (inMinigameInfo == true)
        {
            eventSystem.SetSelectedGameObject(rank2Back);
        }
    }
    public void OnRanksBack()
    {
        sfx.clip = backClick;
        sfx.Play();

        controlsOpen = false;
        ranksOpen = false;

        playerRanksPage.SetActive(false);
        playerRanksPage2.SetActive(false);

        if (inMinigames == true)
        {
            eventSystem.SetSelectedGameObject(missileMadnessButton);
        }
        else if (inMinigameInfo == true)
        {
            eventSystem.SetSelectedGameObject(selButton);
        }
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

        inModes = false;
        inMinigames = false;
        inMinigameInfo = true;

        eventSystem.SetSelectedGameObject(selButton);
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

        inModes = false;
        inMinigames = false;
        inMinigameInfo = true;

        minigameName.text = "Laser Lunacy";
        minigamePreview.sprite = dodgeballPreview;

        dodgeballInfo.SetActive(true);
        missileInfo.SetActive(false);
        racingInfo.SetActive(false);
        geoInfo.SetActive(false);
        rhythmInfo.SetActive(false);

        eventSystem.SetSelectedGameObject(selButton);
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

        minigameName.text = "Rampant Racers";
        minigamePreview.sprite = racingPreview;

        dodgeballInfo.SetActive(false);
        missileInfo.SetActive(false);
        racingInfo.SetActive(true);
        geoInfo.SetActive(false);
        rhythmInfo.SetActive(false);

        inModes = false;
        inMinigames = false;
        inMinigameInfo = true;

        eventSystem.SetSelectedGameObject(selButton);
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

        inModes = false;
        inMinigames = false;
        inMinigameInfo = true;

        eventSystem.SetSelectedGameObject(selButton);
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

        inModes = false;
        inMinigames = false;
        inMinigameInfo = true;

        eventSystem.SetSelectedGameObject(selButton);
        anim.SetBool("goToMinigameInfo", true);
    }
#endregion

    public void RoundSelect()
    {
        sfx.clip = click;
        sfx.Play();

        controlsPopUp.SetActive(false);
        controlsPopUp2.SetActive(false);
        controlsPopUp3.SetActive(false);

        eventSystem.SetSelectedGameObject(confirmButton);
        roundSelect.SetActive(true);
    }
    public void ConfirmRound()
    {
        sfx.clip = specialClick;
        sfx.Play();

        if (roundsDropdown.value == 0) gameManager.rounds = 2;
        if (roundsDropdown.value == 1) gameManager.rounds = 3;
        if (roundsDropdown.value == 2) gameManager.rounds = 4;
        if (roundsDropdown.value == 3) gameManager.rounds = 5;

        gameManager.currentRound = 0;

        OnTournamentModeSelected();
        roundSelect.SetActive(false);
    }
    public void CancelRounds()
    {
        sfx.clip = backClick;
        sfx.Play();

        roundSelect.SetActive(false);
    }

    public void GiveWarning()
    {
        if(PlayerPrefs.GetInt("Mode") == 2)
        {
            sfx.clip = backClick;
            sfx.Play();

            eventSystem.SetSelectedGameObject(proceedButton);
            modeWarning.SetActive(true);
        }
        else
        {
            sfx.clip = click;
            OnBackToModes();
        }
    }
    public void CancelWarning()
    {
        sfx.clip = backClick;
        sfx.Play();

        eventSystem.SetSelectedGameObject(missileMadnessButton);
        modeWarning.SetActive(false);
    }
    public void ProceedToLeave()
    {
        sfx.clip = backClick;

        OnBackToModes();
        modeWarning.SetActive(false);
    }

    public void ReturnToMenu()
    {
        music.clip = menuMusic;
        music.loop = true;
        music.Play();

        PlayerPrefs.SetInt("Mode", 3);
        PlayerPrefs.SetInt("activePlayers", 0);

        gameManager.player1Score = 0;
        gameManager.player2Score = 0;
        gameManager.player3Score = 0;
        gameManager.player4Score = 0;
        gameManager.currentRound = 0;

        eventSystem.SetSelectedGameObject(startButton);
        backToMenuButton.transform.parent.gameObject.SetActive(false);
    }

    public void PrePlayMinigame()
    {
        sfx.clip = specialClick;
        sfx.Play();

        eventSystem.SetSelectedGameObject(playButton);
        datPopUp.SetActive(true);
        selectButton.GetComponent<Button>().enabled = false;
        controlButton.GetComponent<Button>().enabled = false;
        playerRanksButton2.GetComponent<Button>().enabled = false;
    }
    public void ExitPrePlayMinigame()
    {
        sfx.clip = backClick;
        sfx.Play();

        eventSystem.SetSelectedGameObject(selButton);
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

                firstPlace.gameObject.SetActive(true);
                secondPlace.gameObject.SetActive(false);
                thirdPlace.gameObject.SetActive(false);
                fourthPlace.gameObject.SetActive(false);
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

                firstPlace.gameObject.SetActive(true);
                secondPlace.gameObject.SetActive(true);
                thirdPlace.gameObject.SetActive(false);
                fourthPlace.gameObject.SetActive(false);
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

                firstPlace.gameObject.SetActive(true);
                secondPlace.gameObject.SetActive(true);
                thirdPlace.gameObject.SetActive(true);
                fourthPlace.gameObject.SetActive(false);
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

                firstPlace.gameObject.SetActive(true);
                secondPlace.gameObject.SetActive(true);
                thirdPlace.gameObject.SetActive(true);
                fourthPlace.gameObject.SetActive(true);
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
#region Player One
        //Player one: First Place
        if(gameManager.player1Score > gameManager.player2Score && ((gameManager.player1Score > gameManager.player3Score) && (gameManager.player1Score > gameManager.player4Score)))
        {
            firstRankSpot.GetComponent<Text>().text = "Player 1";
            firstRankSpot2.GetComponent<Text>().text = "Player 1";

            firstRankScore.text = " -  " + gameManager.player1Score.ToString();
            firstRankScore2.text = " -  " + gameManager.player1Score.ToString();

            firstPlace.text = "Player 1 Won!";
            firstPlaceScore.text = "with a score of " + gameManager.player1Score.ToString();
        }
        //Player one: Second Place
        else if (gameManager.player1Score < gameManager.player2Score && ((gameManager.player1Score > gameManager.player3Score) && (gameManager.player1Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 1";
            secondRankSpot2.GetComponent<Text>().text = "Player 1";

            secondRankScore.text = " -  " + gameManager.player1Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player1Score.ToString();

            secondPlace.text = "Player 1";
            secondPlaceScore.text = gameManager.player1Score.ToString();
        }
        else if(gameManager.player1Score > gameManager.player3Score && ((gameManager.player1Score > gameManager.player2Score) && (gameManager.player1Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 1";
            secondRankSpot2.GetComponent<Text>().text = "Player 1";

            secondRankScore.text = " -  " + gameManager.player1Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player1Score.ToString();

            secondPlace.text = "Player 1";
            secondPlaceScore.text = gameManager.player1Score.ToString();
        }
        else if(gameManager.player1Score > gameManager.player4Score && ((gameManager.player1Score > gameManager.player2Score) && (gameManager.player1Score > gameManager.player3Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 1";
            secondRankSpot2.GetComponent<Text>().text = "Player 1";

            secondRankScore.text = " -  " + gameManager.player1Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player1Score.ToString();

            secondPlace.text = "Player 1";
            secondPlaceScore.text = gameManager.player1Score.ToString();
        }
        //Player one: Third Place
        else if(gameManager.player1Score < gameManager.player2Score && ((gameManager.player1Score < gameManager.player3Score) && (gameManager.player1Score > gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 1";
            thirdRankSpot2.GetComponent<Text>().text = "Player 1";

            thirdRankScore.text = " -  " + gameManager.player1Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player1Score.ToString();

            thirdPlace.text = "Player 1";
            thirdPlaceScore.text = gameManager.player1Score.ToString();
        }
        else if(gameManager.player1Score < gameManager.player2Score && ((gameManager.player1Score > gameManager.player3Score) && (gameManager.player1Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 1";
            thirdRankSpot2.GetComponent<Text>().text = "Player 1";

            thirdRankScore.text = " -  " + gameManager.player1Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player1Score.ToString();

            thirdPlace.text = "Player 1";
            thirdPlaceScore.text = gameManager.player1Score.ToString();
        }
        else if(gameManager.player1Score > gameManager.player2Score && ((gameManager.player1Score < gameManager.player3Score) && (gameManager.player1Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 1";
            thirdRankSpot2.GetComponent<Text>().text = "Player 1";

            thirdRankScore.text = " -  " + gameManager.player1Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player1Score.ToString();

            thirdPlace.text = "Player 1";
            thirdPlaceScore.text = gameManager.player1Score.ToString();
        }
        //Player one: Fourth Place
        else if(gameManager.player2Score < gameManager.player1Score && ((gameManager.player1Score < gameManager.player3Score) && (gameManager.player1Score < gameManager.player4Score)))
        {
            fourthRankSpot.GetComponent<Text>().text = "Player 1";
            fourthRankSpot2.GetComponent<Text>().text = "Player 1";

            fourthRankScore.text = " -  " + gameManager.player1Score.ToString();
            fourthRankScore2.text = " -  " + gameManager.player1Score.ToString();

            fourthPlace.text = "Player 1";
            fourthPlaceScore.text = gameManager.player1Score.ToString();
        }
        //Player one: Has 0 points
        else
        {
            firstRankSpot.GetComponent<Text>().text = "Player 1";
            firstRankSpot2.GetComponent<Text>().text = "Player 1";

            firstRankScore.text = " -  " + gameManager.player1Score.ToString();
            firstRankScore2.text = " -  " + gameManager.player1Score.ToString();

            firstPlace.text = "Player 1 Won!";
            firstPlaceScore.text = "with a score of " + gameManager.player1Score.ToString();
        }
        #endregion

#region Player Two
        //Player two: First Place
        if (gameManager.player2Score > gameManager.player1Score && ((gameManager.player2Score > gameManager.player3Score) && (gameManager.player2Score > gameManager.player4Score)))
        {
            firstRankSpot.GetComponent<Text>().text = "Player 2";
            firstRankSpot2.GetComponent<Text>().text = "Player 2";

            firstRankScore.text = " -  " + gameManager.player2Score.ToString();
            firstRankScore2.text = " -  " + gameManager.player2Score.ToString();

            firstPlace.text = "Player 2 Won!";
            firstPlaceScore.text = "with a score of " + gameManager.player2Score.ToString();
        }
        //Player two: Second Place
        else if (gameManager.player2Score < gameManager.player1Score && ((gameManager.player2Score > gameManager.player3Score) && (gameManager.player2Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 2";
            secondRankSpot2.GetComponent<Text>().text = "Player 2";

            secondRankScore.text = " -  " + gameManager.player2Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player2Score.ToString();

            secondPlace.text = "Player 2";
            secondPlaceScore.text = gameManager.player2Score.ToString();
        }
        else if (gameManager.player2Score > gameManager.player3Score && ((gameManager.player2Score > gameManager.player1Score) && (gameManager.player2Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 2";
            secondRankSpot2.GetComponent<Text>().text = "Player 2";

            secondRankScore.text = " -  " + gameManager.player2Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player2Score.ToString();

            secondPlace.text = "Player 2";
            secondPlaceScore.text = gameManager.player2Score.ToString();
        }
        else if (gameManager.player2Score > gameManager.player4Score && ((gameManager.player2Score > gameManager.player1Score) && (gameManager.player2Score > gameManager.player3Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 2";
            secondRankSpot2.GetComponent<Text>().text = "Player 2";

            secondRankScore.text = " -  " + gameManager.player2Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player2Score.ToString();

            secondPlace.text = "Player 2";
            secondPlaceScore.text = gameManager.player2Score.ToString();
        }
        //Player two: Third Place
        else if (gameManager.player2Score < gameManager.player1Score && ((gameManager.player2Score < gameManager.player3Score) && (gameManager.player2Score > gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 2";
            thirdRankSpot2.GetComponent<Text>().text = "Player 2";

            thirdRankScore.text = " -  " + gameManager.player2Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player2Score.ToString();

            thirdPlace.text = "Player 2";
            thirdPlaceScore.text = gameManager.player2Score.ToString();
        }
        else if (gameManager.player2Score < gameManager.player1Score && ((gameManager.player2Score > gameManager.player3Score) && (gameManager.player2Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 2";
            thirdRankSpot2.GetComponent<Text>().text = "Player 2";

            thirdRankScore.text = " -  " + gameManager.player2Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player2Score.ToString();

            thirdPlace.text = "Player 2";
            thirdPlaceScore.text = gameManager.player2Score.ToString();
        }
        else if (gameManager.player2Score > gameManager.player1Score && ((gameManager.player2Score < gameManager.player3Score) && (gameManager.player2Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 2";
            thirdRankSpot2.GetComponent<Text>().text = "Player 2";

            thirdRankScore.text = " -  " + gameManager.player2Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player2Score.ToString();

            thirdPlace.text = "Player 2";
            thirdPlaceScore.text = gameManager.player2Score.ToString();
        }
        //Player two: Fourth Place
        else if (gameManager.player2Score < gameManager.player1Score && ((gameManager.player2Score < gameManager.player3Score) && (gameManager.player2Score < gameManager.player4Score)))
        {
            fourthRankSpot.GetComponent<Text>().text = "Player 2";
            fourthRankSpot2.GetComponent<Text>().text = "Player 2";

            fourthRankScore.text = " -  " + gameManager.player2Score.ToString();
            fourthRankScore2.text = " -  " + gameManager.player2Score.ToString();

            fourthPlace.text = "Player 2";
            fourthPlaceScore.text = gameManager.player2Score.ToString();
        }
        //Player two: Has 0 points
        else
        {
            secondRankSpot.GetComponent<Text>().text = "Player 2";
            secondRankSpot2.GetComponent<Text>().text = "Player 2";

            secondRankScore.text = " -  " + gameManager.player2Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player2Score.ToString();

            secondPlace.text = "Player 2";
            secondPlaceScore.text = gameManager.player2Score.ToString();
        }
        #endregion

#region Player Three
        //Player three: First Place
        if (gameManager.player3Score > gameManager.player1Score && ((gameManager.player3Score > gameManager.player2Score) && (gameManager.player3Score > gameManager.player4Score)))
        {
            firstRankSpot.GetComponent<Text>().text = "Player 3";
            firstRankSpot2.GetComponent<Text>().text = "Player 3";

            firstRankScore.text = " -  " + gameManager.player3Score.ToString();
            firstRankScore2.text = " -  " + gameManager.player3Score.ToString();

            firstPlace.text = "Player 3 Won!";
            firstPlaceScore.text = "with a score of " + gameManager.player3Score.ToString();
        }
        //Player three: Second Place
        else if (gameManager.player3Score < gameManager.player1Score && ((gameManager.player3Score > gameManager.player2Score) && (gameManager.player3Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 3";
            secondRankSpot2.GetComponent<Text>().text = "Player 3";

            secondRankScore.text = " -  " + gameManager.player3Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player3Score.ToString();

            secondPlace.text = "Player 3 Won!";
            secondPlaceScore.text = gameManager.player3Score.ToString();
        }
        else if (gameManager.player3Score > gameManager.player2Score && ((gameManager.player3Score > gameManager.player1Score) && (gameManager.player3Score > gameManager.player4Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 3";
            secondRankSpot2.GetComponent<Text>().text = "Player 3";

            secondRankScore.text = " -  " + gameManager.player3Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player3Score.ToString();

            secondPlace.text = "Player 3 Won!";
            secondPlaceScore.text = gameManager.player3Score.ToString();
        }
        else if (gameManager.player3Score > gameManager.player4Score && ((gameManager.player3Score > gameManager.player1Score) && (gameManager.player3Score > gameManager.player2Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 3";
            secondRankSpot2.GetComponent<Text>().text = "Player 3";

            secondRankScore.text = " -  " + gameManager.player3Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player3Score.ToString();

            secondPlace.text = "Player 3 Won!";
            secondPlaceScore.text = gameManager.player3Score.ToString();
        }
        //Player three: Third Place
        else if (gameManager.player3Score < gameManager.player1Score && ((gameManager.player3Score < gameManager.player2Score) && (gameManager.player3Score > gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 3";
            thirdRankSpot2.GetComponent<Text>().text = "Player 3";

            thirdRankScore.text = " -  " + gameManager.player3Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player3Score.ToString();

            thirdPlace.text = "Player 3 Won!";
            thirdPlaceScore.text = gameManager.player3Score.ToString();
        }
        else if (gameManager.player3Score < gameManager.player1Score && ((gameManager.player3Score > gameManager.player2Score) && (gameManager.player3Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 3";
            thirdRankSpot2.GetComponent<Text>().text = "Player 3";

            thirdRankScore.text = " -  " + gameManager.player3Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player3Score.ToString();

            thirdPlace.text = "Player 3 Won!";
            thirdPlaceScore.text = gameManager.player3Score.ToString();
        }
        else if (gameManager.player3Score > gameManager.player1Score && ((gameManager.player3Score < gameManager.player2Score) && (gameManager.player3Score < gameManager.player4Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 3";
            thirdRankSpot2.GetComponent<Text>().text = "Player 3";

            thirdRankScore.text = " -  " + gameManager.player3Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player3Score.ToString();

            thirdPlace.text = "Player 3 Won!";
            thirdPlaceScore.text = gameManager.player3Score.ToString();
        }
        //Player three: Fourth Place
        else if (gameManager.player3Score < gameManager.player1Score && ((gameManager.player3Score < gameManager.player2Score) && (gameManager.player3Score < gameManager.player4Score)))
        {
            fourthRankSpot.GetComponent<Text>().text = "Player 3";
            fourthRankSpot2.GetComponent<Text>().text = "Player 3";

            fourthRankScore.text = " -  " + gameManager.player3Score.ToString();
            fourthRankScore2.text = " -  " + gameManager.player3Score.ToString();

            fourthPlace.text = "Player 3 Won!";
            fourthPlaceScore.text = gameManager.player3Score.ToString();
        }
        //Player three: Has 0 points
        else
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 3";
            thirdRankSpot2.GetComponent<Text>().text = "Player 3";

            thirdRankScore.text = " -  " + gameManager.player3Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player3Score.ToString();

            thirdPlace.text = "Player 3";
            thirdPlaceScore.text = gameManager.player3Score.ToString();
        }
        #endregion

#region Player Four
        //Player four: First Place
        if (gameManager.player4Score > gameManager.player1Score && ((gameManager.player4Score > gameManager.player2Score) && (gameManager.player4Score > gameManager.player3Score)))
        {
            firstRankSpot.GetComponent<Text>().text = "Player 4";
            firstRankSpot2.GetComponent<Text>().text = "Player 4";

            firstRankScore.text = " -  " + gameManager.player4Score.ToString();
            firstRankScore2.text = " -  " + gameManager.player4Score.ToString();

            firstPlace.text = "Player 4 Won!";
            firstPlaceScore.text = "with a score of " + gameManager.player4Score.ToString();
        }
        //Player four: Second Place
        else if (gameManager.player4Score < gameManager.player1Score && ((gameManager.player4Score > gameManager.player2Score) && (gameManager.player4Score > gameManager.player3Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 4";
            secondRankSpot2.GetComponent<Text>().text = "Player 4";

            secondRankScore.text = " -  " + gameManager.player4Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player4Score.ToString();

            secondPlace.text = "Player 4";
            secondPlaceScore.text = gameManager.player4Score.ToString();
        }   
        else if (gameManager.player4Score > gameManager.player2Score && ((gameManager.player4Score > gameManager.player1Score) && (gameManager.player4Score > gameManager.player3Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 4";
            secondRankSpot2.GetComponent<Text>().text = "Player 4";

            secondRankScore.text = " -  " + gameManager.player4Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player4Score.ToString();

            secondPlace.text = "Player 4";
            secondPlaceScore.text = gameManager.player4Score.ToString();
        }   
        else if (gameManager.player4Score > gameManager.player3Score && ((gameManager.player4Score > gameManager.player1Score) && (gameManager.player4Score > gameManager.player2Score)))
        {
            secondRankSpot.GetComponent<Text>().text = "Player 4";
            secondRankSpot2.GetComponent<Text>().text = "Player 4";

            secondRankScore.text = " -  " + gameManager.player4Score.ToString();
            secondRankScore2.text = " -  " + gameManager.player4Score.ToString();

            secondPlace.text = "Player 4";
            secondPlaceScore.text = gameManager.player4Score.ToString();
        }
        //Player four: Third Place
        else if (gameManager.player4Score < gameManager.player1Score && ((gameManager.player4Score < gameManager.player2Score) && (gameManager.player4Score > gameManager.player3Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 4";
            thirdRankSpot2.GetComponent<Text>().text = "Player 4";

            thirdRankScore.text = " -  " + gameManager.player4Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player4Score.ToString();

            thirdPlace.text = "Player 4";
            thirdPlaceScore.text = gameManager.player4Score.ToString();
        }    
        else if (gameManager.player4Score < gameManager.player1Score && ((gameManager.player4Score > gameManager.player2Score) && (gameManager.player4Score < gameManager.player3Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 4";
            thirdRankSpot2.GetComponent<Text>().text = "Player 4";

            thirdRankScore.text = " -  " + gameManager.player4Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player4Score.ToString();

            thirdPlace.text = "Player 4";
            thirdPlaceScore.text = gameManager.player4Score.ToString();
        } 
        else if (gameManager.player4Score > gameManager.player1Score && ((gameManager.player4Score < gameManager.player2Score) && (gameManager.player4Score < gameManager.player3Score)))
        {
            thirdRankSpot.GetComponent<Text>().text = "Player 4";
            thirdRankSpot2.GetComponent<Text>().text = "Player 4";

            thirdRankScore.text = " -  " + gameManager.player4Score.ToString();
            thirdRankScore2.text = " -  " + gameManager.player4Score.ToString();

            thirdPlace.text = "Player 4";
            thirdPlaceScore.text = gameManager.player4Score.ToString();
        }
        //Player four: Fourth Place
        else if (gameManager.player4Score < gameManager.player1Score && ((gameManager.player4Score < gameManager.player2Score) && (gameManager.player4Score < gameManager.player3Score)))
        {
            fourthRankSpot.GetComponent<Text>().text = "Player 4";
            fourthRankSpot2.GetComponent<Text>().text = "Player 4";

            fourthRankScore.text = " -  " + gameManager.player4Score.ToString();
            fourthRankScore2.text = " -  " + gameManager.player4Score.ToString();

            fourthPlace.text = "Player 4";
            fourthPlaceScore.text = gameManager.player4Score.ToString();
        }
        //Player four: Has 0 points
        else
        {
            fourthRankSpot.GetComponent<Text>().text = "Player 4";
            fourthRankSpot2.GetComponent<Text>().text = "Player 4";

            fourthRankScore.text = " -  " + gameManager.player4Score.ToString();
            fourthRankScore2.text = " -  " + gameManager.player4Score.ToString();

            fourthPlace.text = "Player 4";
            fourthPlaceScore.text = gameManager.player4Score.ToString();
        }
    }
#endregion
}
