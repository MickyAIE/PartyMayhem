using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    //Component of MissileMadness Manager

    private MissileMadness manager;

    public GameObject shield;
    public GameObject speedBoost;
    public Sprite shieldSprite;

    public Transform[] spawnSpots;
    public bool canSpawn = true;

    private float spawnTimer = 1;
    private float spawnDelay;

    private void Awake()
    {
        manager = GetComponent<MissileMadness>();

        switch (manager.Difficulty)
        {
            case MissileMadness.Difficulties.Default:
                spawnDelay = 5;
                break;
            case MissileMadness.Difficulties.Easy:
                spawnDelay = 3;
                break;
            case MissileMadness.Difficulties.Normal:
                spawnDelay = 5;
                break;
            case MissileMadness.Difficulties.Hard:
                spawnDelay = 10;
                break;
        }
    }

    private void Update()
    {
        if (manager.State == MissileMadness.GameState.Game && canSpawn == true)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnDelay)
            {
                Instantiate(ItemToSpawn(), SpawnPosition());
                canSpawn = false; //set back to true in PickupItem.cs
                spawnTimer = 0;
            }
        }
    }

    private GameObject ItemToSpawn()
    {
        GameObject item;

        float randomItem;
        randomItem = Random.value;

        if (randomItem <= 0.5f)
            item = shield;
        else
            item = speedBoost;

        return item;
    }

    private Transform SpawnPosition()
    {
        Transform spawnPos;
        spawnPos = spawnSpots[Random.Range(0, spawnSpots.Length)];

        return spawnPos;
    }
}