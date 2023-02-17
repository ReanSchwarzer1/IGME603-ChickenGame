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

    private Vector3 playerPos;
    SpriteRenderer scientistRenderer;
    


    void Start()
    {
        scientistRenderer = GetComponent<SpriteRenderer>(); 
        PatrolMinMax();
    }

    void Update()
    {


        ShootBehaviours();
    }

    void PatrolMinMax() // to adjus values for the patrolling distance for the scientis enemies on the plaforms
    {
        startingPosition = transform.position;
        (patrolMin, patrolMax) = (startingPosition - Vector3.right * patrolRange / 2, startingPosition + Vector3.right * patrolRange / 2);
        targetPosition = patrolMax;
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
        if (distanceToPlayer <= shootRange)
        {
            IfShoot();
        }
        else if (distanceToPlayer > shootRange)
        {
            shooting = false;
            patrolling = true;
        }

        if (patrolling)
        {
            Patrol();
        }
    }



    void Patrol()
    {
        float timer = Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * timer);
        if(targetPosition.x < this.transform.position.x)
        {
            scientistRenderer.flipX = true;
        }
        else
        {
            scientistRenderer.flipX = false;
        }
        if (transform.position == targetPosition)
        {
            if (targetPosition == patrolMax)
            {
                targetPosition = patrolMin;
            }
            else
            {
                targetPosition = patrolMax;
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
