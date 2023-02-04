using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneActor : MonoBehaviour
{
    public float _followDist = 20f;
    [SerializeField]
    private Transform _chicken;
    [SerializeField]
    private float _droneSpeed = 3f;
    [SerializeField]
    private GameObject _droneSynthesizer;

    //private SpriteRenderer _droneSprite;

    //private void Start() => _droneSprite = GetComponent<SpriteRenderer>();

    private void Update()
    {
        if (_followDist > Vector2.Distance(transform.position, _chicken.position))
        {
            transform.position = Vector2.MoveTowards(transform.position, _chicken.position, Time.deltaTime * _droneSpeed); //drone wil go towards the playe with a set speed
            transform.Rotate(new Vector3(0, 0, 100) * Time.deltaTime * 5); // MENACING ROTATION HAHAHHAHAH

        }

        else if (_followDist < Vector2.Distance(transform.position, _chicken.position))
            transform.Rotate(new Vector3(0, 0, 100) * Time.deltaTime * 20);
        //transform.RotateAround(_droneSynthesizer.transform.position, Vector3.up, 20 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        CharacterController playerScript = collision.gameObject.GetComponent<CharacterController>();
        if (playerScript != null)
        {
            playerScript.Die(); //if player comes inside the vicinity of the drone, they die and are moved to the last checkpoin
        }
    }
}
