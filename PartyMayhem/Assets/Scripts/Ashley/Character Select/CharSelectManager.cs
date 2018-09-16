using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelectManager : MonoBehaviour
{
    private PlayerProfile[] players;
    public Button startButton;

    private void Awake()
    {
        players = GameObject.FindGameObjectWithTag("GameManager").GetComponents<PlayerProfile>();
    }

    private void Update()
    {
        if (CanStartGame())
            startButton.interactable = true;
        else
            startButton.interactable = false;
    }

    public bool CanStartGame()
    {
        int playerCount = 0;
        foreach (PlayerProfile player in players)
        {
            if (player.playerPrefab != null)
            {
                playerCount++;
            }
        }

        if (playerCount == 0)
            return false;
        else
            return true;
    }
}