using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kari_enemy : MonoBehaviour
{
    new Rigidbody2D rigidbody = null;

    bool Flg = false;

    Vector3 oldPos = Vector3.zero;
    Vector3 oldStartPos = Vector3.zero;

    float JumpTime = 0.0f;

    float jumpT = 0.0f;

    [SerializeField]
    bool JumpFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();

        oldPos = this.transform.position;

        jumpT = Random.Range(0.5f, 2.0f);
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

        if (!JumpFlg)
        {
            JumpTime += Time.deltaTime;

            if (JumpTime >= jumpT)
            {
                JumpTime = 0.0f;

                rigidbody.velocity += (new Vector2(0.0f, 10.0f));

                jumpT = Random.Range(0.5f, 2.0f);

                JumpFlg = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "enemy")
        {
            Flg = !Flg;
        }
        else
        {
            JumpFlg = false;
        }
    }
}
