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
        patrolMin = startingPosition - Vector3.right * patrolRange / 2;
        patrolMax = startingPosition + Vector3.right * patrolRange / 2;
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
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= shootRange)
        {
            IfShoot();
        }
        else
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
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);
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
        Vector3 direction = (player.transform.position - transform.position).normalized;
        InstanciateBullet(direction);
        Debug.Log(direction);
        FlipScientist(direction);
    }

    void IfShoot()
    {
        shooting = true;
        patrolling = false;
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            ShootAtPlayer();
            shootTimer = 0f;
        }
    }


}
