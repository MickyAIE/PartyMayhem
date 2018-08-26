using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float rotateSpeed = 3f;
    public float ballSpeed = 6f;

    public GameObject player;
    public GameObject ball;

    public float timer = 2;
    public float startTime = 2;

    public float timer2 = 2;
    public float startTime2 = 2;

    public bool hasThrownBall = false;

    private void Update()
    {
        if (hasThrownBall == false)
        {
            TurnToTarget();

            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                hasThrownBall = true;
            }
        }

        if(hasThrownBall == true)
        {
            ThrowBall();

            /*
            timer2 -= Time.deltaTime;
            if(timer2 <= 0)
            {
                timer = startTime;
                timer2 = startTime2;
                hasThrownBall = false;
            }*/
        }
    }

    public void TurnToTarget()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y
            );

        transform.up = -direction;
    }

    public void ThrowBall()
    {
        ball.transform.position += Vector3.left * Time.deltaTime * ballSpeed;

        Debug.Log("Thrown Ball");
    }
}
