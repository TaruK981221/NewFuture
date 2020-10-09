using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kari_enemy : MonoBehaviour
{
    Rigidbody2D rigidbody;

    bool Flg = false;

    Vector3 oldPos = Vector3.zero;

    float JumpTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();

        oldPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rigidbody.AddForce(new Vector2(10.0f, 0.0f), ForceMode2D.Force);

        if (!Flg)
        {
            this.transform.position += new Vector3(0.1f, 0.0f);

            //rigidbody.velocity += new Vector2(1.0f, 0.0f);
        }
        else
        {
            this.transform.position += new Vector3(-0.1f, 0.0f);

            //rigidbody.velocity += new Vector2(-1.0f, 0.0f);
        }

        JumpTime += Time.deltaTime;

        if(JumpTime >= 2.0f)
        {
            JumpTime = 0.0f;

            rigidbody.velocity += (new Vector2(0.0f, 10.0f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Flg = !Flg;
        }
    }
}
