using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostOff : MonoBehaviour {
    public GameObject[] Player;
    [HideInInspector]
    public float BoostedSpeed; //This is the speed the player will be boosted to.
    public float DefaultSpeed; //This is the default running speed the player is set at.


    public void Start()
    {
        BoostedSpeed = 200 * 3;
        DefaultSpeed = 200;
        Player = GameObject.FindGameObjectsWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D SpeedCollision)
    {
        if (SpeedCollision.gameObject.tag == "Player") //When a Player collides with the speed pad it triggers a boost in the players speed and sends a Debug Log Message.
        {
            SpeedCollision.GetComponent<PlayerMovement>().speed = DefaultSpeed;
        }
    }
}
