using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileMadness : MonoBehaviour
{
    public GameObject[] players;
    public Text timer;
    public Text message;

    public float timeLimit;
    public float countdown;
    public bool allowMovement;

    public enum GameState
    {
        Start,
        Game,
        Finish
    }
    private GameState gameState;
    public GameState State { get { return gameState; } }

    private void Awake()
    {
        timer.gameObject.SetActive(false);
        message.gameObject.SetActive(true);
        allowMovement = false;
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                message.text = countdown.ToString("f0");
                countdown -= Time.deltaTime;

                if (countdown <= 0)
                {
                    gameState = GameState.Game;
                    allowMovement = true;
                }
                
                break;

            case GameState.Game:
                timer.gameObject.SetActive(true);
                message.gameObject.SetActive(false);

                timeLimit -= Time.deltaTime;
                int seconds = Mathf.RoundToInt(timeLimit);
                timer.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

                OnePlayerLeft();

                if (timeLimit <= 0 || OnePlayerLeft())
                {
                    EndGame();
                    gameState = GameState.Finish;
                }

                break;

            case GameState.Finish:

                break;
        }
    }

    private void EndGame()
    {
        message.text = "FINISH!";
        timer.gameObject.SetActive(false);
        message.gameObject.SetActive(true);

        //Freezes players when game ends
        foreach (GameObject player in players)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }

            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.enabled = false;
            }
        }
    }

    private bool OnePlayerLeft()
    {
        int playerCount = 0;

        foreach (GameObject player in players)
        {
            if (player.activeSelf == true)
                playerCount++;
        }

        return (playerCount == 1);
    }
}