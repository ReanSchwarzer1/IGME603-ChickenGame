using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController playerScript = collision.gameObject.GetComponent<CharacterController>();
        if (playerScript != null)
        {
            playerScript.Die();
        }
    }
}
