using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string playerNumber;

    Rigidbody2D playerRigid;

    public AudioClip punchSound1; public AudioClip punchSound2;

    public float speed = 300;
    [HideInInspector] public float speedMod;

    [HideInInspector] public float turnSpeed;

    [HideInInspector] private float punchCooldown;

    public void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        if (Input.GetButton("P" + playerNumber + " Punch"))
        {
            Punch();
        }
        punchCooldown -= Time.deltaTime;
    }

    public void Move()
    {
        Vector3 newPosition = new Vector2(Input.GetAxis("P" + playerNumber + " Horizontal"), Input.GetAxis("P" + playerNumber + " Vertical"));

        playerRigid.velocity = newPosition * speed * speedMod * Time.deltaTime;

        if ((Input.GetAxis("P" + playerNumber + " Horizontal") > 0.1 || Input.GetAxis("P" + playerNumber + " Horizontal") < -0.1) && (Input.GetAxis("P" + playerNumber + " Vertical") > 0.1 || Input.GetAxis("P" + playerNumber + " Vertical") < -0.1))
        {
            speedMod = 0.75f;
        }
        else
        {
            speedMod = 1;
        }
    }

    public void Punch()
    {
        if (punchCooldown <= 0)
        {
            Debug.Log("PUNCH " + playerNumber);
            punchCooldown = 0.5f;
        }
    }
}
