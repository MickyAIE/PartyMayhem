using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 200;
    public float speedMod;

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
        Vector3 newPosition = new Vector2(Input.GetAxis("P1 Horizontal"), Input.GetAxis("P1 Vertical"));

        playerRigid.velocity = newPosition * speed * speedMod;

        //Vector3 newPosition = new Vector3();
        //newPosition.x = (Input.GetAxis("P1 Horizontal") * speed) * Time.deltaTime;
        //newPosition.y = (Input.GetAxis("P1 Vertical") * speed) * Time.deltaTime;
        //transform.Translate(newPosition);
    }
    private void FixedUpdate()
    {
        if ((Input.GetAxis("P1 Horizontal") > 0.1 || Input.GetAxis("P1 Horizontal") < -0.1) && (Input.GetAxis("P1 Vertical") > 0.1 || Input.GetAxis("P1 Vertical") < -0.1))
        {
            speedMod = 0.75f;
        }
        else
        {
            speedMod = 1;
        }
    }
}
