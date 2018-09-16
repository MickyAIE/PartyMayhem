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
        SpeedModifier = false;
        SpeedChange = 200f;
    }
	
	// Update is called once per frame
	void Update () {
		if (SpeedModifier == true)
        {
            if (SpeedChange >= 150f) { SpeedChange = SpeedChange + 2f; }
            if (SpeedChange <= 250f) { SpeedChange = SpeedChange - 2f; }
        }
	}

    public void OnTriggerEnter2D(Collider2D PowerUpCollision)
    {
        //Player = SpeedCollision.gameObject;
        if (PowerUpCollision.gameObject.tag != "Player") { return; }
        if (PowerUpCollision.gameObject.tag == "Player") //When a Player collides with the speed pad it triggers a boost in the players speed
        {
            SpeedModifier = true;
            if (PowerUpCollision.gameObject == Players[0]) { Invoke("SpeedModifierReset1", 2); }//this code was taken from the winner.cs script after i found out this code had flaws.
            if (PowerUpCollision.gameObject == Players[1]) { Invoke("SpeedModifierReset2", 2); }
            if (PowerUpCollision.gameObject == Players[2]) { Invoke("SpeedModifierReset3", 2); }
            if (PowerUpCollision.gameObject == Players[3]) { Invoke("SpeedModifierReset4", 2); } else { return; }
        }
    }
    public void SpeedModifierReset1()
    {
        SpeedModifier = false;
        Players[0].GetComponent<PlayerMovement>().speed = 200f;
        SpeedChange = 200f;
    }
    public void SpeedModifierReset2()
    {
        SpeedModifier = false;
        Players[0].GetComponent<PlayerMovement>().speed = 200f;
        SpeedChange = 200f;
    }
    public void SpeedModifierReset3()
    {
        SpeedModifier = false;
        Players[0].GetComponent<PlayerMovement>().speed = 200f;
        SpeedChange = 200f;
    }
    public void SpeedModifierReset4()
    {
        SpeedModifier = false;
        Players[0].GetComponent<PlayerMovement>().speed = 200f;
        SpeedChange = 200f;
    }

}
