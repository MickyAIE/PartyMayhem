using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerProfile[] profiles;
    public int activePlayerCount;

    private Transform[] startPositions;

    private void Awake()
    {
        KeepGameManager();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
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

        return i;
    }

    public void SpawnPlayers() //Use to spawn in players to scene.
    {
        startPositions = GameObject.FindGameObjectWithTag("StartPositions").GetComponentsInChildren<Transform>();

        if (profiles != null && startPositions != null)
        {
            for (int i = 0; i < ActivePlayerCount(); i++)
            {
                if (profiles[i].isActive)
                    Instantiate(profiles[i].playerPrefab, startPositions[i + 1]);
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