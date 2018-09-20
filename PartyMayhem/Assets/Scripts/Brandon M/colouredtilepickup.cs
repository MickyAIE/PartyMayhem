using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colouredtilepickup : MonoBehaviour
{
    public string whatPLAYER;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "player1" && whatPLAYER == "1")
        { Destroy(gameObject); }


        if (other.name == "player2" && whatPLAYER == "1")
        { Destroy(gameObject); }

        if (other.name == "player3" && whatPLAYER == "1")
        { Destroy(gameObject); }

        if (other.name == "player4" && whatPLAYER == "1")
        { Destroy(gameObject); }
    }


}