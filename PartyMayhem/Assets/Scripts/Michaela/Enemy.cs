using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float rotateSpeed = 3f;
    public float ballSpeed = 6f;

    public GameObject player;
    public GameObject ballPrefab;
    public GameObject ballSpawn;

    public float timerToThrow = 2;
    public float startTimeToThrow = 2;

    public float timerToWait = 2;
    public float startTimeToWait = 2;

    public bool hasThrownBall = false;
    public bool hasSpawnedBall = false;

    public GameObject ball;

    private void Update()
    {
        if (hasThrownBall == false)
        {
            TurnToTarget();

            if (hasSpawnedBall == false)
            {
                ball = Instantiate(ballPrefab, ballSpawn.transform.position, ballSpawn.transform.rotation, gameObject.transform);
                hasSpawnedBall = true;
            }

            timerToThrow -= Time.deltaTime;
            if(timerToThrow <= 0)
            {
                hasThrownBall = true;

            }
        }

        if (hasThrownBall == true)
        {
            hasSpawnedBall = false;
            ThrowBall();
            
            timerToWait -= Time.deltaTime;
            if(timerToWait <= 0)
            {
                timerToThrow = startTimeToThrow;
                timerToWait = startTimeToWait;
                Destroy(ball);

                hasThrownBall = false;
            }
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
        float rotation = gameObject.transform.rotation.z;
        ball.transform.position += Vector3.left * Time.deltaTime * ballSpeed;
    }
}
