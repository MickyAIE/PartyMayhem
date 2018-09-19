using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

    public GameObject[] Player;
    [HideInInspector]
    public float BoostedSpeed; //This is the speed the player will be boosted to.
    public float DefaultSpeed; //This is the default running speed the player is set at.
    public CheckPoints CheckPointScripts;
    public GameManager manager;
    

    public void Start()
    {
        BoostedSpeed = 200 * 3;
        DefaultSpeed = 200;
        Player = GameObject.FindGameObjectsWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D SpeedCollision)
    {
        if (SpeedCollision.gameObject.tag == "Player") //When a Player collides with the speed pad it triggers a boost in the players speed and sends a Debug Log Message.
        {
            SpeedCollision.GetComponent<PlayerMovement>().speed = BoostedSpeed;
            //.GetComponent<PlayerMovement>().playerScore = 25;
            if (CheckPointScripts.CurrentLeader == "Player 1") { Debug.Log("Awarded Points to Player 1"); manager.player1Score += 25;}
            if (CheckPointScripts.CurrentLeader == "Player 2") { Debug.Log("Awarded Points to Player 2"); manager.player2Score += 25;}
            if (CheckPointScripts.CurrentLeader == "Player 3") { Debug.Log("Awarded Points to Player 3"); manager.player3Score += 25;}
            if (CheckPointScripts.CurrentLeader == "Player 4") { Debug.Log("Awarded Points to Player 4"); manager.player4Score += 25;}
        }
    }
}
