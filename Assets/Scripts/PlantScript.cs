using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    [SerializeField] Animator animatorScript;
    [SerializeField] BoxCollider2D areaTrigger;
    [SerializeField] GameObject peaObject;
    [SerializeField] bool isFacingRight;
    GameObject playerObject;
    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        //Set up the animator in scripts and set the plant to idleing
        animatorScript = GetComponent<Animator>();
        animatorScript.SetBool("idle", true);
        if (isFacingRight)
        {
            //Should flip the plant so it's facing the other way
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, 1);
        }
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject.GetComponent<Collider2D>().IsTouching(areaTrigger))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

        animatorScript.SetBool("shooting", isShooting);
        animatorScript.SetBool("idle", !isShooting);
    }

    void ShootPea()
    {
        Debug.Log("The Pea has been shot");
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.IsTouching(areaTrigger))
    //    {
    //        isShooting = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (!collision.IsTouching(areaTrigger))
    //    {
    //        isShooting = false;
    //    }
    //}

    //have a invisible trigger extending out in front of the pea shooter
    //if the chicken is inside the trigger, repeatedly shoot peas
    //if the chicken leaves the trigger stop shooting and return to idle
}
