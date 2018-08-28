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

    private void Start()
    {
        Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, 3)].transform.position, Quaternion.identity);
        enemies.Add(enemyPrefab);

        shouldSpawnEnemy = true;

        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(go => go.name).ToArray();
    }

    private void Update()
    {
        GameTimer();

        foreach (GameObject player in players)
        {
            //player.AddComponent<>();
        }

        enemyCount = enemies.Count;
        if(enemyCount >= maxEnemies)
        {
            shouldSpawnEnemy = false;
        }
        else
        {
            shouldSpawnEnemy = true;
        }

        timer -= Time.deltaTime;
        if(timer <= 0 && shouldSpawnEnemy == true)
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
