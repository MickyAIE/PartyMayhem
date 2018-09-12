using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePatch : MonoBehaviour {

    public PlayerMovement MoveSpeed;
    public GameObject[] Player;
    public bool SpeedDelayOn = false;

    // Use this for initialization
    void Start () {
        MoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Player = new GameObject[4];
    }
	
	// Update is called once per frame
	void Update () {
		 if(SpeedDelayOn == true)
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
