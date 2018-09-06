using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LapsCounter : MonoBehaviour {

    public GameObject[] checkPointArray; //This array contains the 4 total checkpoints including the starting line
    public static GameObject[] NextCheckpoint; //The next checkpoint in descending order.
    public static int currentCheckpoint = 0;
    public static int currentLap = 0;
    public int Lap;
      
    void Start()
    {
        checkPointArray = GameObject.FindGameObjectsWithTag("CheckPoint").OrderBy (CheckPoints => CheckPoints.name ).ToArray(); //this adds the checkpoints to the array and sorts them by name.
        currentCheckpoint = 0;
        currentLap = 0;   
    }

    void Update()
    {
        Lap = currentLap;
        NextCheckpoint = checkPointArray;
    }    
}
