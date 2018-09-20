using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;


public class RacingGameManager : MonoBehaviour {

    public float Countdown = 3.0f; //Used to control the timer at the start to countdown from 3.
    public int Laps1; //used to control the total amount of laps required to win the race
    public bool MiddleTextCleared;
    public bool PointsAwarded;
    public Color StartingColor; //it took all my mental fortitude to not type this the Australian way.
    public Color EndColor; //Starting color and end color are used to transition the guidance arrows to fade away
    public Text LapCounter1;
    public Text MiddleText;
    public GameObject playerInfoPrefab;
    public GameObject[] playerInfoPositions;
    public GameObject player1Info;
    public GameObject player2Info;
    public GameObject player3Info;
    public GameObject player4Info;
    public Component[] playerProfiles;
    public GameObject[] Players;
    public GameObject[] Guides;
    public CheckPoints CheckPointScripts;
    public GameManager manager;
//    public PlayerMovement PlayerMovement;

    public void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();           
    }

    void Start() {
        playerProfiles = manager.GetComponents(typeof(PlayerProfile));
        manager.SpawnPlayers();
        Guides = GameObject.FindGameObjectsWithTag("Guide");
        Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Player in Players)
        {
            Player.AddComponent(typeof(LapsCounter));           
            Player.GetComponent<PlayerMovement>().enabled = false;
            if (Player == Players[0]) Player.gameObject.name = "Player 1";
            else if (Player == Players[1]) Player.gameObject.name = "Player 2";
            else if (Player == Players[2]) Player.gameObject.name = "Player 3";
            else if (Player == Players[3]) Player.gameObject.name = "Player 4";
        }
        Laps1 = GameObject.FindGameObjectWithTag("Player").GetComponent<LapsCounter>().Lap;
        StartingColor = Guides[0].GetComponent<SpriteRenderer>().color;
        EndColor = new Color(StartingColor.r, StartingColor.g, StartingColor.b, 0f);
        MiddleTextCleared = true;
        PointsAwarded = true;
        manager.player1Score += 25;
        manager.player2Score += 25;
        manager.player3Score += 25;
        manager.player4Score += 25;
        if (manager.ActivePlayerCount() == 1)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
        }
        else if (manager.ActivePlayerCount() == 2)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
            player2Info = Instantiate(playerInfoPrefab, playerInfoPositions[1].transform.position, Quaternion.identity, playerInfoPositions[1].transform);
            player2Info.transform.GetChild(3).GetComponent<Text>().text = "Player 2";
        }
        else if (manager.ActivePlayerCount() == 3)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
            player2Info = Instantiate(playerInfoPrefab, playerInfoPositions[1].transform.position, Quaternion.identity, playerInfoPositions[1].transform);
            player2Info.transform.GetChild(3).GetComponent<Text>().text = "Player 2";
            player3Info = Instantiate(playerInfoPrefab, playerInfoPositions[2].transform.position, Quaternion.identity, playerInfoPositions[2].transform);
            player3Info.transform.GetChild(3).GetComponent<Text>().text = "Player 3";
        }
        else if (manager.ActivePlayerCount() == 4)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
            player2Info = Instantiate(playerInfoPrefab, playerInfoPositions[1].transform.position, Quaternion.identity, playerInfoPositions[1].transform);
            player2Info.transform.GetChild(3).GetComponent<Text>().text = "Player 2";
            player3Info = Instantiate(playerInfoPrefab, playerInfoPositions[2].transform.position, Quaternion.identity, playerInfoPositions[2].transform);
            player3Info.transform.GetChild(3).GetComponent<Text>().text = "Player 3";
            player4Info = Instantiate(playerInfoPrefab, playerInfoPositions[3].transform.position, Quaternion.identity, playerInfoPositions[3].transform);
            player4Info.transform.GetChild(3).GetComponent<Text>().text = "Player 4";
        }
    }

    void Update () {
        Laps1 = Players[0].GetComponent<LapsCounter>().Lap;
        LapCounter1.text = Laps1 + "/" + manager.gameLaps;
        PlayerInfos();
        foreach (GameObject Guide in Guides)
        {           
            Guide.GetComponent<SpriteRenderer>().material.color = Color.Lerp(StartingColor, EndColor, Time.time/3f);            
        }
        if (Laps1 >= manager.gameLaps)
        {
            Time.timeScale = 0.5F;
            Invoke("ResetTimeScale", 1);
            MiddleText.text = CheckPointScripts.CurrentLeader + " Has Won!";
            if (CheckPointScripts.CurrentLeader == "Player 1" && PointsAwarded == true) { Debug.Log("Awarded Winner Points to Player 1"); manager.player1Score += 500; PointsAwarded = false; }
            if (CheckPointScripts.CurrentLeader == "Player 2" && PointsAwarded == true) { Debug.Log("Awarded Winner Points to Player 2"); manager.player2Score += 500; PointsAwarded = false; }
            if (CheckPointScripts.CurrentLeader == "Player 3" && PointsAwarded == true) { Debug.Log("Awarded Winner Points to Player 3"); manager.player3Score += 500; PointsAwarded = false; }
            if (CheckPointScripts.CurrentLeader == "Player 4" && PointsAwarded == true) { Debug.Log("Awarded Winner Points to Player 4"); manager.player4Score += 500; PointsAwarded = false; }
            Invoke("BackToMainMenu", 3);
        }
        Countdown -= Time.deltaTime;
        if (Countdown < 3f && MiddleTextCleared == true)
        {
            MiddleText.text = "3";
        }
        if (Countdown < 2f && MiddleTextCleared == true)
        {
            MiddleText.text = "2";
        }
        if (Countdown < 1f && MiddleTextCleared == true)
        {
            MiddleText.text = "1";
        }
        if (Countdown < 0f && MiddleTextCleared == true)
        {
            MiddleText.text = "Go!";
            foreach (GameObject Player in Players)
            { 
            Player.GetComponent<PlayerMovement>().enabled = true;
            }
        }
        if (Countdown < -0.5f && MiddleTextCleared == true)
        {
            MiddleText.text = "";
            MiddleTextCleared = false;
        }
    }
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
    public void BackToMainMenu()
    {
        //manager.returningToMenus = true;
        SceneManager.LoadScene("Menus");
        if (PlayerPrefs.GetInt("Mode") == 2 && manager.currentRound == manager.rounds)
            manager.returningToMenus = false;
        else
            manager.returningToMenus = true;

        if (PlayerPrefs.GetInt("Mode") == 2) manager.currentRound += 1;
    }
    public void PlayerInfos()
    {
        if (PlayerPrefs.GetInt("Mode") == 2)
        {
            if (player1Info != null)
                player1Info.transform.GetChild(2).GetComponent<Text>().text = manager.player1Score.ToString();
            if (player2Info != null)
                player2Info.transform.GetChild(2).GetComponent<Text>().text = manager.player2Score.ToString();
            if (player3Info != null)
                player3Info.transform.GetChild(2).GetComponent<Text>().text = manager.player3Score.ToString();
            if (player4Info != null)
                player4Info.transform.GetChild(2).GetComponent<Text>().text = manager.player4Score.ToString();
        }
        else
        {
            if (player1Info != null)
                player1Info.transform.GetChild(2).gameObject.SetActive(false);
            if (player2Info != null)
                player2Info.transform.GetChild(2).gameObject.SetActive(false);
            if (player3Info != null)
                player3Info.transform.GetChild(2).gameObject.SetActive(false);
            if (player4Info != null)
                player4Info.transform.GetChild(2).gameObject.SetActive(false);
        }

        if (player1Info != null)
        {
            foreach (PlayerProfile pp in playerProfiles)
            {
                if (pp.playerNumber == 1)
                    player1Info.transform.GetChild(1).GetComponent<Image>().sprite = pp.playerPortrait;
            }
        }
        if (player2Info != null)
        {
            foreach (PlayerProfile pp in playerProfiles)
            {
                if (pp.playerNumber == 2)
                    player2Info.transform.GetChild(1).GetComponent<Image>().sprite = pp.playerPortrait;
            }
        }
        if (player3Info != null)
        {
            foreach (PlayerProfile pp in playerProfiles)
            {
                if (pp.playerNumber == 3)
                    player3Info.transform.GetChild(1).GetComponent<Image>().sprite = pp.playerPortrait;
            }
        }
        if (player4Info != null)
        {
            foreach (PlayerProfile pp in playerProfiles)
            {
                if (pp.playerNumber == 4)
                    player4Info.transform.GetChild(1).GetComponent<Image>().sprite = pp.playerPortrait;
            }
        }
    }
}
