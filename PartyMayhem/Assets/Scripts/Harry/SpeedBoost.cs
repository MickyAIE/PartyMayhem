using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

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
            SpeedCollision.GetComponent<PlayerMovement>().speed = BoostedSpeed;
            Invoke("SetDefaultSpeed1", 1.5f);
            Invoke("SetDefaultSpeed2", 1.5f);
            Invoke("SetDefaultSpeed3", 1.5f);
            Invoke("SetDefaultSpeed4", 1.5f);
            //if (SpeedCollision.gameObject == Player[0]) { Invoke("SetDefaultSpeed1", 2f); }//this code was taken from the winner.cs script after i found out this code had flaws.
            //if (SpeedCollision.gameObject == Player[1]) { Invoke("SetDefaultSpeed1", 2f); }
            //if (SpeedCollision.gameObject == Player[2]) { Invoke("SetDefaultSpeed1", 2f); }
            //if (SpeedCollision.gameObject == Player[3]) { Invoke("SetDefaultSpeed1", 2f); }// else { return; }
        }
    }

    public void OnTriggerExit2D(Collider2D SpeedCollisionExit)
    {

    }

    public void SetDefaultSpeed1()
    {            
        Player[0].GetComponent<PlayerMovement>().speed = DefaultSpeed;    
    }
    public void SetDefaultSpeed2()
    {
        Player[1].GetComponent<PlayerMovement>().speed = DefaultSpeed;
    }
    public void SetDefaultSpeed3()
    {
        Player[2].GetComponent<PlayerMovement>().speed = DefaultSpeed;
    }
    public void SetDefaultSpeed4()
    {            
        Player[3].GetComponent<PlayerMovement>().speed = DefaultSpeed;    
    }
}
