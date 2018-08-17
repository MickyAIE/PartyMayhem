using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileMadness : MonoBehaviour
{
    public Player[] players;
    public Text timer;
    public Text message;

    public float timeLimit;
    public bool gameInProgress;

    private void Awake()
    {
        timer.gameObject.SetActive(true);
        message.gameObject.SetActive(false);
    }

    private void Start()
    {
        gameInProgress = true;
    }

    private void Update()
    {
        if (gameInProgress)
            Game();
    }

    private void Game()
    {
        timeLimit -= Time.deltaTime;
        int seconds = Mathf.RoundToInt(timeLimit);
        timer.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

        if (timeLimit <= 0 || OnePlayerLeft())
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameInProgress = false;
        message.text = "FINISH!";
        timer.gameObject.SetActive(false);
        message.gameObject.SetActive(true);

        //something to carry over scores to a results screen or tournament mode or whatever
    }

    private bool OnePlayerLeft()
    {
        return false;
    }
}