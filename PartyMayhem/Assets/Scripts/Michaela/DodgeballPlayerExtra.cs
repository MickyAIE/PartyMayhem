using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeballPlayerExtra : MonoBehaviour {

    public bool playerOneHasBeenHit = false;
    public bool playerTwoHasBeenHit = false;
    public bool playerThreeHasBeenHit = false;
    public bool playerFourHasBeenHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Player")
        {
            playerOneHasBeenHit = true;
        }
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Target1")
        {
            playerTwoHasBeenHit = true;
        }
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Target2")
        {
            playerThreeHasBeenHit = true;
        }
        if (collision.gameObject.tag == "Ball" && gameObject.name == "Target3")
        {
            playerFourHasBeenHit = true;
        }
    }
}
