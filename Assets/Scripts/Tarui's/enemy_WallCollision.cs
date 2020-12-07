using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_WallCollision : MonoBehaviour
{
    Rigidbody2D rb;

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
        rb = this.GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isWall && collision.tag == "Ground")
        {
            rb.velocity = Vector2.zero;

            if (collision.transform.position.x >= this.transform.parent.position.x)
            {
                this.transform.parent.position +=
                    new Vector3(-0.01f, 0) * this.transform.parent.lossyScale.x;
            }
            else
            {
                this.transform.parent.position +=
                    new Vector3(0.01f, 0) * this.transform.parent.lossyScale.x;
            }

            isWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isWall && collision.tag == "Ground")
        {
            isWall = false;
        }
    }

    // 壁にぶつかると落ちてしまうバグの修正
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isWall && collision.tag == "Ground")
        {
            rb.velocity = Vector2.zero;

            if (collision.transform.position.x >= this.transform.parent.position.x)
            {
                this.transform.parent.position +=
                    new Vector3(-0.01f, 0) * this.transform.parent.lossyScale.x;
            }
            else
            {
                this.transform.parent.position +=
                    new Vector3(0.01f, 0) * this.transform.parent.lossyScale.x;
            }
        }
    }
}
