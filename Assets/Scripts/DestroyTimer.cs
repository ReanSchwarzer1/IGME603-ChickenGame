using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// destroys the game object after a set time
public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float duration;

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
