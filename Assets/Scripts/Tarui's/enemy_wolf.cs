﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_wolf : MonoBehaviour
{
    enum STATUS
    {
        stay=0,
        attack,
        walk,

        end
    }

    enum ATK_STATUS
    {
        stay=0,
        jump,

        end
    }

    [SerializeField]
    STATUS status = STATUS.stay;
    [SerializeField]
    ATK_STATUS aStatus = ATK_STATUS.stay;

    GameObject player = null;

    new Rigidbody2D rb = null;

    new SpriteRenderer sprite = null;

    // 連続で攻撃しないようにするため
    bool isAttackOK = false;
    float AtkOKTime = 0;
    [SerializeField]
    float AtkOKTimeLimit = 2;

    float AtkTime = 0;
    [SerializeField]
    float AtkStayTimeLimit = 2 , AtkJumpTimeLimit = 2;
    bool AtkFlg = false;
    

    // false : 左    true : 右
    bool isLR = false;

    float WalkTime = 0;
    [SerializeField]
    float WalkTimeLimit = 2;

    float StayTime = 0;
    [SerializeField]
    float StayTimeLimit = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb = this.gameObject.GetComponent<Rigidbody2D>();

        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 攻撃が可能か管理
        // [攻撃中]じゃなかったらカウントを開始する
        if (status != STATUS.attack)
        {
            if (!isAttackOK)
            {
                if (AtkOKTimeLimit > AtkOKTime)
                {
                    AtkOKTime += Time.deltaTime;
                }
                else
                {
                    AtkOKTime = 0;

                    isAttackOK = true;
                }
            }
        }


        // 行動の中身
        switch (status)
        {
            // 待機
            case STATUS.stay:
                {
                    if (StayTimeLimit > StayTime)
                    {
                        StayTime += Time.deltaTime;
                    }
                    else
                    {
                        StayTime = 0;
                        status = STATUS.walk;

                        // 左右の方向を決める
                        if (player.transform.position.x >= this.transform.position.x)
                        {
                            isLR = true;
                        }
                        else
                        {
                            isLR = false;
                        }
                    }
                }
                break;

            // 攻撃
            case STATUS.attack:
                {
                    // 攻撃する時間の管理
                    switch (aStatus)
                    {
                        // 攻撃中の待機
                        case ATK_STATUS.stay:
                            {
                                if (AtkStayTimeLimit > AtkTime)
                                {
                                    AtkTime += Time.deltaTime;
                                }
                                else
                                {
                                    AtkTime = 0;
                                    aStatus = ATK_STATUS.jump;
                                    AtkFlg = true;
                                }
                            }
                            break;

                        // 攻撃中のジャンプ(攻撃の内容)
                        case ATK_STATUS.jump:
                            {
                                if(AtkFlg)
                                {
                                    AtkFlg = false;

                                    if(isLR)
                                    {
                                        rb.velocity = new Vector2(4.0f, 8.0f);
                                    }
                                    else
                                    {
                                        rb.velocity = new Vector2(-4.0f, 8.0f);
                                    }
                                }

                                if (AtkJumpTimeLimit > AtkTime)
                                {
                                    AtkTime += Time.deltaTime;
                                }
                                else
                                {
                                    AtkTime = 0;
                                    AtkOKTime = 0;
                                    status = STATUS.stay;
                                    aStatus = ATK_STATUS.stay;
                                }
                            }
                            break;
                    }
                }
                break;

            // 歩行
            case STATUS.walk:
                {
                    // 左右で移動が変わる
                    if (isLR)
                    {
                        rb.velocity = new Vector2(5.0f, 0.0f);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-5.0f, 0.0f);
                    }

                    // 歩行する時間の管理
                    if (WalkTimeLimit > WalkTime)
                    {
                        WalkTime += Time.deltaTime;
                    }
                    else
                    {
                        WalkTime = 0;
                        status = STATUS.stay;

                        rb.velocity = Vector2.zero;
                    }
                }
                break;
        }


        // 左右の向き
        if(isLR)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            // 攻撃可能か
            if (isAttackOK)
            {
                isAttackOK = false;

                status = STATUS.attack;
                aStatus = ATK_STATUS.stay;

                rb.velocity = Vector2.zero;

                if(collision.transform.position.x >= this.transform.position.x)
                {
                    isLR = true;
                }
                else
                {
                    isLR = false;
                }
            }
        }
    }
}
