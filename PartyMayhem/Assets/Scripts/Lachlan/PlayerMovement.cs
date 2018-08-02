using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 200;
    public float turnSpeed;

    public AudioClip punchSound1;
    public AudioClip punchSound2;

    private void Update()
    {
        Vector3 newPosition = new Vector3(Input.GetAxis("P1 Horizontal"), Input.GetAxis("P1 Vertical"), 0.0f);

        //Vector3 newPosition = new Vector3();
        //newPosition.x = (Input.GetAxis("P1 Horizontal") * speed) * Time.deltaTime;
        //newPosition.y = (Input.GetAxis("P1 Vertical") * speed) * Time.deltaTime;
        //transform.Translate(newPosition);
    }
}
