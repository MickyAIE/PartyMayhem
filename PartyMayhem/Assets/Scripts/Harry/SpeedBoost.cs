using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

    [HideInInspector]
    public float BoostedSpeed; //This is the speed the player will be boosted to.
    public float DefaultSpeed; //This is the default running speed the player is set at.
    public GameObject Player;

    public void Start()
    {
        BoostedSpeed = 200 * 3;
        DefaultSpeed = 150;
    }

    public void OnTriggerEnter2D(Collider2D SpeedCollision)
    {
        Player = SpeedCollision.gameObject;
        if (SpeedCollision.gameObject.tag == "Player") //When a Player collides with the speed pad it triggers a boost in the players speed and sends a Debug Log Message.
        {
            SpeedCollision.GetComponent<PlayerMovement>().speed = BoostedSpeed;
            //Debug.Log("Collision Detected With Player");          
        }
    }

    public void OnTriggerExit2D(Collider2D SpeedCollisionExit)
    {
        Invoke("SetDefaultSpeed", 1);
    }

    public void SetDefaultSpeed()
    {
        Player.GetComponent<PlayerMovement>().speed = DefaultSpeed;
    }
}
