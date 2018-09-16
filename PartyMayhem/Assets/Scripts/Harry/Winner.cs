using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour {

    public RacingGameManager RacingGameManager;
    public GameManager GameManager;

    void Start () {
        RacingGameManager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<RacingGameManager>();
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject == RacingGameManager.Players[0].gameObject && RacingGameManager.Laps1 >= GameManager.gameLaps) {RacingGameManager.Player1Wins = true; Debug.Log("Player1Wins"); } else { return; }// if this gameobject is the gameobject that is listed
        if (this.gameObject == RacingGameManager.Players[1].gameObject && RacingGameManager.Laps1 >= GameManager.gameLaps) {RacingGameManager.Player2Wins = true; Debug.Log("Player2Wins"); } else { return; }//as [1] in the players array & has past 3 laps then 
        if (this.gameObject == RacingGameManager.Players[2].gameObject && RacingGameManager.Laps1 >= GameManager.gameLaps) {RacingGameManager.Player3Wins = true; Debug.Log("Player3Wins"); } else { return; }//set the correct players winning bool to true
        if (this.gameObject == RacingGameManager.Players[3].gameObject && RacingGameManager.Laps1 >= GameManager.gameLaps) {RacingGameManager.Player4Wins = true; Debug.Log("Player4Wins"); } else { return; }// which configures the correct winners text on screen

    }
}
