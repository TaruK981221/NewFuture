﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_one : MonoBehaviour
{
    enum ENEMY_STATUS
    {
        Move=0,
        attack,
        Wait,

        end
    }

    [SerializeField]
    float MoveDis = 10.0f;

    [SerializeField]
    GameObject bullet = null;

    bool MoveFlg = true;
    bool attackFlg = false;

    bool LRFlg = false;

    [SerializeField]
    ENEMY_STATUS status = ENEMY_STATUS.Wait;

    const float moveTime = 3.0f;
    const float waitTime = 3.0f;
    const float attackTime = 1.5f;

    float actionTime = 0.0f;

    Rigidbody2D rigidbody = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (status)
        {
            case ENEMY_STATUS.Move:
                {
                    if(actionTime < moveTime)
                    {
                        actionTime += Time.deltaTime;

                        // ここで移動の挙動
                        if (LRFlg)
                        {
                            rigidbody.velocity = new Vector2(-5.0f, 0.0f);
                        }
                        else
                        {
                            rigidbody.velocity = new Vector2(5.0f, 0.0f);
                        }

                        // ここで移動モーション
                    }
                    else
                    {
                        actionTime = 0.0f;

                        rigidbody.velocity = Vector2.zero;

                        status = ENEMY_STATUS.Wait;
                    }
                }
                break;

            case ENEMY_STATUS.attack:
                {
                    if (actionTime < attackTime)
                    {
                        actionTime += Time.deltaTime;

                        if (!attackFlg && actionTime > attackTime / 2)
                        {
                            GameObject obj;
                            obj = Instantiate(bullet, this.transform.position, Quaternion.identity);

                            obj.GetComponent<enemy_bullet>().LRFLG = LRFlg;

                            attackFlg = true;
                        }

                        // ここで攻撃モーション
                    }
                    else
                    {
                        attackFlg = false;
                        actionTime = 0.0f;

                        status = ENEMY_STATUS.Wait;
                    }
                }
                break;

            case ENEMY_STATUS.Wait:
                {
                    if (actionTime < waitTime)
                    {
                        actionTime += Time.deltaTime;

                        // ここで待機モーション
                    }
                    else
                    {
                        actionTime = 0.0f;

                        if (MoveFlg)
                        {
                            status = ENEMY_STATUS.Move;
                            MoveFlg = false;
                        }
                        else
                        {
                            status = ENEMY_STATUS.attack;
                            MoveFlg = true;
                        }
                    }
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "wall")
        {
            LRFlg = !LRFlg;
        }
    }
}
