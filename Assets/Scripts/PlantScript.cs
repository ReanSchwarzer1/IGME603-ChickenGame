using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    //reference to animator for swapping between animation states
    [SerializeField] Animator animatorScript;
    //trigger for detecting when the player is "visible" to the enemy
    [SerializeField] BoxCollider2D areaTrigger;
    //reference to the pea projectile the plant shoots
    [SerializeField] GameObject peaObject;
    //reference to the specific point from which the pea spawns
    [SerializeField] Transform peaLocation;
    //the speed at which the plant will move along the platform when patrolling
    [SerializeField] float patrolSpeed;
    //a bool to determine whether the plant is facing left or right
    [SerializeField] bool isFacingRight;
    //a bool to determine whether the plant is a patrol type, or stationary type
    [SerializeField] bool isPatroller;
    //a reference to the collider of the platform the plant is on so it can stay within its bounds
    [SerializeField] Collider2D platformCollider;
    //a reference to the player
    GameObject playerObject;
    //a bool to determine whether the plant is current shooting peas or not
    private bool isShooting;

    private AudioSource src;

    //enum states for enemy states, either walking or aggro'd on the player
    enum AIStates
    {
        Walking = 1,
        Aggro = 2
    }
    AIStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        //Set up the animator in scripts and set the plant to idleing
        currentState = AIStates.Walking;
        animatorScript = GetComponent<Animator>();
        animatorScript.SetBool("idle", true);
        if (isFacingRight)
        {
            //Should flip the plant so it's facing the other way
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, 1);
        }
        playerObject = GameObject.FindGameObjectWithTag("Player");
        src = this.GetComponent<AudioSource>(); // Get the audio source of the gameobject
    }

    // Update is called once per frame
    void Update()
    {
        //switch (currentState)
        //{
        //    case AIStates.Walking:
        //        break;
        //    case AIStates.Aggro:
        //        break;
        //    default:
        //        break;
        //}

        //if (playerObject.GetComponent<Collider2D>().IsTouching(areaTrigger))
        //{
        //    isShooting = true;
        //    currentState = AIStates.Aggro;
        //}
        //else
        //{
        //    isShooting = false;
        //    currentState = AIStates.Walking;
        //}

        //animatorScript.SetBool("shooting", isShooting);
        //animatorScript.SetBool("idle", !isShooting);
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case AIStates.Walking:
                if (isPatroller)
                {
                    PatrolPlatform();
                }
                break;
            case AIStates.Aggro:
                break;
            default:
                if (isPatroller)
                {
                    PatrolPlatform();
                }
                break;
        }

        //if the player is whithin the visible range, the plant goes into aggro mode
        if (playerObject.GetComponent<Collider2D>().IsTouching(areaTrigger))
        {
            isShooting = true;
            currentState = AIStates.Aggro;
        }
        else
        {
            isShooting = false;
            currentState = AIStates.Walking;
        }

        animatorScript.SetBool("shooting", isShooting);
        animatorScript.SetBool("idle", !isShooting);
    }

    //The plant will move along their given platform until they reach its edge, at which point they'll swap directions and repeat
    void PatrolPlatform()
    {
        gameObject.transform.position += new Vector3(patrolSpeed, 0, 0);

        //if reaches the end of the platform
        if (gameObject.transform.position.x <= platformCollider.bounds.min.x || gameObject.transform.position.x >= platformCollider.bounds.max.x)
        {
            patrolSpeed *= -1;
            isFacingRight = !isFacingRight;
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, 1);
        }
    }

    //The plan shoots a pea. The movement is handled by the pea, so this just instantiates it
    void ShootPea()
    {
        GameObject pea = Instantiate(peaObject, peaLocation.transform.position, Quaternion.identity);
        BulletScript peaLogic = pea.GetComponent<BulletScript>();
        peaLogic.isMovingRight = isFacingRight;
        src.Play();
    }

    //have a invisible trigger extending out in front of the pea shooter
    //if the chicken is inside the trigger, repeatedly shoot peas
    //if the chicken leaves the trigger stop shooting and return to idle
}
