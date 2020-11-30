using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_GroundNotFall : MonoBehaviour
{
    Rigidbody2D rb = null;

    bool isNotFall = false;
    public bool IsNotFall
    {
        get
        {
            return isNotFall;
        }
    }

    private void Start()
    {
        rb = this.GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(isNotFall)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(collision.tag == "Ground"))
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Dynamic;
            isNotFall = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            isNotFall = true;
        }
    }
}
