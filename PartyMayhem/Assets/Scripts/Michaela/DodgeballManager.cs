using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeballManager : MonoBehaviour {

    public GameObject[] enemySpawnPoints;
    public GameObject enemyPrefab;
    public List<GameObject> enemies;

    public float timer = 5;
    public float startTime = 5;

    public bool shouldSpawnEnemy = true;

    public int enemyCount;

    private void Start()
    {
        Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, 3)].transform.position, Quaternion.identity);
        enemies.Add(enemyPrefab);

        shouldSpawnEnemy = true;
    }

    private void Update()
    {
        enemyCount = enemies.Count;
        if(enemyCount >= 5)
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
