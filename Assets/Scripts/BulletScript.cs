using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject playerObject;
    public bool isMovingRight;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLife;  //life of the bullet in seconds. After this time has passed, it will dissapear

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (!isMovingRight)
        {
            bulletSpeed = -bulletSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move bullet
        transform.position += new Vector3(bulletSpeed * Time.deltaTime,0,0);

        if(bulletLife <= 0)
        {
            Destroy(gameObject);
        }
        bulletLife -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == playerObject.GetComponent<Collider2D>())
        {
            playerObject.GetComponent<CharacterController>().Die();
            Destroy(gameObject);
        }
    }
}
