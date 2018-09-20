using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour {

    public int Winner = 0;



    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // gameobjects array 1 = gameobjects find with tag "p1"
        // gameobjects array 2 = gameobjects find with tag "p2"
        // gameobjects array 3 = gameobjects find with tag "p3"
        // gameobjects array 4 = gameobjects find with tag "p4"\

        GameObject[] items1 = GameObject.FindGameObjectsWithTag("p1");
        GameObject[] items2 = GameObject.FindGameObjectsWithTag("p2");
        GameObject[] items3 = GameObject.FindGameObjectsWithTag("p3");
        GameObject[] items4 = GameObject.FindGameObjectsWithTag("p4");

        // if array 1 count == 0
        // Winner = 1

        if (items1.Length == 0)
        {
            Winner = 1;
        }

         if (items2.Length == 0)
          {
              Winner = 2;
          }

          if (items3.Length == 0)
          {
              Winner = 3;
          }

          if (items4.Length == 0)
          {
              Winner = 4;
          }

          // if array 2 count == 0
          // Winner = 2

          // if array 3 count == 0
          // Winner = 4

          // if array 4 count == 0
          // Winner = 4

          if ( Winner !=0)
          {
              Winner = Winner;
          }

          // if winner != 0
          // do code Winner is Winner
      
    }
}
