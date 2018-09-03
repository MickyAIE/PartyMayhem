using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectManager : MonoBehaviour
{
    private PlayerProfile[] players;

    private void Awake()
    {
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