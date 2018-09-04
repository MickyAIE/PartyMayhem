using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeballPlayerExtra : MonoBehaviour {

    public DodgeballManager dM;

    private void Start()
    {
        dM = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<DodgeballManager>();
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Player")
        {
            dM.playerOneHasBeenHit = true;
        }
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Target1")
        {
            dM.playerTwoHasBeenHit = true;
        }
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Target2")
        {
            dM.playerThreeHasBeenHit = true;
        }
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Target3")
        {
            dM.playerFourHasBeenHit = true;
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Player1")
        {
            dM.playerOneHasBeenHit = true;
        }
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Player2")
        {
            dM.playerTwoHasBeenHit = true;
        }
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Player3")
        {
            dM.playerThreeHasBeenHit = true;
        }
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Player4")
        {
            dM.playerFourHasBeenHit = true;
        }
    }
}
