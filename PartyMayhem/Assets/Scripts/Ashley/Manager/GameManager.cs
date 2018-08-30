using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerProfile[] profiles;
    public Transform[] startPositions;
    public int playerCount;

    private void Awake()
    {
        KeepGameManager();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void SpawnPlayers() //Use to spawn in players to scene.
    {
        startPositions = GameObject.FindGameObjectWithTag("StartPositions").GetComponentsInChildren<Transform>();

        if (profiles != null && startPositions != null)
        {
            for (int i = 1; i < profiles.Length; i++)
            {
                profiles[i].UpdateCharacterChoice();
                Instantiate(profiles[i].player, startPositions[i]);
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