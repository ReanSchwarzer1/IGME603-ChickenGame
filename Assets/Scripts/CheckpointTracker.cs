using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// store the respawn point and don't go away when the scene resets
public class CheckpointTracker : MonoBehaviour
{
    private static CheckpointTracker instance;
    public Vector3 SpawnPoint { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // limit one in existence while keeping it after resetting the scene
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
