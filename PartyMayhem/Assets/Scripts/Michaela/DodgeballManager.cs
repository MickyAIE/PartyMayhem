﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DodgeballManager : MonoBehaviour {

    private GameManager manager;

    public GameObject[] enemySpawnPoints;
    public GameObject enemyPrefab;
    public List<GameObject> enemies;
    public GameObject[] players;

    public float timer = 5;
    public float startTime = 5;

    public bool shouldSpawnEnemy = true;

    public int enemyCount;
    public int maxEnemies;

    public float gameTime;
    public Text timerText;

    public bool allPlayersHit = false;
    public bool endGame = false;

    public GameObject winMessage;
    public GameObject loseMessage;
    public GameObject optionButtons;

    public bool playerOneHasBeenHit = false;
    public bool playerTwoHasBeenHit = false;
    public bool playerThreeHasBeenHit = false;
    public bool playerFourHasBeenHit = false;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        manager.SpawnPlayers();

        Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, 3)].transform.position, Quaternion.identity);
        enemies.Add(enemyPrefab);

        winMessage.SetActive(false);
        loseMessage.SetActive(false);
        optionButtons.SetActive(false);

        shouldSpawnEnemy = true;

        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(go => go.name).ToArray();   

        foreach(GameObject player in players)
        {
            player.AddComponent<DodgeballPlayerExtra>();
        }
    }

    private void Update()
    {
        foreach (GameObject player in players)
        {
            if(playerOneHasBeenHit == true)
            {
                players[0].SetActive(false);
            }
            if (playerTwoHasBeenHit == true)
            {
                players[1].SetActive(false);
            }
            if (playerThreeHasBeenHit == true)
            {
                players[2].SetActive(false);
            }
            if (playerFourHasBeenHit == true)
            {
                players[3].SetActive(false);
            }

            if ((playerOneHasBeenHit == true && playerTwoHasBeenHit == true)
                && (playerThreeHasBeenHit == true && playerFourHasBeenHit == true)) {

                allPlayersHit = true;
            }

        }

        if (allPlayersHit == true)
        {
            endGame = true;
            shouldSpawnEnemy = false;

            foreach (GameObject player in players)
            {
                player.SetActive(false);
            }

            foreach (GameObject enemy in enemies)
            {
            }

            winMessage.SetActive(false);
            loseMessage.SetActive(true);
            optionButtons.SetActive(true);

        }

        if (gameTime <= 0 && allPlayersHit == false)
        {
            endGame = true;
            shouldSpawnEnemy = false;

            foreach (GameObject player in players)
            {
                player.SetActive(false);
            }

            winMessage.SetActive(true);
            loseMessage.SetActive(false);
            optionButtons.SetActive(true);
        }

        GameTimer();

        enemyCount = enemies.Count;
        if (enemyCount >= maxEnemies && endGame == false)
        {
            shouldSpawnEnemy = false;
        }
        else if(enemyCount > maxEnemies && endGame == false)
        {
            shouldSpawnEnemy = true;
        }

        timer -= Time.deltaTime;
        if (timer <= 0 && shouldSpawnEnemy == true)
        {
            SpawnEnemy();
            timer = startTime;
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

        gameTime -= Time.deltaTime;
        int seconds = Mathf.RoundToInt(gameTime);
        timerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));
    }

    public void OnPlayAgain()
    {
        SceneManager.LoadScene("DodgeballDojo");
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("Menus");
    }
}
