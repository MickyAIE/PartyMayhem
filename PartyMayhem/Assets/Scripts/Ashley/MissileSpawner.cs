using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject redMissile;
    public GameObject blueMissile;
    public Transform[] spawnPositions;

    public float spawnTimer;
    public float spawnDelay;
    public float maxMissileLifetime;
    public int maxMissilesOnScreen;
    private bool redOrBlue;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay)
        {
            //SpawnMissile();
            spawnTimer = 0;
        }
    }

    private void SpawnMissile()
    {
        redOrBlue = (Random.value > 0.5f);

        if (redOrBlue)
            Instantiate(redMissile); //TO DO: determine spawn location, have missile rotation point toward middle of stage
        else
            Instantiate(blueMissile);
    }
}