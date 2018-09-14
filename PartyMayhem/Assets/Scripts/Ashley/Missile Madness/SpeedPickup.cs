using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    //This script is attached to the player when they pick up the speed boost item.
    //Increases player's speed for a small amount of time.

    private PlayerMovement player;
    private SpriteRenderer[] sprites;
    private readonly float powerupLifetime = 5f;
    private float powerupTimer = 0f;

    private void Awake()
    {
        player = gameObject.GetComponent<PlayerMovement>();
        sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = Color.red;
        }

        if (player != null)
            player.speed = 300f;
    }

    private void Update()
    {
        powerupTimer += Time.deltaTime;

        if (powerupTimer >= powerupLifetime)
            RemoveItem();
    }

    private void RemoveItem()
    {
        if (player != null)
            player.speed = 200f;

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = Color.white;
        }

        Destroy(this);
    }
}