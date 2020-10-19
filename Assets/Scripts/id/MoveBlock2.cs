using UnityEngine;
using System.Collections;

public class MoveBlock2 : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Vector2 defaultPos;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        defaultPos = transform.position;
    }

    void FixedUpdate()
    {
        rigid.MovePosition(new Vector2(defaultPos.x + Mathf.PingPong(Time.time, 2), defaultPos.y));
    }
}