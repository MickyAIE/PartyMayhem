using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DodgeballManager : MonoBehaviour {

    private GameManager gameManager;

    public GameObject[] enemySpawnPoints;
    public GameObject enemyPrefab;
    public List<GameObject> enemies;
    public GameObject[] players;

    public float enemySpawnTimer = 5;
    public float enemySpawnStartTime = 5;

    public float waitToReturn = 2;
    public float waitToStart = 3;


    public bool shouldSpawnEnemy = true;
    public bool beginningCountdown = true;

    public int enemyCount;
    public int maxEnemies;

    public float gameTime;
    public Text timerText;

    public bool allPlayersHit = false;
    public bool endGame = false;

    public GameObject winMessage;
    public GameObject loseMessage;
    public Text countdownText;
    public Text hardModeMessage;

    public bool playerOneHasBeenHit = false;
    public bool playerTwoHasBeenHit = false;
    public bool playerThreeHasBeenHit = false;
    public bool playerFourHasBeenHit = false;

    public AudioClip deathClip;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        if (gameManager.gameTimer != 0) gameTime = gameManager.gameTimer;
        if (gameManager.difficultyIndex == 1)
        {
            maxEnemies = 5;
            enemySpawnTimer = 10;
            enemySpawnStartTime = 10;
        }
        if (gameManager.difficultyIndex == 2)
        {
            maxEnemies = 6;
            enemySpawnTimer = 6;
            enemySpawnStartTime = 6;
        }
        if (gameManager.difficultyIndex == 3)
        {
            maxEnemies = 10;
            enemySpawnTimer = 5;
            enemySpawnStartTime = 5;
        }

        gameManager.SpawnPlayers();

        //Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, 3)].transform.position, Quaternion.identity);
        enemies.Add(enemyPrefab);

        winMessage.SetActive(false);
        loseMessage.SetActive(false);

        shouldSpawnEnemy = false;

        waitToReturn = 2;
        waitToStart = 3;

        countdownText.gameObject.SetActive(true);
        hardModeMessage.gameObject.SetActive(false);

        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(go => go.name).ToArray();

        foreach (GameObject player in players)
        {
            player.AddComponent<DodgeballPlayerExtra>();
            player.GetComponent<PlayerMovement>().enabled = false;

            if (player == players[0]) player.gameObject.name = "Player1";
            else if (player == players[1]) player.gameObject.name = "Player2";
            else if (player == players[2]) player.gameObject.name = "Player3";
            else if (player == players[3]) player.gameObject.name = "Player4";

            //player.AddComponent<AudioSource>();
            //player.GetComponent<AudioSource>().playOnAwake = false;
            //player.GetComponent<AudioSource>().clip = deathClip;
        }
    }

    private void Update()
    {
        waitToStart -= Time.deltaTime;
        if (waitToStart <= 3 && beginningCountdown == true)
        {
            countdownText.text = "3";
        }
        if (waitToStart <= 2 && beginningCountdown == true)
        {
            countdownText.text = "2";

            if (gameManager.difficultyIndex == 3) hardModeMessage.gameObject.SetActive(true);
        }
        if (waitToStart <= 1 && beginningCountdown == true)
        {
            countdownText.text = "1";
        }
        if (waitToStart <= 0 && beginningCountdown == true)
        {
            beginningCountdown = false;

            shouldSpawnEnemy = true;
            countdownText.gameObject.SetActive(false);
            hardModeMessage.gameObject.SetActive(false);

            Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, 3)].transform.position, Quaternion.identity);
            enemySpawnTimer = enemySpawnStartTime;

            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerMovement>().enabled = true;
            }
        }

        foreach (GameObject player in players)
        {
            if (playerOneHasBeenHit == true) players[0].SetActive(false);
            if (playerTwoHasBeenHit == true) players[1].SetActive(false);
            if (playerThreeHasBeenHit == true) players[2].SetActive(false);
            if (playerFourHasBeenHit == true) players[3].SetActive(false);

            if (players.Length == 1)
            {
                if (playerOneHasBeenHit == true) allPlayersHit = true;
            }
            else if (players.Length == 2)
            {
                if (playerOneHasBeenHit == true && playerTwoHasBeenHit == true)
                    allPlayersHit = true;
            }
            else if (players.Length == 3)
            {
                if ((playerOneHasBeenHit == true && playerTwoHasBeenHit == true) && playerThreeHasBeenHit == true)
                    allPlayersHit = true;
            }
            else if (players.Length == 4)
            {
                if ((playerOneHasBeenHit == true && playerTwoHasBeenHit == true) && (playerThreeHasBeenHit == true && playerFourHasBeenHit == true))
                    allPlayersHit = true;
            }
        }

        if (allPlayersHit == true)
        {
            endGame = true;
            shouldSpawnEnemy = false;

            foreach (GameObject player in players) player.GetComponent<PlayerMovement>().enabled = false;

            winMessage.SetActive(false);
            loseMessage.SetActive(true);

            ReturnToMainMenu();
        }

        if (gameTime <= 0 && allPlayersHit == false)
        {
            endGame = true;
            shouldSpawnEnemy = false;

            foreach (GameObject player in players) player.GetComponent<PlayerMovement>().enabled = false;

            winMessage.SetActive(true);
            loseMessage.SetActive(false);

            ReturnToMainMenu();
        }

        GameTimer();

        enemyCount = enemies.Count;
        if (enemyCount >= maxEnemies && endGame == false) shouldSpawnEnemy = false;
        else if (enemyCount > maxEnemies && endGame == false) shouldSpawnEnemy = true;

        enemySpawnTimer -= Time.deltaTime;
        if (enemySpawnTimer <= 0 && shouldSpawnEnemy == true)
        {
            SpawnEnemy();
            enemySpawnTimer = enemySpawnStartTime;
        }
    }


    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, 5)].transform.position, Quaternion.identity);
        enemies.Add(enemyPrefab);
    }

    public void GameTimer()
    {
        if (gameTime <= 0 || endGame == true)
        {
            return;
        }

        if(beginningCountdown == false) gameTime -= Time.deltaTime;
        int seconds = Mathf.RoundToInt(gameTime);
        timerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));
    }

    public void ReturnToMainMenu()
    {
        waitToReturn -= Time.deltaTime;
        if(waitToReturn <= 0)
        {
            foreach(GameObject player in players)
            {
                if(player.activeInHierarchy == true)
                {
                    if (player.name == "Player1") gameManager.player1Score += 100;
                    else if (player.name == "Player2") gameManager.player2Score += 100;
                    else if (player.name == "Player3") gameManager.player3Score += 100;
                    else if (player.name == "Player4") gameManager.player4Score += 100;
                }
            }

            if (PlayerPrefs.GetInt("Mode") == 2) gameManager.currentRound += 1;

            if (PlayerPrefs.GetInt("Mode") == 2 && gameManager.currentRound == gameManager.rounds)
                gameManager.returningToMenus = false;
            else
                gameManager.returningToMenus = true;

            SceneManager.LoadScene("Menus");
        }
    }
}
