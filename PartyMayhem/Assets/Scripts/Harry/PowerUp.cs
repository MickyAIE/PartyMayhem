using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public GameObject[] Players;
    public bool SpeedModifier;
    public float SpeedChange;

	// Use this for initialization
	void Start () {
        Players = GameObject.FindGameObjectsWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {

	}

    public void OnTriggerEnter2D(Collider2D PowerUpCollision)
    {
        //Player = SpeedCollision.gameObject;
        /*if (PowerUpCollision.gameObject.tag != "Player") { return; }
        if (PowerUpCollision.gameObject.tag == "Player") //When a Player collides with the speed pad it triggers a boost in the players speed
        {
            
        }*/
    }
}
