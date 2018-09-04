using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    //There are four of these attached to the GameManager, one for every player.

    public GameManager gameManager;

    public GameObject playerPrefab; //prefab to spawn in minigames
    public Sprite playerPortrait; //portrait to display

    public int playerNumber; //set in inspector
    public bool isActive; //set in inspector

    public float score; //does nothing yet, store player scores here

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void UpdatePlayerNumbers()
    {
        if (playerPrefab != null)
        {
            playerPrefab.GetComponent<PlayerMovement>().playerNumber = playerNumber.ToString();
            playerPrefab.GetComponent<CharacterMoveTransitions>().playerNumber = playerNumber;
        }
    }
}