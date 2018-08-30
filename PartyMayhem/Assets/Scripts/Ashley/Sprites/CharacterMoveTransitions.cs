using System.Collections;
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

    public bool isPaused = false; //set elsewhere to stop inputs affecting animator

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isPaused)
        {
            DetermineInput();
            CheckDirection();
            CheckMovement();
        }
    }

    public void DetermineInput() //takes player number to create input shorthand
    {
        verticalInput = Input.GetAxis("P" + playerNumber + " Vertical");
        horizontalInput = Input.GetAxis("P" + playerNumber + " Horizontal");
        punchInput = Input.GetButtonDown("P" + playerNumber + " Punch");
    }

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

    public void Punch() //referenced in PlayerMovement to set punch trigger
    {
        animator.SetTrigger("punch");
    }

    public void ResetAnimation() //use to reset animator to default state
    {
        animator.SetInteger("direction", down);
        animator.SetBool("isRunning", false);
    }
}