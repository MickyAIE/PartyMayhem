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
    public bool allowMovement = false;
    public bool onePlayerMode = false;

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

        if (PlayerCount() == 1)
        {
            onePlayerMode = true;
        }
    }

    private void Update()
    {
        PlayerCount();

        switch (gameState)
        {
            case GameState.Start:
                message.text = countdown.ToString("f0");
                countdown -= Time.deltaTime;

                if (countdown <= 0)
                {
                    allowMovement = true;
                    timer.gameObject.SetActive(true);
                    message.gameObject.SetActive(false);
                    gameState = GameState.Game;
                }
                break;

            case GameState.Game:
                timeLimit -= Time.deltaTime;
                int seconds = Mathf.RoundToInt(timeLimit);
                timer.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

                if (timeLimit <= 0 || (PlayerCount() == 1 && !onePlayerMode) || PlayerCount() == 0)
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
        allowMovement = false;

        //Freezes players when game ends, implement something better later.
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

            CharacterMoveTransitions cmt = player.GetComponent<CharacterMoveTransitions>();
            if (cmt != null)
            {
                cmt.isPaused = true;
                cmt.ResetAnimation();
            }
        }
    }

    private int PlayerCount()
    {
        int playerCount = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf == true)
                playerCount++;
        }
        return playerCount;
    }
}