using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    [HideInInspector] public float scoreAward;


	void Start()
    {
		
	}

	void Update()
    {
        CalculateScore();
	}

    void CalculateScore()
    {
        scoreAward = Mathf.Sin(Time.time * 20);

        Debug.Log(scoreAward.ToString());
    }
}
