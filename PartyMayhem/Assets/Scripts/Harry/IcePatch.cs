using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePatch : MonoBehaviour {

    public PlayerMovement MoveSpeed; //Most of this script was created by tweaking prexisting code in the game from the speedboost script.
    public GameObject[] Player;
    public bool SpeedDelayOn = false;

    public void Start()
    {
        MoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Player = GameObject.FindGameObjectsWithTag("Player");
        Player = new GameObject[4];
    }

    void Update () {
        Player = GameObject.FindGameObjectsWithTag("Player");
	}

    public void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            MoveSpeed.speed = 160f;
            SpeedDelayOn = true;
            Debug.Log("IceCollisionDetected");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {       
        MoveSpeed.speed = 200f; 
    }

}
