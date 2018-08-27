using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingGameManager : MonoBehaviour {

    public int Laps;

	void Start () {
        Laps = GameObject.Find("ThePlayer").GetComponent<LapsCounter>().Lap = Laps;
    }
	
	void Update () {

        if (Laps >= 3)
        {
            Time.timeScale = 0.25F;
            Invoke("ResetTimeScale", 2);
        }
    }
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
}
