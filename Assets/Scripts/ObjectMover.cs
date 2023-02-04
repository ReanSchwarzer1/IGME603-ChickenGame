using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private GameObject[] points;

    // index for current waypoint
    private int point_Index = 0;

    [SerializeField] private float speed = 5f; //speed for moving platform

    private void Update()
    {
        //if the distance between currrent waypoint index and next index is negligible
        if (Vector2.Distance(points[point_Index].transform.position, transform.position) < 0.5f)
        {
            point_Index++; //increment all indexes
            //switch to next waypoint in index
            if (point_Index >= points.Length)
            {
                point_Index = 0;
            }
        }
        //moving the platform towards next waypoint index
        transform.position = Vector2.MoveTowards(transform.position, points[point_Index].transform.position, Time.deltaTime * speed);
    }



    /* code for the player to stick to the platform (not working properly maybe because
       the lizard is the parent of the cylinder which is the "player"?) */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
