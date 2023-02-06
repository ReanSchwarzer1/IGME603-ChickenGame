using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{
    private BoxCollider2D trampolineCollider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        trampolineCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //trampolineCollider.size = new Vector2(spriteRenderer.bounds.size.x/2, spriteRenderer.bounds.size.y/2);
        //Debug.Log(spriteRenderer.bounds.size);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float xPos = collision.gameObject.transform.position.x;
            if(xPos <= transform.position.x - transform.localScale.x / 2 || xPos >= transform.position.x + transform.localScale.x / 2)
            {
                // horizontal bounce
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            } else {
                // vertical bounce
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
            }
        }
    }
}
