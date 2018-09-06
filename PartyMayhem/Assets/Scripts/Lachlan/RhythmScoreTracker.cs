using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmScoreTracker : MonoBehaviour
{
    [HideInInspector] public int points;

    public GameObject drumRed;
    public GameObject drumGreen;
    public GameObject drumBlue;
    public GameObject drumYellow;

    [HideInInspector] public RhythmManager rManager;
    [HideInInspector] public int award;
    [HideInInspector] public string currentColour;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Rhythm Manager") != null)
        {
            rManager = GameObject.FindGameObjectWithTag("Rhythm Manager").GetComponent<RhythmManager>();
        }
    }

    private void Update()
    {
        award = rManager.award;
        if (rManager.chosenColours.Count > 0)
        //I had this:
        //if(rManager.chosenColours[0] != null)
        {
            currentColour = rManager.chosenColours[0];
        }
        else
        {
            currentColour = "No Colour";
        }
    }
}
