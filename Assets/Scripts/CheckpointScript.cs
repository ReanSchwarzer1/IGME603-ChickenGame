using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] Animator animatorScript;

    private void Start()
    {
        animatorScript = GetComponent<Animator>();
        animatorScript.SetBool("Idle", true);
        animatorScript.SetBool("CheckFlagOut", false);
        animatorScript.SetBool("CheckFlagIdle", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController playerScript = collision.gameObject.GetComponent<CharacterController>();
        if (playerScript != null)
        {
            GameObject.Find("Checkpoint Tracker").GetComponent<CheckpointTracker>().SpawnPoint = gameObject.transform.position;
            animatorScript.SetBool("Idle", false);
            animatorScript.SetBool("CheckFlagOut", true);
        }  
    }

    void SetFlagOutIdle()
    {
        Debug.Log("TheFlagIsIdle");
        animatorScript.SetBool("CheckFlagOut", false);
        animatorScript.SetBool("CheckFlagIdle", true);
    }
}
