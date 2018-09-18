using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissileMadness : MonoBehaviour
{
    private GameManager manager;

    public GameObject[] players;
    public Text timer;
    public Text message;

    public AudioSource music;
    public AudioSource itemSound;

    public float timeLimit;
    private float countdown;
    public bool onePlayerMode = false;

    public enum GameState
    {
        Start,
        Game,
        Finish
    }
    public GameState State { get; private set; }

    public enum Difficulties
    {
        Default,
        Easy,
        Normal,
        Hard
    }
    public Difficulties Difficulty { get; private set; }

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        timeLimit = manager.gameTimer;
        timer.gameObject.SetActive(false);
        message.gameObject.SetActive(true);
        countdown = 3.5f;

        if (manager.gameTimer != 0)
            timeLimit = manager.gameTimer;
        else
            timeLimit = 60;

        Difficulty = (Difficulties)manager.difficultyIndex;
    }

    private void Start()
    {
        manager.SpawnPlayers();
        players = GameObject.FindGameObjectsWithTag("Player");

        if (CurrentPlayerCount() == 1)
        {
            onePlayerMode = true;
        }

        DisablePlayerMovement();
    }

    private void Update()
    {
        CurrentPlayerCount();

        switch (State)
        {
            case GameState.Start:
                countdown -= Time.deltaTime;
                message.text = countdown.ToString("f0");

                if (countdown <= 1)
                {
                    timer.gameObject.SetActive(true);
                    message.gameObject.SetActive(false);

                    EnablePlayerMovement();
                    State = GameState.Game;
                }
                break;

            case GameState.Game:
                timeLimit -= Time.deltaTime;
                int seconds = Mathf.RoundToInt(timeLimit);
                timer.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

                if (timeLimit <= 0 || (CurrentPlayerCount() == 1 && !onePlayerMode) || CurrentPlayerCount() == 0)
                {
                    message.text = "FINISH!";
                    timer.gameObject.SetActive(false);
                    message.gameObject.SetActive(true);
                    countdown = 2f;

                    DisablePlayerMovement();
                    State = GameState.Finish;
                }
                break;

            case GameState.Finish:
                countdown -= Time.deltaTime;

                if (countdown <= 0)
                {
                    if (PlayerPrefs.GetInt("Mode") == 2)
                        manager.currentRound++;
                        
                    if (PlayerPrefs.GetInt("Mode") == 2 && manager.currentRound == manager.rounds)
                        manager.returningToMenus = false;
                    else
                        manager.returningToMenus = true;

                    SceneManager.LoadScene("Menus");
                }
                break;
        }
    }

    private int CurrentPlayerCount() //Number of players alive.
    {
        int playerCount = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf == true)
                playerCount++;
        }
        return playerCount;
    }

    private void DisablePlayerMovement() //Freezes players when game ends, implement something better later.
    {
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

    private void EnablePlayerMovement() //Reenables player movement after using DisablePlayerMovement.
    {
        foreach (GameObject player in players)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }

            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.enabled = true;
                pm.speed = 200f;
            }

            CharacterMoveTransitions cmt = player.GetComponent<CharacterMoveTransitions>();
            if (cmt != null)
            {
                cmt.isPaused = false;
                cmt.ResetAnimation();
            }
        }
    }
}