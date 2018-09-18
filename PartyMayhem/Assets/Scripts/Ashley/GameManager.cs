using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public PlayerProfile[] profiles;
    public int activePlayerCount;
    public string minigameToLoad;

    public int gameTimer;
    public int gameLaps;
    public int difficultyIndex;
    
    public enum Mode
    {
        Board,
        Tournament,
        Freeplay
    };
    public Mode mode;

    public bool returningToMenus = false;

    public int player1Score;
    public int player2Score;
    public int player3Score;
    public int player4Score;

    public int rounds;
    public int currentRound;

    private Transform[] startPositions;

    private void Start()
    {
        KeepGameManager();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            returningToMenus = false;
            PlayerPrefs.SetInt("activePlayers", 0);

            Application.Quit();
        }
    }

    public int ActivePlayerCount() //Number of profiles active.
    {
        int i = 0;

        foreach (PlayerProfile profile in profiles)
        {
            if (profile.isActive)
            {
                i++;
            }
        }

        if (i >= PlayerPrefs.GetInt("activePlayers"))
            PlayerPrefs.SetInt("activePlayers", i);

        return i;
    }

    public void SpawnPlayers() //Use to spawn in players to scene.
    {
        startPositions = GameObject.FindGameObjectWithTag("StartPositions").GetComponentsInChildren<Transform>();

        for (int i = 0; i < ActivePlayerCount(); i++)
        {
            if (profiles[i].isActive)
            {
                if (profiles != null && startPositions != null)
                {
                    Instantiate(profiles[i].playerPrefab, startPositions[i + 1]);
                    profiles[i].UpdatePlayerNumbers();
                }
            }
        }
    }

    private void KeepGameManager() //Keeps the GameManager across scenes.
    {
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}