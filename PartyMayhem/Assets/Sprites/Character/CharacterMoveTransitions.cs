using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveTransitions : MonoBehaviour
{
    private Animator animator;

    //named ints for easier reading
    readonly int down = 0;
    readonly int up = 1;
    readonly int left = 2;
    readonly int right = 3;

    private int playerNumber;
    private float verticalInput;
    private float horizontalInput;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerNumber = 1; //TO DO: write script to differenciate players and take number from there
    }

    private void Update()
    {
        DetermineInput();
        CheckDirection();
        CheckMovement();
    }

    public void DetermineInput() //takes player number to create input shorthand
    {
        verticalInput = Input.GetAxis("P" + playerNumber + " Vertical");
        horizontalInput = Input.GetAxis("P" + playerNumber + " Horizontal");
    }

    //TO DO: make it better
    public void CheckDirection() //tells the animation controller what direction the player is facing
    {
        if (horizontalInput < 0)
            animator.SetInteger("direction", left);

        else if (horizontalInput > 0)
            animator.SetInteger("direction", right);

        if (verticalInput < 0)
            animator.SetInteger("direction", down);

        else if (verticalInput > 0)
            animator.SetInteger("direction", up);
    }

    public void CheckMovement() //tells the animation controller if player is running or not
    {
        if (verticalInput != 0 || horizontalInput != 0)
            animator.SetBool("isRunning", true);

        else
            animator.SetBool("isRunning", false);
    }
}