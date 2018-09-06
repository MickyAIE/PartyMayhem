using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D CheckPointUpdate)
    {
        //Is it the Player who enters the collider?
        if (CheckPointUpdate.tag !="Player")
            return; //If it's not the player dont continue

        if (transform == LapsCounter.NextCheckpoint[LapsCounter.currentCheckpoint].transform) //If this checkpoints transform is equal to NextCheckpoint's transform (which is always the descending through the Array containing all 4 checkpoints)
        {
            //Check so the current checkpoint counter is less than the next checkpoint.
            if (LapsCounter.currentCheckpoint + 1 < LapsCounter.NextCheckpoint.Length)
            {
                //Add to currentLap if currentCheckpoint is 
                if (LapsCounter.currentCheckpoint == 0)
                    LapsCounter.currentLap++; //Adds a 1 to the current lap. When the scene begins the Lap counter is at 0 because the players start just before the start line.
                LapsCounter.currentCheckpoint++; //Changes the current checkpoint to the next checkpoint (kind of a confusing sentence but it just moves down the array)
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                LapsCounter.currentCheckpoint = 0;
            }
        }
    }
}
