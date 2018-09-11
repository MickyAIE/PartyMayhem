using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RacingGameManager : MonoBehaviour {

    public float Countdown = 30.0f;
    public int Laps;
    public bool AllLapsCompleted;
    public bool MiddleTextCleared;
    public Text LapCounter;
    public Text MiddleText;
    public GameObject[] Players;
    public GameManager manager;
    public PlayerMovement PlayerMovement;

    public void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();           
    }

    void Start() {
        manager.SpawnPlayers();
        Players = GameObject.FindGameObjectsWithTag("Player");       
        foreach (GameObject Player in Players)
        {
            Player.AddComponent(typeof(LapsCounter));
            Player.GetComponent<PlayerMovement>().enabled = false;
        }
        Laps = GameObject.FindGameObjectWithTag("Player").GetComponent<LapsCounter>().Lap;
        AllLapsCompleted = true;
        MiddleTextCleared = true;
    }

    void Update () {
        Laps = GameObject.FindGameObjectWithTag("Player").GetComponent<LapsCounter>().Lap;
        LapCounter.text = Laps + "/3";
        if (AllLapsCompleted == false) { return; }
        if (Laps >= 3 && AllLapsCompleted == true)
        {
            Time.timeScale = 0.5F;
            Invoke("ResetTimeScale", 1);
            MiddleText.text = "Race Over!" //TODO Figure out a way to display that it was this player that won (IE PLAYER 1 instead of the gameobjects name)
            AllLapsCompleted = false;
        }
        Countdown -= Time.deltaTime;
        if (Countdown < 3 && MiddleTextCleared == true)
        {
            MiddleText.text = "3";
        }
        if (Countdown < 2 && MiddleTextCleared == true)
        {
            MiddleText.text = "2";
        }
        if (Countdown < 1 && MiddleTextCleared == true)
        {
            MiddleText.text = "1";
        }
        if (Countdown < 0 && MiddleTextCleared == true)
        {
            MiddleText.text = "Go!";
            foreach (GameObject Player in Players)
            { 
            Player.GetComponent<PlayerMovement>().enabled = true;
            }
        }
        if (Countdown < -0.5 && MiddleTextCleared == true)
        {
            MiddleText.text = "";
            MiddleTextCleared = false;
        }
    }
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }

    public void ResetMiddleText()
    {
        
    }
}
