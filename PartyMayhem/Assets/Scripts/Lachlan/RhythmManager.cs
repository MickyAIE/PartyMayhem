using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public AudioSource beat;

    [HideInInspector] public int gameSpeed;

    [HideInInspector] public bool goingUp;

    [HideInInspector] public int award;
    [HideInInspector] public int increment;

    [HideInInspector] public int max;
    [HideInInspector] public int min;

    [HideInInspector] public float timer;
    [HideInInspector] public float timerMax;

    [HideInInspector] public List<string> colours = new List<string>();
    [HideInInspector] public string chosenColour;
    [HideInInspector] public List<string> chosenColours = new List<string>();
    [HideInInspector] public bool usingColours;

    [HideInInspector] public int points;
    [HideInInspector] public int rounds;

    public void Start()
    {
        points = 4;

        colours.Add("Red");
        colours.Add("Green");
        colours.Add("Blue");
        colours.Add("Yellow");

        gameSpeed = 500;
        goingUp = true;
        award = 0;
        increment = 250;
        max = 1000;
        min = 0;
        timer = 0;
        timerMax = 20;
    }

	public void Update()
    {
        if(timer >= timerMax)
        {
            timer = 0;
            CalculateScore();
        }
        else
        {
            timer += gameSpeed * Time.deltaTime;
        }

        if(chosenColours.Count < points && usingColours == false)
        {
            GetColours();
        }

        if(chosenColours.Count == 0)
        {
            usingColours = false;
        }
    }

    public void CalculateScore()
    {
        if(goingUp == true)
        {
            award += increment;
        }
        else
        {
            award -= increment;
        }

        if(award == min)
        {
            goingUp = true;
        }
        
        if(award == max)
        {
            beat.Play();
            goingUp = false;
        }
    }

    public void StateScore()
    {
        Debug.Log(award.ToString());
    }

    public void GetColours()
    {
        chosenColour = colours[Random.Range(0, colours.Count)];
        chosenColours.Add(chosenColour);

        if (chosenColours.Count == points)
        {
            usingColours = true;

            Debug.Log(chosenColours[0] + chosenColours[1] + chosenColours[2] + chosenColours[3]);

            //chosenColours.RemoveAt(0); //TESTING REMOVAL
            //Debug.Log(chosenColours[0] + chosenColours[1] + chosenColours[2]);
        }
    }
}
