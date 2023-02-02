using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // make sure the object made contact from the top or bottom
            float xPos = collision.gameObject.transform.position.x;
            if(xPos <= transform.position.x - transform.localScale.x / 2 || xPos >= transform.position.x + transform.localScale.x / 2)
            {
                return;
            }

            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
    }
}
