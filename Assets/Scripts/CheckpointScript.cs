using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController playerScript = collision.gameObject.GetComponent<CharacterController>();
        if (playerScript != null)
        {
            GameObject.Find("Checkpoint Tracker").GetComponent<CheckpointTracker>().SpawnPoint = gameObject.transform.position;
        }
    }
}
