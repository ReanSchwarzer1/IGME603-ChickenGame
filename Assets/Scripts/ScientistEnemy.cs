using System.Collections;
using UnityEngine;

public class ScientistEnemy : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float shootRange = 5f;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private float shootTimer = 0f;
    [SerializeField] private float patrolRange = 10f;

    private AudioSource src;

   // [SerializeField] private GameObject[] points;

    // index for current waypoint
   // private int point_Index = 0;

   [SerializeField] private float speed = 5f; //speed for moving object

    private bool shooting = false;
    private bool patrolling = true;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private Vector3 patrolMin;
    private Vector3 patrolMax;

    public Vector3 rightPos;

    private Vector3 playerPos;
    SpriteRenderer scientistRenderer;
    


    void Start()
    {
        scientistRenderer = GetComponent<SpriteRenderer>(); 
        PatrolMinMax();
        src = this.GetComponent<AudioSource>(); // Get the audio source of the gameobject
    }

    void Update()
    {

        ShootBehaviours();
    }

    void PatrolMinMax() // to adjus values for the patrolling distance for the scientis enemies on the plaforms
    {
        float timer = Time.deltaTime;
        targetPosition = Vector2.MoveTowards(transform.position, patrolMax, timer * speed);
        startingPosition = transform.position;
        rightPos = Vector3.right;
        (patrolMin, patrolMax) = (startingPosition - rightPos * patrolRange / 2, startingPosition + rightPos * patrolRange / 2);
        targetPosition = patrolMax;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

    private void FlipScientist(Vector2 direction)
    {
        //code to flip the scientists towards the side they are jumping in / flip the chicken opposite the side they are throwing the eggs
        if (direction.x < 0)
        {
            scientistRenderer.flipX = true;
        }
        else
        {
            scientistRenderer.flipX = false;
        }
    }

    void ShootBehaviours()
    {
        playerPos = player.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPos);

        switch (distanceToPlayer <= shootRange)
        {
            case true:
                IfShoot();
                break;

            case false:
                shooting = false; //setting bools to false and true respectively
                patrolling = true;
                break;
        }

        if (patrolling)
        {
            Patrol();
        }
    }



    void Patrol()
    {

        /* https://stackoverflow.com/questions/68026399/enemy-not-moving */

        float timer = Time.deltaTime;
        Vector3 playerPosit = this.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * timer);

        switch (targetPosition.x < playerPosit.x)
        {
            case true:
                scientistRenderer.flipX = true;
                scientistRenderer.color = Color.red;
                break;
            case false:
                scientistRenderer.flipX = false;
                scientistRenderer.color = Color.cyan;
                break;
        }

        if (transform.position == targetPosition)
        {
            switch (targetPosition == patrolMax)
            {
                case true:
                    targetPosition = patrolMin;
                    break;
                case false:
                    targetPosition = patrolMax;
                    break;
            }
        }



    }

    void InstanciateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 20f;
    }

    void ShootAtPlayer()
    {
            playerPos = player.transform.position;
            Vector3 direction = (playerPos - transform.position).normalized;
            InstanciateBullet(direction);
            FlipScientist(direction);
            src.Play();
    }

    void IfShoot()
    {
        float timer = Time.deltaTime;
        shooting = true;
        patrolling = false;
        shootTimer += timer;
        if (shootTimer >= shootInterval)
        {
            ShootAtPlayer();
            shootTimer = 0f;
        }
    }


}
