using UnityEngine;
using System.Collections;

public class MoveBlock : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Vector3 defaultPos;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        defaultPos = transform.position;
    }

    void FixedUpdate()
    {
        rigid.MovePosition(new Vector2(defaultPos.x, defaultPos.y + Mathf.PingPong(Time.time, 2), defaultPos.z));
    }
}
