using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    //Changes the player's layer to one that won't collide with missiles.

    private SpriteRenderer[] sprites;
    private readonly float powerupLifetime = 5f;
    private float powerupTimer = 0f;

    private void Awake()
    {
        sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = Color.red;
        }

        gameObject.layer = 10;
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

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = Color.white;
        }

        Destroy(this);
    }
}