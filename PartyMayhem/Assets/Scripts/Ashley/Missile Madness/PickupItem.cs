using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    //Attaches a powerup to player when triggered.

    private ItemSpawner spawner;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<ItemSpawner>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<ShieldPickup>() == null && collision.gameObject.GetComponent<SpeedPickup>() == null)
            {
                if (gameObject.tag == "ShieldPickup")
                {
                    collision.gameObject.AddComponent<ShieldPickup>();
                }

                if (gameObject.tag == "SpeedPickup")
                {
                    collision.gameObject.AddComponent<SpeedPickup>();
                }

                spawner.canSpawn = true;

                Destroy(gameObject);
            }
        }
    }
}