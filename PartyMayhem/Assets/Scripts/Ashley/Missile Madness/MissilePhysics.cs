using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePhysics : MonoBehaviour
{
    private MissileMadness manager;

    public Rigidbody2D missile;
    public CircleCollider2D lockOnRange;
    public Transform player;
    public GameObject explosion;

    public float speed;
    public float turnSpeed;
    public float lifeTime; //how long a missile stays alive before exploding itself
    public bool isLockedOn = false;

    public float maxTurnAngle = 90; //limits how much a missile can turn before it loses focus
    public float originalRotation; //angle that missile spawns at
    public float lowerRotLimit; //left rotation limit relative to original rotation
    public float upperRotLimit; //same, but other way

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<MissileMadness>();
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
        missile.velocity = transform.up * speed * Time.deltaTime; //missile gooooo
        lifeTime -= Time.deltaTime;

        if (isLockedOn)
            RotateToTarget();
        else
            missile.angularVelocity = 0;

        if (lifeTime <= 0 || manager.State != MissileMadness.GameState.Game)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //locks-on to first player that enters lockOnRange
    {
        if (player == null && collision.tag == "Player")
        {
            player = collision.transform;
            isLockedOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //resumes regular movement on player's exit from lockOnRange
    {
        if (player != null)
        {
            if (collision.transform == player)
                isLockedOn = false; //prevents locking on to more than one player during missile's lifetime
        }
    }

    private void RotateToTarget() //points missile toward player up until a max turning point
    {
        Vector2 targetDirection = player.position - transform.position;
        targetDirection.Normalize();

        float rotateAmount = Vector3.Cross(targetDirection, transform.up).z;
        missile.angularVelocity = -rotateAmount * turnSpeed * Time.deltaTime;

        if (missile.rotation <= lowerRotLimit || missile.rotation >= upperRotLimit)
            isLockedOn = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) //destroys missile on collision with player or other missile
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            DestroyObject(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SetActive(false);
        }

        Instantiate(explosion, transform.position, transform.rotation);
    }
}