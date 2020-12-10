using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_WallCollision : MonoBehaviour
{
    [SerializeField,Header("false : 左　true : 右")]
    bool LRFlg = false;

    Rigidbody2D rb;

    Transform parent;

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

        parent = rb.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isWall && collision.tag == "Ground")
        {
            rb.velocity *= new Vector2(0,1);

            if(LRFlg)
            {
                parent.position +=
                    new Vector3(-0.01f,0) * parent.lossyScale.x;
            }
            else
            {
                parent.position +=
                    new Vector3(0.01f, 0) * parent.lossyScale.x;
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
            rb.velocity *= new Vector2(0, 1);

            if (LRFlg)
            {
                parent.position +=
                    new Vector3(-0.01f, 0) * parent.lossyScale.x;
            }
            else
            {
                parent.position +=
                    new Vector3(0.01f, 0) * parent.lossyScale.x;
            }
        }
    }
}
