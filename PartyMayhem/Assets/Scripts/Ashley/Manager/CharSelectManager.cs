using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectManager : MonoBehaviour
{
    private GameManager manager;
    private PlayerProfile[] players;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        players = GameObject.FindGameObjectWithTag("GameManager").GetComponents<PlayerProfile>();
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