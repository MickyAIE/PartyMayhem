using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePatch : MonoBehaviour {

    public PlayerMovement MoveSpeed; //Most of this script was created by tweaking prexisting code in the game from the speedboost script.
    public GameObject[] Player;
    public bool SpeedDelayOn = false;

    private void Awake()
    {
        MoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Start () {
        MoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Player = new GameObject[4];
    }
   
    void Update () {
        MoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (SpeedDelayOn == true)
            {
             SpeedDelay();
             SpeedDelayOn = false;
            }
	}

    public void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            Player = GameObject.FindGameObjectsWithTag("Player");
            SpeedDelayOn = true;
            Debug.Log("IceCollisionDetected");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("IceCollisionExit");
        SpeedDelayOn = false;
        MoveSpeed.speed = 200f; 
    }

    public void SpeedDelay()
    {
        MoveSpeed.speed = 160f;
    }
}
