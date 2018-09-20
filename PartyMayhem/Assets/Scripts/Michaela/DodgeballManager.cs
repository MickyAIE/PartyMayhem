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

    public Text timerText;
    public Slider timerBar;

    public GameObject playerInfoPrefab;
    public GameObject[] playerInfoPositions;
    GameObject player1Info;
    GameObject player2Info;
    GameObject player3Info;
    GameObject player4Info;

    public Component[] playerProfiles;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerProfiles = gameManager.GetComponents(typeof(PlayerProfile));
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

        if(gameManager.ActivePlayerCount() == 1)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
        }
        else if (gameManager.ActivePlayerCount() == 2)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
            player2Info = Instantiate(playerInfoPrefab, playerInfoPositions[1].transform.position, Quaternion.identity, playerInfoPositions[1].transform);
            player2Info.transform.GetChild(3).GetComponent<Text>().text = "Player 2";
        }
        else if (gameManager.ActivePlayerCount() == 3)
        {
            player1Info = Instantiate(playerInfoPrefab, playerInfoPositions[0].transform.position, Quaternion.identity, playerInfoPositions[0].transform);
            player1Info.transform.GetChild(3).GetComponent<Text>().text = "Player 1";
            player2Info = Instantiate(playerInfoPrefab, playerInfoPositions[1].transform.position, Quaternion.identity, playerInfoPositions[1].transform);
            player2Info.transform.GetChild(3).GetComponent<Text>().text = "Player 2";
            player3Info = Instantiate(playerInfoPrefab, playerInfoPositions[2].transform.position, Quaternion.identity, playerInfoPositions[2].transform);
            player3Info.transform.GetChild(3).GetComponent<Text>().text = "Player 3";
        }
        else if (gameManager.ActivePlayerCount() == 4)
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

        gameManager.SpawnPlayers();

        timerBar.minValue = 0;
        timerBar.maxValue = gameTime;

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

            if (player.name == "Player1") gameManager.player1Score += 25;
            else if (player.name == "Player2") gameManager.player2Score += 25;
            else if (player.name == "Player3") gameManager.player3Score += 25;
            else if (player.name == "Player4") gameManager.player4Score += 25;
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

            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }

            winMessage.SetActive(true);
            loseMessage.SetActive(false);

            ReturnToMainMenu();
        }

        PlayerInfos();
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
        timerBar.value = gameTime;

    }

    public void ReturnToMainMenu()
    {
        waitToReturn -= Time.deltaTime;
        if (waitToReturn <= 0)
        {
            foreach (GameObject player in players)
            {
                if (player.activeInHierarchy == true)
                {
                    if (player.name == "Player1") gameManager.player1Score += 500;
                    else if (player.name == "Player2") gameManager.player2Score += 500;
                    else if (player.name == "Player3") gameManager.player3Score += 500;
                    else if (player.name == "Player4") gameManager.player4Score += 500;
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

    public void PlayerInfos()
    {
        if(PlayerPrefs.GetInt("Mode") == 2)
        {
            if (player1Info != null)
                player1Info.transform.GetChild(2).GetComponent<Text>().text = gameManager.player1Score.ToString();
            if (player2Info != null)
                player2Info.transform.GetChild(2).GetComponent<Text>().text = gameManager.player2Score.ToString();
            if (player3Info != null)
                player3Info.transform.GetChild(2).GetComponent<Text>().text = gameManager.player3Score.ToString();
            if (player4Info != null)
                player4Info.transform.GetChild(2).GetComponent<Text>().text = gameManager.player4Score.ToString();
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
            foreach(PlayerProfile pp in playerProfiles)
            {
                if(pp.playerNumber == 1)              
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


