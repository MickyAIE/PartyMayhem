using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

    public GameObject Player;
    [HideInInspector]
    public float BoostedSpeed;
    

    void Start () {
        BoostedSpeed = Player.GetComponent<PlayerMovement>().speed;
    }

    public void OnTriggerEnter2D(Collider2D SpeedCollision)
    {
        BoostedSpeed = BoostedSpeed + 500f;
        Debug.Log("Collision Detected NOT with Player");

        if (SpeedCollision.gameObject.tag == "Player")
        {
            BoostedSpeed = BoostedSpeed + 500f;
            Debug.Log("Collision Detected With Player");
        }
    }
    void Update () {
		
	}
}
