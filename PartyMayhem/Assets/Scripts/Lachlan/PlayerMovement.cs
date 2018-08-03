using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 200;
    public float speedMod;

    public string playerNumber;

    public float turnSpeed;

    Rigidbody2D playerRigid;

    public AudioClip punchSound1;
    public AudioClip punchSound2;

    public void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 newPosition = new Vector2(Input.GetAxis("P" + playerNumber + " Horizontal"), Input.GetAxis("P" + playerNumber + " Vertical"));

        playerRigid.velocity = newPosition * speed * speedMod;
    }
    private void FixedUpdate()
    {
        if ((Input.GetAxis("P" + playerNumber + " Horizontal") > 0.1 || Input.GetAxis("P" + playerNumber + " Horizontal") < -0.1) && (Input.GetAxis("P" + playerNumber + " Vertical") > 0.1 || Input.GetAxis("P" + playerNumber + " Vertical") < -0.1))
        {
            speedMod = 0.75f;
        }
        else
        {
            speedMod = 1;
        }
    }
}
