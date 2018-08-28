using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RacingGameManager : MonoBehaviour {

    public int Laps;
    public Text LapCounter;
    public GameObject[] Players;

    void Start() {
        Players = GameObject.FindGameObjectsWithTag("Player");
        Players[0].AddComponent(typeof(LapsCounter));
        if (Players.Length >= 0) { Players[1].AddComponent(typeof(LapsCounter)); }
        if (Players.Length >= 1) { Players[2].AddComponent(typeof(LapsCounter)); }
        if (Players.Length >= 2) { Players[3].AddComponent(typeof(LapsCounter)); }
        Laps = GameObject.FindGameObjectWithTag("Player").GetComponent<LapsCounter>().Lap;
    }

    void Update () {
        Players = GameObject.FindGameObjectsWithTag("Player");
        Laps = GameObject.FindGameObjectWithTag("Player").GetComponent<LapsCounter>().Lap;
        LapCounter.text = Laps + "/3";
        if (Laps >= 3)
        {
            Time.timeScale = 0.25F;
            Invoke("ResetTimeScale", 2);
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
