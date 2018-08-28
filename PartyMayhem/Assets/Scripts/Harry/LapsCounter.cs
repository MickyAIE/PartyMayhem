using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapsCounter : MonoBehaviour {

    public Transform[] checkPointArray; //This array contains the 4 total checkpoints including the starting line
    public static Transform[] NextCheckpoint; //The next checkpoint in descending order.
    public static int currentCheckpoint = 0;
    public static int currentLap = 0;
    public int Lap;
    public Text LapCounter;

    void Start()
    {
        currentCheckpoint = 0;
        currentLap = 0;
    }

    void Update()
    {
        Lap = currentLap;
        NextCheckpoint = checkPointArray;
        LapCounter.text = Lap + "/3";
    }    
}
