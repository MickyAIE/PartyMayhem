using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    private MissileMadness manager;

    public GameObject redMissile;
    public GameObject blueMissile;

    public float spawnTimer;
    public float spawnDelay;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<MissileMadness>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay && manager.gameInProgress == true)
        {
            SpawnMissile();
            spawnTimer = 0;
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

        Vector3 spawnTop = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0f, 1f), 1.1f, 1f));
        Vector3 spawnRight = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(-0f, 1f), 1f));
        Vector3 spawnBottom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0f, 1f), -0.1f, 1f));
        Vector3 spawnLeft = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, Random.Range(-0f, 1f), 1f));

        float pos;
        pos = Random.value;

        if (pos <= 0.25f)
            spawnPos = spawnTop;
        else if (pos <= 0.5f)
            spawnPos = spawnRight;
        else if (pos <= 0.75f)
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
        if (spawnDelay >= 0.7f)
            spawnDelay -= 0.1f;
    }
}