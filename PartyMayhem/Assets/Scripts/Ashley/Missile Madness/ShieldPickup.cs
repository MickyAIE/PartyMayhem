using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    //This script is attached to the player when they pick up the shield item.
    //Changes the player's layer to one that won't collide with missiles and adds a shield sprite.

    private ItemSpawner spawner;
    private SpriteRenderer sRenderer;

    private readonly float powerupLifetime = 5f;
    private float powerupTimer = 0f;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<ItemSpawner>();
        sRenderer = gameObject.AddComponent<SpriteRenderer>();

        gameObject.layer = 10;
    }

    private void Start()
    {
        sRenderer.sprite = spawner.shieldSprite;
        sRenderer.sortingOrder = 6;
    }

    private void Update()
    {
        powerupTimer += Time.deltaTime;

        if (powerupTimer >= powerupLifetime)
            RemoveItem();
    }

    private void RemoveItem()
    {
        gameObject.layer = 0;

        Destroy(sRenderer);
        Destroy(this);
    }
}