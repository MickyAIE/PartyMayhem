using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmScoreTracker : MonoBehaviour
{
    [HideInInspector] public int points;

    [HideInInspector] public Dictionary<string, string> drums = new Dictionary<string, string>();
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

        drums.Add(drumRed.name, "Red");
        drums.Add(drumGreen.name, "Green");
        drums.Add(drumBlue.name, "Blue");
        drums.Add(drumYellow.name, "Yellow");
    }

    private void Update()
    {
        award = rManager.award;
        if (rManager.chosenColours.Count > 0)
        {
            currentColour = rManager.chosenColours[0];
        }
        else
        {
            currentColour = "No Colour";
        }
    }
}
