﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveTransitions : MonoBehaviour
{
    public Animator animator;

    //named ints for easier reading
    readonly int down = 0;
    readonly int up = 1;
    readonly int left = 2;
    readonly int right = 3;

    public int playerNumber = 1;
    public float verticalInput;
    public float horizontalInput;
    public bool punchInput;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        punchInput = Input.GetButtonDown("P" + playerNumber + " Punch");
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

    public void Punch() //sets punch trigger when punch input is pressed
    {
        animator.SetTrigger("punch");
    }
}