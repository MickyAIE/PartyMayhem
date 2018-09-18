using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    //Component of Minigame Manager

    private MissileMadness manager;

    public GameObject redMissile;
    public GameObject blueMissile;
    public GameObject missileIndicator;
    public int arrowDirection;

    public float spawnTimer;
    public float spawnDelay;

    private void Awake()
    {
        manager = GetComponent<MissileMadness>();
    }

    private void Update()
    {
        if (manager.State == MissileMadness.GameState.Game)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnDelay)
            {
                SpawnMissile();
                spawnTimer = 0;
            }
        }
    }

    private void SpawnMissile()
    {
        //Determine colour
        GameObject missile;

        bool redOrBlue;
        redOrBlue = (Random.value > 0.5f);

        if (redOrBlue)
            missile = redMissile;
        else
            missile = blueMissile;

        //Determine position
        Vector3 spawnPos;

        Vector3 spawnTop = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0f, 1f), 1.3f, 1f));
        Vector3 spawnRight = Camera.main.ViewportToWorldPoint(new Vector3(1.3f, Random.Range(-0f, 1f), 1f));
        Vector3 spawnBottom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0f, 1f), -0.3f, 1f));
        Vector3 spawnLeft = Camera.main.ViewportToWorldPoint(new Vector3(-0.3f, Random.Range(-0f, 1f), 1f));

        float pos;
        pos = Random.value;

        if (pos <= 0.25f && missile == redMissile) //blue missile is limited to only spawning from left or right
            spawnPos = spawnTop;
        else if (pos <= 0.5f)
            spawnPos = spawnRight;
        else if (pos <= 0.75f && missile == redMissile)
            spawnPos = spawnBottom;
        else
            spawnPos = spawnLeft;

        //Determine rotation
        Quaternion spawnRot;

        if (spawnPos == spawnTop)
            spawnRot = Quaternion.Euler(180, 0, 0);
        else if (spawnPos == spawnRight)
            spawnRot = Quaternion.Euler(0, 0, 90);
        else if (spawnPos == spawnLeft)
            spawnRot = Quaternion.Euler(0, 0, -90);
        else
            spawnRot = Quaternion.Euler(0, 0, 0);

        //Spawn
        Instantiate(missile, spawnPos, spawnRot);

        //Decrease delay between next missile
        switch (manager.Difficulty)
        {
            case MissileMadness.Difficulties.Default:
                if (spawnDelay >= 0.7f)
                    spawnDelay -= 0.1f;
                break;

            case MissileMadness.Difficulties.Easy:
                if (spawnDelay >= 0.9f)
                    spawnDelay -= 0.05f;
                break;

            case MissileMadness.Difficulties.Normal:
                if (spawnDelay >= 0.7f)
                    spawnDelay -= 0.1f;
                break;

            case MissileMadness.Difficulties.Hard:
                if (spawnDelay >= 0.5f)
                    spawnDelay -= 0.1f;
                break;
        }

        //Get indicator position offset
        Vector3 offset;

        if (spawnPos == spawnTop)
        {
            offset = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 17f));
            arrowDirection = 0;
        }
        else if (spawnPos == spawnRight)
        {
            offset = Camera.main.ViewportToWorldPoint(new Vector3(0.14f, 0.5f, 17f));
            arrowDirection = 1;
        }
        else if (spawnPos == spawnLeft)
        {
            offset = Camera.main.ViewportToWorldPoint(new Vector3(0.86f, 0.5f, 17f));
            arrowDirection = 2;
        }
        else
        {
            offset = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.9f, 17f));
            arrowDirection = 3;
        }

        //Show indicator
        Instantiate(missileIndicator, spawnPos + offset, Quaternion.identity);
    }
}