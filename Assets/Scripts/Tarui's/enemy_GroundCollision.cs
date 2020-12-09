using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_GroundCollision : MonoBehaviour
{
    Rigidbody2D rb;
    
    bool isGround = false;
    public bool IsGround
    {
        get
        {
            return isGround;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGround && collision.tag == "Ground")
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;

            isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isGround && collision.tag == "Ground")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

            isGround = false;
        }
    }

    // 壁にぶつかると落ちてしまうバグの修正
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            if(!isGround)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;

                this.transform.parent.position += 
                    new Vector3(0, 0.05f) * this.transform.parent.lossyScale.x;
            }
        }
    }
}
