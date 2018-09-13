using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour {

    public RacingGameManager RacingGameManager;

    void Start () {
        RacingGameManager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<RacingGameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject == RacingGameManager.Players[0] && RacingGameManager.Laps >= 3) {RacingGameManager.Player1Wins = true;} else { return; }// if this gameobject is the gameobject that is listed
        if (this.gameObject == RacingGameManager.Players[1] && RacingGameManager.Laps >= 3) { RacingGameManager.Player2Wins = true;} else { return; }//as [1] in the players array & has past 3 laps then 
        if (this.gameObject == RacingGameManager.Players[2] && RacingGameManager.Laps >= 3) { RacingGameManager.Player3Wins = true;} else { return; }//set the correct players winning bool to true
        if (this.gameObject == RacingGameManager.Players[3] && RacingGameManager.Laps >= 3) {RacingGameManager.Player4Wins = true; } else { return; }// which configures the correct winners text on screen

    }
}
