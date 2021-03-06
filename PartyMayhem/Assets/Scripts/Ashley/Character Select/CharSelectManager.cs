﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharSelectManager : MonoBehaviour
{
    private GameManager manager;
    private PlayerProfile[] players;
    public Button startButton;
    public EventSystem eventSystem;

    public AudioSource music;
    public AudioSource boopSound;
    public AudioSource selectSound;
    public AudioSource cancelSound;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        players = GameObject.FindGameObjectWithTag("GameManager").GetComponents<PlayerProfile>();
        eventSystem = EventSystem.current;
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

        if (playerCount > 0 && !manager.tournamentMode)
            return true;

        if (playerCount > 1 && manager.tournamentMode)
            return true;

        else
            return false;
    }
}