using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class RacingGameManager : MonoBehaviour {

    public float Countdown = 3.0f; //Used to control the timer at the start to countdown from 3.
    public int Laps1; //used to control the total amount of laps required to win the race
    public bool MiddleTextCleared;
    public bool Player1Wins;
    public bool Player2Wins;
    public bool Player3Wins;
    public bool Player4Wins;
    public Color StartingColor; //it took all my mental fortitude to not type this the Australian way.
    public Color EndColor; //Starting color and end color are used to transition the guidance arrows to fade away
    public Text LapCounter1;
    public Text MiddleText;
    public GameObject[] Players;
    public GameObject[] Guides;
    public GameManager manager;
    public PlayerMovement PlayerMovement;

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
            Player.AddComponent(typeof(Winner));
            Player.GetComponent<PlayerMovement>().enabled = false;
        }
        Laps1 = GameObject.FindGameObjectWithTag("Player").GetComponent<LapsCounter>().Lap;

        StartingColor = Guides[0].GetComponent<SpriteRenderer>().color;
        EndColor = new Color(StartingColor.r, StartingColor.g, StartingColor.b, 0f);
        MiddleTextCleared = true;
        Player1Wins = false;
        Player2Wins = false;
        Player3Wins = false;
        Player4Wins = false;
    }

    void Update () {
        Laps1 = Players[0].GetComponent<LapsCounter>().Lap;
        LapCounter1.text = Laps1 + "/" + manager.gameLaps;

        foreach (GameObject Guide in Guides)
        {           
            Guide.GetComponent<SpriteRenderer>().material.color = Color.Lerp(StartingColor, EndColor, Time.time/3f);            
        }
        if (Laps1 >= manager.gameLaps)
        {
            Time.timeScale = 0.5F;
            Invoke("ResetTimeScale", 1);
            if (Player1Wins == true) { MiddleText.text = "Player 1 Wins"; Invoke("BackToMainMenu", 3); }
            if (Player2Wins == true) { MiddleText.text = "Player 2 Wins"; Invoke("BackToMainMenu", 3); }
            if (Player3Wins == true) { MiddleText.text = "Player 3 Wins"; Invoke("BackToMainMenu", 3); }
            if (Player4Wins == true) { MiddleText.text = "Player 4 Wins"; Invoke("BackToMainMenu", 3); }
            //MiddleText.text = "Race Over!"; //TODO Figure out a way to display that it was this player that won (IE PLAYER 1 instead of the gameobjects name)
            //AllLapsCompleted = false;
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
        manager.returningToMenus = true;
        SceneManager.LoadScene("Menus");
    }
}
