using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class DodgeballManager : MonoBehaviour {

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

    private void Start()
    {
        Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, 3)].transform.position, Quaternion.identity);
        enemies.Add(enemyPrefab);

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
            if(player.GetComponent<DodgeballPlayerExtra>().playerOneHasBeenHit == true)
            {
                //players[0].SetActive(false);
            }
            if (player.GetComponent<DodgeballPlayerExtra>().playerTwoHasBeenHit == true)
            {
                players[1].SetActive(false);
            }
            if (player.GetComponent<DodgeballPlayerExtra>().playerThreeHasBeenHit == true)
            {
                players[2].SetActive(false);
            }
            if (player.GetComponent<DodgeballPlayerExtra>().playerFourHasBeenHit == true)
            {
                players[3].SetActive(false);
            }

            if (player.GetComponent<DodgeballPlayerExtra>().playerOneHasBeenHit == true && (player.GetComponent<DodgeballPlayerExtra>().playerTwoHasBeenHit == true
                && (player.GetComponent<DodgeballPlayerExtra>().playerThreeHasBeenHit == true && player.GetComponent<DodgeballPlayerExtra>().playerFourHasBeenHit == true))) {

                allPlayersHit = true;
            }

        }
            GameTimer();

            enemyCount = enemies.Count;
            if (enemyCount >= maxEnemies)
            {
                shouldSpawnEnemy = false;
            }
            else
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
        Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, 3)].transform.position, Quaternion.identity);
        enemies.Add(enemyPrefab);
    }

    public void GameTimer()
    {
        if (gameTime <= 0)
        {
            return;
        }

        gameTime -= Time.deltaTime;
        int seconds = Mathf.RoundToInt(gameTime);
        timerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));
    }
}
