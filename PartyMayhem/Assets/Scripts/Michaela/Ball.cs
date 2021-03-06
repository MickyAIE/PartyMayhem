﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Enemy parentEnemy;

    private void Awake()
    {
        parentEnemy = transform.parent.GetComponent<Enemy>();
    }

    private void Update()
    {
        if (parentEnemy.bools.hasThrownBall == true)
            transform.parent = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Shield")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
