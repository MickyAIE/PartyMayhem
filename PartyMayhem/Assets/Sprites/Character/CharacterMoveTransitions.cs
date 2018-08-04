using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveTransitions : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckDirection();
        CheckMovement();
    }

    public void CheckDirection() //0 = down/forward, 1 = up/backwards, 2 = left, 3 = right
    {
        if (Input.GetKey("down"))
            animator.SetInteger("direction", 0);
        else if (Input.GetKey("up"))
            animator.SetInteger("direction", 1);
        else if (Input.GetKey("left"))
            animator.SetInteger("direction", 2);
        else if (Input.GetKey("right"))
            animator.SetInteger("direction", 3);
    }

    public void CheckMovement()
    {
        if (Input.GetKey("down") != true && Input.GetKey("up") != true && Input.GetKey("left") != true && Input.GetKey("right") != true)
            animator.SetBool("isRunning", false);
        else
            animator.SetBool("isRunning", true);
    }
}