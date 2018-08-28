using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    private void Start()
    {
        Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, 3)].transform.position, Quaternion.identity);
        enemies.Add(enemyPrefab);

        shouldSpawnEnemy = true;

        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(go => go.name).ToArray();
    }

    private void Update()
    {
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

}
