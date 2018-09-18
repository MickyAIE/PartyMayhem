using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour {

    public string CurrentLeader;
    
    void OnTriggerEnter2D(Collider2D CheckPointUpdate)
    {
        if (CheckPointUpdate.tag !="Player") //If a gameobject enteres the collision and doesnt have the player tag, do not continue
            return;

        if (transform == LapsCounter.NextCheckpoint[LapsCounter.currentCheckpoint].transform) //If this checkpoints transform is equal to NextCheckpoint's transform (which is always the descending through the Array containing all 4 checkpoints)
        {
            CurrentLeader = CheckPointUpdate.gameObject.name;
            Debug.Log(CurrentLeader);
            if (LapsCounter.currentCheckpoint + 1 < LapsCounter.NextCheckpoint.Length)//Check so the current checkpoint counter is less than the next checkpoint.
            {                 
                if (LapsCounter.currentCheckpoint == 0)//Add to currentLap if currentCheckpoint is
                    LapsCounter.currentLap++; //Adds a 1 to the current lap. When the scene begins the Lap counter is at 0 because the players start just before the start line.
                LapsCounter.currentCheckpoint++; //Changes the current checkpoint to the next checkpoint (kind of a confusing sentence but it just moves down the array)               
            }
            else
            {
                //If we dont have any Checkpoints set currentcheckpoint to 0
                LapsCounter.currentCheckpoint = 0;
            }
        }
    }
}
