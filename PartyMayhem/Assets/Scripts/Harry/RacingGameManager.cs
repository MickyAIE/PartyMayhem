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
    public Text CurrentLeaderText;
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
        CurrentLeaderText.text = "";
        Laps1 = GameObject.FindGameObjectWithTag("Player").GetComponent<LapsCounter>().Lap;
        StartingColor = Guides[0].GetComponent<SpriteRenderer>().color;
        EndColor = new Color(StartingColor.r, StartingColor.g, StartingColor.b, 0f);
        MiddleTextCleared = true;
        PointsAwarded = true;
    }

    void Update () {
        Laps1 = Players[0].GetComponent<LapsCounter>().Lap;
        LapCounter1.text = Laps1 + "/" + manager.gameLaps + "Laps";

        foreach (GameObject Guide in Guides)
        {           
            Guide.GetComponent<SpriteRenderer>().material.color = Color.Lerp(StartingColor, EndColor, Time.time/3f);            
        }
        if (Laps1 >= manager.gameLaps)
        {
            Time.timeScale = 0.5F;
            Invoke("ResetTimeScale", 1);
            MiddleText.text = CheckPointScripts.CurrentLeader + " Has Won!";
            if (CheckPointScripts.CurrentLeader == "Player 1" && PointsAwarded == true) { Debug.Log("Awarded Points to Player 1"); manager.player1Score += 500; PointsAwarded = false; }
            if (CheckPointScripts.CurrentLeader == "Player 2" && PointsAwarded == true) { Debug.Log("Awarded Points to Player 2"); manager.player2Score += 500; PointsAwarded = false; }
            if (CheckPointScripts.CurrentLeader == "Player 3" && PointsAwarded == true) { Debug.Log("Awarded Points to Player 3"); manager.player3Score += 500; PointsAwarded = false; }
            if (CheckPointScripts.CurrentLeader == "Player 4" && PointsAwarded == true) { Debug.Log("Awarded Points to Player 4"); manager.player4Score += 500; PointsAwarded = false; }
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
}
