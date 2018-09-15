using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private readonly float lifetime = 1f;
    private float timer = 0f;

    private MissileSpawner spawner;
    public GameObject[] sprites;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<MissileSpawner>();

        sprites[spawner.arrowDirection].SetActive(true);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}