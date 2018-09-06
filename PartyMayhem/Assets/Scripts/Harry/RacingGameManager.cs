using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RacingGameManager : MonoBehaviour {

    public float Countdown = 30.0f;
    public int Laps;
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
    }

    void Update () {
        Laps = GameObject.FindGameObjectWithTag("Player").GetComponent<LapsCounter>().Lap;
        LapCounter.text = Laps + "/3";
        if (Laps >= 3)
        {
            Time.timeScale = 0.25F;
            Invoke("ResetTimeScale", 0.5f);
            MiddleText.text = "The Winner Is" + this.gameObject.name; //TODO Figure out a way to display that it was this player that won (IE PLAYER 1 instead of the gameobjects name)
        }
        Countdown -= Time.deltaTime;
        if (Countdown < 3)
        {
            MiddleText.text = "3";
        }
        if (Countdown < 2)
        {
            MiddleText.text = "2";
        }
        if (Countdown < 1)
        {
            MiddleText.text = "1";
        }
        if (Countdown < 0)
        {
            MiddleText.text = "Go!";
            foreach (GameObject Player in Players)
            { 
            Player.GetComponent<PlayerMovement>().enabled = true;
            }
        }
        if (Countdown < -0.5)
        {
            MiddleText.text = "";
        }
    }
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }

    public void ApplyScripts()
    {

    }
}
