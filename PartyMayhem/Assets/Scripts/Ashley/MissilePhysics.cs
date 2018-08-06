using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePhysics : MonoBehaviour
{
    public Rigidbody2D missile;
    public CircleCollider2D lockOnRange;
    public Transform player;

    public float speed;
    public float turnSpeed;
    public bool isLockedOn = false;

    public float lowerRotLimit;
    public float upperRotLimit;
    public float originalRotation;
    public float maxTurnAngle = 90;

    private void Awake()
    {
        missile = GetComponent<Rigidbody2D>();
        lockOnRange = GetComponent<CircleCollider2D>();

        player = null;
    }

    private void Start()
    {
        originalRotation = missile.rotation;
        lowerRotLimit = originalRotation - maxTurnAngle;
        upperRotLimit = originalRotation + maxTurnAngle;
    }

    private void Update()
    {
        missile.velocity = transform.up * speed;

        if (isLockedOn)
            RotateToTarget();
        else
            missile.angularVelocity = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) //locks on to player within circle collider, only locks on once
    {
        if (player == null && collision.tag == "Player")
        {
            player = collision.transform;
            isLockedOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player != null)
        {
            if (collision.transform == player)
                isLockedOn = false;
        }
    }

    private void RotateToTarget() //points missile toward player up until a max turning point
    {
        Vector2 targetDirection = player.position - transform.position;
        targetDirection.Normalize();

        float rotateAmount = Vector3.Cross(targetDirection, transform.up).z;
        missile.angularVelocity = -rotateAmount * turnSpeed;

        if (missile.rotation <= lowerRotLimit || missile.rotation >= upperRotLimit)
            isLockedOn = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) //destroys missile on collision with player or other missile
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
            DestroyObject(gameObject);
    }
}