using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    [SerializeField] Animator animatorScript;
    [SerializeField] GameObject peaObject;
    [SerializeField] bool isFacingLeft;
    [SerializeField] bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        //Set up the animator in scripts and set the plant to idleing
        animatorScript = GetComponent<Animator>();
        animatorScript.SetBool("idle", true);
        if (!isFacingLeft)
        {
            //Should flip the plant so it's facing the other way
        }
    }

    // Update is called once per frame
    void Update()
    {
        animatorScript.SetBool("shooting", isShooting);
        animatorScript.SetBool("idle", !isShooting);
    }

    void ShootPea()
    {
        animatorScript.SetBool("shooting", true);
    }

    //have a invisible trigger extending out in front of the pea shooter
    //if the chicken is inside the trigger, repeatedly shoot peas
    //if the chicken leaves the trigger stop shooting and return to idle
}
