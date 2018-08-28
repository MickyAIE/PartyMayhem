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


    void Start()
    {
        gameSpeed = 500;
        goingUp = true;
        award = 0;
        increment = 250;
        max = 1000;
        min = 0;
        timer = 0;
        timerMax = 20;
	}

	void Update()
    {
        if(timer >= timerMax)
        {
            timer = 0;
            CalculateScore();
            Debug.Log(award.ToString());
        }
        else
        {
            timer += gameSpeed * Time.deltaTime;
        }
    }

    void CalculateScore()
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
}
