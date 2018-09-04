using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RacingGameManager : MonoBehaviour {

    public int Laps;
    public Text LapCounter;
    public GameObject[] Players;
    public GameManager manager;

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
        }
        Laps = GameObject.FindGameObjectWithTag("Player").GetComponent<LapsCounter>().Lap;
    }

    void Update () {
        Players = GameObject.FindGameObjectsWithTag("Player");
        Laps = GameObject.FindGameObjectWithTag("Player").GetComponent<LapsCounter>().Lap;
        LapCounter.text = Laps + "/3";
        if (Laps >= 3)
        {
            Time.timeScale = 0.25F;
            Invoke("ResetTimeScale", 0.5f);
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
