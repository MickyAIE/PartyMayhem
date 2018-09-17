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

    public float Movetimer = .9f;
    public float moveStartTime = .9f;
}

[System.Serializable]
public class Bools
{
    public bool hasThrownBall = false;
    public bool hasSpawnedBall = false;

    public bool hasMoved = false;

    public bool hasPickedNumber = false;
    public bool hasPickedNumber2 = false;

    public bool hasChosenTarget = false;

    public bool hasBeenHit = false;
}

public class Enemy : MonoBehaviour {

    public Timers timers;
    public Bools bools;

    private GameManager gameManager;
    private DodgeballManager dodgeballManager;

    public GameObject player;
    public GameObject ballPrefab;
    public GameObject ballSpawn;
    public GameObject ball;

    public float rotateSpeed = 3f;
    public float ballSpeed = 6f;

    public float onStartMoveDistance;
    public float randomMoveDistance = 2;

    public int randomNumber;

    public RaycastHit hit;
    public float rayDistance = 1;


    private void Start()
    {
        dodgeballManager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<DodgeballManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (gameManager.difficultyIndex == 1) ballSpeed = 3.5f;     
        if (gameManager.difficultyIndex == 2) ballSpeed = 5f;    
        if (gameManager.difficultyIndex == 3) ballSpeed = 7f;

        bools.hasThrownBall = false;
        bools.hasSpawnedBall = false;
        bools.hasMoved = false;
        bools.hasPickedNumber = false;

        int rand = Random.Range(0, 2);

        //Move either left or right a bit
        onStartMoveDistance = Random.Range(0f, 1.1f);
        if(rand == 0)
        {
            transform.position += Vector3.left * onStartMoveDistance;
        }
        else
        {
            transform.position += Vector3.right * onStartMoveDistance;
        }
    }

    private void Update()
    {
        if(dodgeballManager.endGame == true)
        {
            return;
        }

        //If hasn't thrown a ball yet
        if (bools.hasThrownBall == false)
        {
            shouldMakeSound = true;
            TurnToTarget();

            bools.hasPickedNumber = false;
            bools.hasMoved = false;

            //If hasn't spawned in a ball yet, spawn one
            if (bools.hasSpawnedBall == false)
            {
                ball = Instantiate(ballPrefab, ballSpawn.transform.position, ballSpawn.transform.rotation, gameObject.transform);
                bools.hasSpawnedBall = true;
            }

            PickRandomTime();

            //Throw ball
            timers.throwTimer -= Time.deltaTime;
            if (timers.throwTimer <= 0)
            {
                bools.hasThrownBall = true;
            }
        }

        //If has thrown ball 
        if (bools.hasThrownBall == true)
        {
            bools.hasSpawnedBall = false;

            ThrowBall();
            RandomMove();

            //Wait for a couple seconds before starting the process again
            timers.waitTimer -= Time.deltaTime;
            if (timers.waitTimer <= 0)
            {
                timers.throwTimer = timers.throwStartTime;
                timers.waitTimer = timers.waitStartTime;
                timers.waitToMoveTimer = timers.waitToMoveStartTime;
                bools.hasPickedNumber2 = false;
                bools.hasChosenTarget = false;
                Destroy(ball);

                bools.hasThrownBall = false;
            }
        }
        
    }

    //Aim at the player
    public void TurnToTarget()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (bools.hasChosenTarget == false)
        {
            player = dodgeballManager.players[Random.Range(0, dodgeballManager.players.Length)];
            if (CheckTargetIsAlive(player))
            {
                bools.hasChosenTarget = true;
            }
        }

        if(bools.hasChosenTarget == true)
        {
            Vector2 direction = new Vector2(
                player.transform.position.x - transform.position.x,
                player.transform.position.y - transform.position.y
                );

            transform.up = -direction;
        }
    }

    public bool CheckTargetIsAlive(GameObject p)
    {
        if(p.gameObject.name == "Player1" && dodgeballManager.playerOneHasBeenHit == true)
        {
            return false;
        }
        if(p.gameObject.name == "Player1" && dodgeballManager.playerOneHasBeenHit == false)
        {
            return true;
        }

        if (p.gameObject.name == "Player2" && dodgeballManager.playerTwoHasBeenHit == true)
        {
            return false;
        }
        if (p.gameObject.name == "Player2" && dodgeballManager.playerTwoHasBeenHit == false)
        {
            return true;
        }

        if (p.gameObject.name == "Player3" && dodgeballManager.playerThreeHasBeenHit == true)
        {
            return false;
        }
        if (p.gameObject.name == "Player3" && dodgeballManager.playerThreeHasBeenHit == false)
        {
            return true;
        }

        if (p.gameObject.name == "Player4" && dodgeballManager.playerFourHasBeenHit == true)
        {
            return false;
        }
        if (p.gameObject.name == "Player4" && dodgeballManager.playerFourHasBeenHit == false)
        {
            return true;
        }
        else if(dodgeballManager.allPlayersHit == true)
        {
            return false;
        }
        else
        {
            Debug.LogWarning("Couldn't check targeted player");
            return false;
        }
    }

    bool shouldMakeSound = true;

    //Throw the ball
    public void ThrowBall()
    {
        if(ball != null)
        {
            if(shouldMakeSound == true)
            {
                GetComponent<AudioSource>().Play();
                shouldMakeSound = false;
            }
            ball.transform.position += -transform.TransformDirection(Vector3.up) * Time.deltaTime * ballSpeed;
        }
    }

    //Move randomly either up or down
    public void RandomMove()
    {
        bool shouldMove = true;

        Debug.DrawRay(transform.position, Vector3.up * rayDistance);
        if (Physics.Raycast(transform.position, Vector3.up, out hit, rayDistance))
        {
            timers.Movetimer -= Time.deltaTime;
            if(timers.Movetimer > 0)
            {
                gameObject.transform.position += Vector3.down * Time.deltaTime;
            }
            else
            {
                shouldMove = false;
                timers.Movetimer = timers.moveStartTime;
                bools.hasMoved = true;
            }
        }

        Debug.DrawRay(transform.position, Vector3.down * rayDistance);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
        {
            timers.Movetimer -= Time.deltaTime;
            if (timers.Movetimer > 0)
            {
                gameObject.transform.position += Vector3.up * Time.deltaTime;
            }
            else
            {
                shouldMove = false;
                timers.Movetimer = timers.moveStartTime;
                bools.hasMoved = true;
            }
        }

        //Wait for a couple seconds before moving (so the ball is thrown first)
        timers.waitToMoveTimer -= Time.deltaTime;
        if(timers.waitToMoveTimer <= 0 && shouldMove == true)
        {
            //If hasn't picked a number yet, pick a random number
            if (bools.hasPickedNumber == false)
            {
                randomNumber = Random.Range(0, 2);
                bools.hasPickedNumber = true;
            }

            //If enemy has not moved yet
            if (bools.hasMoved == false)
            {
                //Determines how long enemy moves for
                timers.Movetimer -= Time.deltaTime;

                //If number is 0, move up
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
                //If number is 1, move down
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

    //Pick a random number (to determine when to throw ball)
    public void PickRandomTime()
    {
        //If number hasn't been picked yet, pick a random number
        if(bools.hasPickedNumber2 == false)
        {
            timers.throwTimer = Random.Range(1f, timers.throwStartTime + 0.1f);
            timers.throwStartTime = timers.throwTimer;

            bools.hasPickedNumber2 = true;
        }
    }
}
