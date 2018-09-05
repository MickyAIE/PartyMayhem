using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelectSlot : MonoBehaviour
{
    //This script is attached to each player's character select slot in the Character Select Screen

    public GameManager manager;

    private PlayerProfile player; //the player profile associated with this player slot
    public PlayerProfile[] players; //all player profiles attached to the GameManager
    public int slotNumber = 1; //set in inspector for each player slot, 1 by default
    public bool choiceConfirmed = false; //has player selected a character?

    public Sprite[] doggos; //every flavour of dog
    public Sprite[] shibes; //every flavour of shiba inu
    private int characterIdx; //number determining player's character
    private int flavourIdx; //number determining player's flavour

    public GameObject[] prefabs; //every prefab
    public GameObject playerPrefab; //prefab to be spawned by PlayerProfile

    public Image playerSlot; //square where character portrait is displayed, set in inspector
    public Image currentPortrait; //Image displayed on character select screen, set in inspector

    private float buttonTimer = 0;
    private readonly float buttonDelay = 0.3f;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        players = GameObject.FindGameObjectWithTag("GameManager").GetComponents<PlayerProfile>();

        player = players[slotNumber - 1];
    }

    private void Start()
    {
        if (slotNumber == 1)
        {
            player.isActive = true;
        }

        if (player.isActive == false)
        {
            playerSlot.color = Color.black;
            currentPortrait.color = Color.clear;
        }
    }

    private void Update()
    {
        PortraitToBeDisplayed();
        Inputs();
    }

    private void PortraitToBeDisplayed() //Determines portrait to display in the player's square on the character select screen.
    {
        if (characterIdx == 0)
            currentPortrait.sprite = doggos[flavourIdx];
        else if (characterIdx == 1)
            currentPortrait.sprite = shibes[flavourIdx];
    }

    //TO DO: Add back input for all 4 players, pressing B affects all slots.
    private void Inputs()
    {
        buttonTimer += Time.deltaTime;

        if (buttonTimer > buttonDelay)
        {
            if (Input.anyKey || Input.GetAxis("P" + slotNumber + " Vertical") != 0 || Input.GetAxis("P" + slotNumber + " Horizontal") != 0)
            {
                buttonTimer = 0;
            }

            if (Input.GetButtonDown("P" + slotNumber + " Punch"))
            {
                if (player.isActive)
                {
                    if (!choiceConfirmed)
                        SelectCharacter();
                }
                else
                {
                    ActivatePlayerProfile();
                }
            }

            if (Input.GetKey(KeyCode.B))
            {
                if (player.isActive)
                {
                    if (choiceConfirmed)
                    {
                        UndoCharacterChoice();
                    }
                    else
                    {
                        DeactivatePlayerProfile();
                    }
                }
            }

            if (player.isActive && !choiceConfirmed)
            {
                if (Input.GetAxis("P" + slotNumber + " Vertical") > 0)
                {
                    if (flavourIdx > 0)
                        flavourIdx--;
                    else
                        flavourIdx = doggos.Length - 1;
                }

                if (Input.GetAxis("P" + slotNumber + " Vertical") < 0)
                {
                    if (flavourIdx < doggos.Length - 1)
                        flavourIdx++;
                    else
                        flavourIdx = 0;
                }

                if (Input.GetAxis("P" + slotNumber + " Horizontal") > 0)
                {
                    if (characterIdx == 0)
                        characterIdx = 1;
                    else
                        characterIdx = 0;
                }

                if (Input.GetAxis("P" + slotNumber + " Horizontal") < 0)
                {
                    if (characterIdx == 0)
                        characterIdx = 1;
                    else
                        characterIdx = 0;
                }
            }
        }
    }
    
    private void ActivatePlayerProfile() //Sets corresponding player profile's current active status to true and shows portrait in menu.
    {
        player.isActive = true;
        playerSlot.color = Color.white;
        currentPortrait.color = Color.white;
    }

    private void DeactivatePlayerProfile() //Sets corresponding player profile's current active status to false and hides portrait in menu.
    {
        player.isActive = false;
        playerSlot.color = Color.black;
        currentPortrait.color = Color.clear;
    }

    private void SelectCharacter() //Select character with punch button.
    {
        if (IsCharacterAvailable())
        {
            choiceConfirmed = true;
            playerSlot.color = Color.green;

            SetCharacterChoice();
            DisplayUnselectableCharacter();
        }
    }

    private bool IsCharacterAvailable() //Checks if someone has already chosen character.
    {
        foreach (PlayerProfile player in players)
        {
            if (player.playerPrefab != null && player.isActive == true)
            {
                if (player.playerPrefab == ChosenCharacter())
                    return false;
            }
        }
        return true;
    }

    private void SetCharacterChoice() //Sends choice to appropriate PlayerProfile.
    {
        player.playerPrefab = ChosenCharacter();
        player.playerPortrait = currentPortrait.sprite;
    }

    private void DisplayUnselectableCharacter() //Greys out portrait if it has been chosen by another player.
    {
        foreach (PlayerProfile p in players)
        {
            if (p.playerPortrait != null && p.isActive == true)
            {
                if (p.playerPortrait == currentPortrait.sprite && p.playerNumber != slotNumber)
                {
                    currentPortrait.color = new Color(0, 0, 0, 0.5f);
                }
            }
        }
    }

    private GameObject ChosenCharacter() //Uses the player's chosen character and flavour to determine prefab to spawn.
    {
        if (characterIdx == 0)
        {
            playerPrefab = prefabs[flavourIdx];
        }
        else if (characterIdx == 1)
        {
            playerPrefab = prefabs[flavourIdx + shibes.Length];
        }

        return playerPrefab;
    }

    private void UndoCharacterChoice() //Undoes your confirmed choice.
    {
        player.playerPrefab = null;
        player.playerPortrait = null;
        choiceConfirmed = false;
        playerSlot.color = Color.white;
        ReenableUnselectableCharacter();
    }

    private void ReenableUnselectableCharacter() //Undoes greying out of portraits.
    {
        foreach (PlayerProfile p in players)
        {
            if (p.playerPortrait != null && p.isActive == true)
            {
                if (p.playerPortrait == currentPortrait.sprite && p.playerNumber != slotNumber)
                {
                    currentPortrait.color = Color.white;
                }
            }
        }
    }
}