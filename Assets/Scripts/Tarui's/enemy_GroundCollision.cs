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

    bool isWall = false;
    public bool IsWall
    {
        get
        {
            return isWall;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "wall")
        {
            rb.velocity = Vector2.zero;

            if(collision.transform.position.x >= this.transform.position.x)
            {
                this.transform.position += new Vector3(-0.05f,0);
            }
            else
            {
                this.transform.position += new Vector3(0.05f, 0);
            }

            isWall = true;
        }

        if (collision.tag == "Ground")
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;

            isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

            isGround = false;
        }

        if(collision.tag == "wall")
        {
            isWall = false;
        }
    }

    // 壁にぶつかると落ちてしまうバグの修正
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            rb.velocity = Vector2.zero;

            if (collision.transform.position.x >= this.transform.position.x)
            {
                this.transform.position += new Vector3(-0.05f, 0);
            }
            else
            {
                this.transform.position += new Vector3(0.05f, 0);
            }
        }

        if (collision.tag == "Ground")
        {
            if(!isGround)
            {
                this.transform.position += new Vector3(0, 0.05f);
            }
        }
    }
}
