using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissileMadness : MonoBehaviour
{
    private GameManager manager;

    public GameObject[] players;
    public Text message;
    public Text timer;
    public Slider timerBar;

    public GameObject playerInfoPrefab;
    public GameObject[] playerInfoPositions;
    private GameObject player1Info;
    private GameObject player2Info;
    private GameObject player3Info;
    private GameObject player4Info;
    public Component[] playerProfiles;

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
        timerBar.gameObject.SetActive(false);
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
        playerProfiles = manager.GetComponents<PlayerProfile>();

        if (CurrentPlayerCount() == 1)
        {
            onePlayerMode = true;
        }

        DisablePlayerMovement();

        timerBar.maxValue = timeLimit;
        HUDSpawn();

        foreach (GameObject player in players)
        {
            if (player.activeInHierarchy == true)
            {
                if (player.GetComponent<CharacterMoveTransitions>().playerNumber == 1)
                    manager.player1Score += 25;
                if (player.GetComponent<CharacterMoveTransitions>().playerNumber == 2)
                    manager.player2Score += 25;
                if (player.GetComponent<CharacterMoveTransitions>().playerNumber == 3)
                    manager.player3Score += 25;
                if (player.GetComponent<CharacterMoveTransitions>().playerNumber == 4)
                    manager.player4Score += 25;
            }
        }
    }

    private void Update()
    {
        CurrentPlayerCount();
        PlayerInfos();

        switch (State)
        {
            case GameState.Start:
                countdown -= Time.deltaTime;
                message.text = countdown.ToString("f0");

                if (countdown <= 1)
                {
                    timer.gameObject.SetActive(true);
                    timerBar.gameObject.SetActive(true);
                    message.gameObject.SetActive(false);

                    EnablePlayerMovement();
                    State = GameState.Game;
                }
                break;

            case GameState.Game:
                timeLimit -= Time.deltaTime;
                int seconds = Mathf.RoundToInt(timeLimit);
                timer.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));
                timerBar.value = timeLimit;

                if (timeLimit <= 0 || (CurrentPlayerCount() == 1 && !onePlayerMode) || CurrentPlayerCount() == 0)
                {
                    message.text = "FINISH!";
                    timer.gameObject.SetActive(false);
                    timerBar.gameObject.SetActive(false);
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

                    if (PlayerPrefs.GetInt("Mode") == 2 && manager.currentRound >= manager.rounds)
                        manager.returningToMenus = false;
                    else
                        manager.returningToMenus = true;

                    foreach (GameObject player in players)
                    {
                        if (player.activeInHierarchy == true)
                        {
                            if (player.GetComponent<CharacterMoveTransitions>().playerNumber == 1)
                                manager.player1Score += 500;
                            if (player.GetComponent<CharacterMoveTransitions>().playerNumber == 2)
                                manager.player2Score += 500;
                            if (player.GetComponent<CharacterMoveTransitions>().playerNumber == 3)
                                manager.player3Score += 500;
                            if (player.GetComponent<CharacterMoveTransitions>().playerNumber == 4)
                                manager.player4Score += 500;
                        }
                    }

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

    private void HUDSpawn()
    {
        if (manager.ActivePlayerCount() == 1)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
        }
        else if (manager.ActivePlayerCount() == 2)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
            player2Info = Instantiate(playerInfoPrefab, playerInfoPositions[1].transform.position, Quaternion.identity, playerInfoPositions[1].transform);
            player2Info.transform.GetChild(3).GetComponent<Text>().text = "Player 2";
        }
        else if (manager.ActivePlayerCount() == 3)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
            player2Info = Instantiate(playerInfoPrefab, playerInfoPositions[1].transform.position, Quaternion.identity, playerInfoPositions[1].transform);
            player2Info.transform.GetChild(3).GetComponent<Text>().text = "Player 2";
            player3Info = Instantiate(playerInfoPrefab, playerInfoPositions[2].transform.position, Quaternion.identity, playerInfoPositions[2].transform);
            player3Info.transform.GetChild(3).GetComponent<Text>().text = "Player 3";
        }
        else if (manager.ActivePlayerCount() == 4)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
            player2Info = Instantiate(playerInfoPrefab, playerInfoPositions[1].transform.position, Quaternion.identity, playerInfoPositions[1].transform);
            player2Info.transform.GetChild(3).GetComponent<Text>().text = "Player 2";
            player3Info = Instantiate(playerInfoPrefab, playerInfoPositions[2].transform.position, Quaternion.identity, playerInfoPositions[2].transform);
            player3Info.transform.GetChild(3).GetComponent<Text>().text = "Player 3";
            player4Info = Instantiate(playerInfoPrefab, playerInfoPositions[3].transform.position, Quaternion.identity, playerInfoPositions[3].transform);
            player4Info.transform.GetChild(3).GetComponent<Text>().text = "Player 4";
        }
    }

    public void PlayerInfos()
    {
        if (PlayerPrefs.GetInt("Mode") == 2)
        {
            if (player1Info != null)
                player1Info.transform.GetChild(2).GetComponent<Text>().text = manager.player1Score.ToString();
            if (player2Info != null)
                player2Info.transform.GetChild(2).GetComponent<Text>().text = manager.player2Score.ToString();
            if (player3Info != null)
                player3Info.transform.GetChild(2).GetComponent<Text>().text = manager.player3Score.ToString();
            if (player4Info != null)
                player4Info.transform.GetChild(2).GetComponent<Text>().text = manager.player4Score.ToString();
        }
        else
        {
            if (player1Info != null)
                player1Info.transform.GetChild(2).gameObject.SetActive(false);
            if (player2Info != null)
                player2Info.transform.GetChild(2).gameObject.SetActive(false);
            if (player3Info != null)
                player3Info.transform.GetChild(2).gameObject.SetActive(false);
            if (player4Info != null)
                player4Info.transform.GetChild(2).gameObject.SetActive(false);
        }

        if (player1Info != null)
        {
            foreach (PlayerProfile pp in playerProfiles)
            {
                if (pp.playerNumber == 1)
                    player1Info.transform.GetChild(1).GetComponent<Image>().sprite = pp.playerPortrait;
            }
        }
        if (player2Info != null)
        {
            foreach (PlayerProfile pp in playerProfiles)
            {
                if (pp.playerNumber == 2)
                    player2Info.transform.GetChild(1).GetComponent<Image>().sprite = pp.playerPortrait;
            }
        }
        if (player3Info != null)
        {
            foreach (PlayerProfile pp in playerProfiles)
            {
                if (pp.playerNumber == 3)
                    player3Info.transform.GetChild(1).GetComponent<Image>().sprite = pp.playerPortrait;
            }
        }
        if (player4Info != null)
        {
            foreach (PlayerProfile pp in playerProfiles)
            {
                if (pp.playerNumber == 4)
                    player4Info.transform.GetChild(1).GetComponent<Image>().sprite = pp.playerPortrait;
            }
        }
    }
}