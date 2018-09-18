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
        /*Debug.Log();
        Debug.Log(GameManager.profiles[1].name);
        Debug.Log(GameManager.profiles[2].name);
        Debug.Log(GameManager.profiles[3].name);*/
        if (this.gameObject.name == RacingGameManager.Players[0].name && RacingGameManager.Laps1 >= GameManager.gameLaps) {RacingGameManager.Player1Wins = true; Debug.Log("Player1Wins"); } else { return; }// if this gameobject is the gameobject that is listed
        if (this.gameObject.name == RacingGameManager.Players[1].name && RacingGameManager.Laps2 >= GameManager.gameLaps) {RacingGameManager.Player2Wins = true; Debug.Log("Player2Wins"); } else { return; }//as [1] in the players array & has past 3 laps then 
        if (this.gameObject.name == RacingGameManager.Players[2].name && RacingGameManager.Laps3 >= GameManager.gameLaps) {RacingGameManager.Player3Wins = true; Debug.Log("Player3Wins"); } else { return; }//set the correct players winning bool to true
        if (this.gameObject.name == RacingGameManager.Players[3].name && RacingGameManager.Laps4 >= GameManager.gameLaps) {RacingGameManager.Player4Wins = true; Debug.Log("Player4Wins"); } else { return; }// which configures the correct winners text on screen

    }
}
