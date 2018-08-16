using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterMoveTransitions scrAnimations;
    public string playerNumber;
    private Rigidbody2D playerRigid;
    public GameObject sRotate;
    public GameObject sPunch;
    public AudioClip punchSound1; public AudioClip punchSound2;

    public float speed;
    [HideInInspector] public float speedMod;
    [HideInInspector] public float turnSpeed;

    [HideInInspector] private float punchCooldown;
    [HideInInspector] public bool isPunching;

    [HideInInspector] public float axisX;
    [HideInInspector] public float axisY;
    [HideInInspector] public float angle;

    public void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        isPunching = false;
    }

    private void Update()
    {
        if(sPunch != null)
        {
            if (isPunching == true)
            {
                sPunch.SetActive(true);
            }
            else
            {
                sPunch.SetActive(false);
            }
        }
        else
        {
            Debug.Log("| MISSING GAME OBJECT |  | Player " + playerNumber + " |  | sPunch |");
        }

        Aim();

        Move();

        if (Input.GetButton("P" + playerNumber + " Punch"))
        {
            Punch();
        }
        punchCooldown -= Time.deltaTime;

        if(punchCooldown <= 0.4f)
        {
            isPunching = false;
        }


    }

    public void Aim()
    {
        axisX = Input.GetAxis("P" + playerNumber + " Aim Horizontal");
        axisY = Input.GetAxis("P" + playerNumber + " Aim Vertical");

        angle = Mathf.Atan2(axisY, axisX) * Mathf.Rad2Deg;
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
            isPunching = true;
            //scrAnimations.Punch();

            Debug.Log("PUNCH " + playerNumber + " " + angle);
            punchCooldown = 0.5f;

            if(sRotate != null)
            {
                sRotate.transform.eulerAngles = new Vector3(sPunch.transform.rotation.x, sPunch.transform.rotation.y, angle + 270);
            }
            else
            {
                Debug.Log("| MISSING GAME OBJECT |  | Player " + playerNumber + " |  | sRotate |");
            }
        }
    }
}
