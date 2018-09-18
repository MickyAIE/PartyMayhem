using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colouredtilepickup : MonoBehaviour {
    /*GameObject p1;
    GameObject p2;
    GameObject p3;
    GameObject p4;*/

    private void OnTriggerEnter2D(Collider2D colourchangercheck)
    {
        if (colourchangercheck.name == "player1")
            Destroy(gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D p2)
    {
        if (p2.name == "player2")
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D p3)
    {
        if (p3.name == "player3")
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D p4)
    {
        if (p4.name == "player4")
            Destroy(gameObject);
    }*/


}
