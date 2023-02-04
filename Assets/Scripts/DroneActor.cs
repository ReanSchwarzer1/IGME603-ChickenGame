using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneActor : MonoBehaviour
{
    [SerializeField]
    private float _radiusDeath = 1f;
    [SerializeField]
    private float _followDist = 5f;
    [SerializeField]
    private Transform _chicken;
    [SerializeField]
    private float _droneSpeed;


    private void Update()
    {
        float distBetwnPlayerDrone = Vector2.Distance(transform.position, _chicken.position);

        if (distBetwnPlayerDrone < _radiusDeath)
        {
            CharacterController playerScript = _chicken.gameObject.GetComponent<CharacterController>();
            if (playerScript != null)
            {
                playerScript.Die();
            }
        }

        if (distBetwnPlayerDrone < _followDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, _chicken.position, Time.deltaTime * _droneSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController playerScript = collision.gameObject.GetComponent<CharacterController>();
        if (playerScript != null)
        {
            playerScript.Die();
        }
    }
}
