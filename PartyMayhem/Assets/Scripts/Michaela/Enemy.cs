using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timers
{
    public float waitToMoveTimer = 2;
    public float waitToMoveStartTime = 2;

    public float throwTimer = 2;
    public float throwStartTime = 2;

    public float waitTimer = 3;
    public float waitStartTime = 3;

    public float Movetimer = .5f;
    public float moveStartTime = .5f;
}

[System.Serializable]
public class Bools
{
    public bool hasThrownBall = false;
    public bool hasSpawnedBall = false;

    public bool hasMoved = false;
    public bool hasPickedNumber = false;
}

public class Enemy : MonoBehaviour {

    public Timers timers;
    public Bools bools;

    public float rotateSpeed = 3f;
    public float ballSpeed = 6f;

    public int randomNumber;

    public GameObject player;
    public GameObject ballPrefab;
    public GameObject ballSpawn;
    public GameObject ball;

    private void Start()
    {
        bools.hasThrownBall = false;
        bools.hasSpawnedBall = false;
        bools.hasMoved = false;
        bools.hasPickedNumber = false;
    }

    private void Update()
    {
        if (bools.hasThrownBall == false)
        {
            TurnToTarget();

            bools.hasPickedNumber = false;
            bools.hasMoved = false;

            if (bools.hasSpawnedBall == false)
            {
                ball = Instantiate(ballPrefab, ballSpawn.transform.position, ballSpawn.transform.rotation, gameObject.transform);
                bools.hasSpawnedBall = true;
            }

            timers.throwTimer -= Time.deltaTime;
            if(timers.throwTimer <= 0)
            {
                bools.hasThrownBall = true;
            }
        }

        if (bools.hasThrownBall == true)
        {
            bools.hasSpawnedBall = false;
            ThrowBall();
            RandomMove();

            timers.waitTimer -= Time.deltaTime;
            
            if(timers.waitTimer <= 0)
            {
                timers.throwTimer = timers.throwStartTime;
                timers.waitTimer = timers.waitStartTime;
                timers.waitToMoveTimer = timers.waitToMoveStartTime;
                Destroy(ball);

                bools.hasThrownBall = false;
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
        ball.transform.position += -transform.TransformDirection(Vector3.up) * Time.deltaTime * ballSpeed;
    }

    public void RandomMove()
    {
        timers.waitToMoveTimer -= Time.deltaTime;
        if(timers.waitToMoveTimer <= 0)
        {
            if (bools.hasPickedNumber == false)
            {
                randomNumber = Random.Range(0, 2);
                bools.hasPickedNumber = true;
            }

            if (bools.hasMoved == false)
            {
                timers.Movetimer -= Time.deltaTime;
                if (randomNumber == 0)
                {
                    if (timers.Movetimer > 0)
                    {
                        gameObject.transform.position += Vector3.up * Time.deltaTime;
                    }
                    else
                    {
                        bools.hasMoved = true;
                        timers.Movetimer = timers.moveStartTime;
                    }
                }
                if (randomNumber == 1)
                {
                    if (timers.Movetimer > 0)
                    {
                        gameObject.transform.position += Vector3.down * Time.deltaTime;
                    }
                    else
                    {
                        bools.hasMoved = true;
                        timers.Movetimer = timers.moveStartTime;
                    }
                }
            }
        }
    }
}
