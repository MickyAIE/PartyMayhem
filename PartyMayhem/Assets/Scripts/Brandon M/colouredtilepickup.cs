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



    GameObject Tile()
    {
        GameObject[] tile;

        tile = GameObject.FindGameObjectsWithTag("tile");
        GameObject tile => 1;
        
        foreach (GameObject tile)
        {
            if (tile < 1)
            {
               Debug.Log "Player" + /*what player +*/ "has won the game";
            }
        }
        return tile;
    }
}