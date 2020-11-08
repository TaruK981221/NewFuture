using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bard : MonoBehaviour
{
    enum STATUS
    {
        fly = 0,
        attack,

        end
    }
    enum ATK_STATUS
    {
        stay=0,
        fall,       // 降下
        rise,       // 上昇

        end
    }

    // こいつの状態
    STATUS status = STATUS.fly;
    ATK_STATUS aStatus = ATK_STATUS.stay;

    // 使用する予定のコンポーネント
    Rigidbody2D rb;
    SpriteRenderer sr;

    // 攻撃対象
    GameObject player = null;

    // 連続で攻撃しないようにする
    bool isAttackOK = false;
    float AtkOKTime = 0;
    [SerializeField]
    float AtkOKTimeLimit = 2;

    // 攻撃の際に使用
    float AtkTime = 0;
    [SerializeField]
    float AtkStayTimeLimit = 2, AtkJumpTimeLimit = 2;
    bool AtkFlg = false;

    // 飛行の際に使用
    float FlyTime = 0;
    [SerializeField]
    float FlyTimeLimit = 2;
    [SerializeField, Header("縦の揺れの幅(周波数)")]
    float T = 1.0f;

    // false : 左    true : 右
    bool isLR = false;

    float StartY = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");

        StartY = this.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Action();
    }

    void Action()
    {
        switch (status)
        {
            case STATUS.fly:
                {
                    Fly();
                }
                break;
            case STATUS.attack:
                {
                    Attack();
                }
                break;
        }
    }

    /* Actionの中身 */

    void Fly()
    {
        // 歩行する時間の管理
        if (FlyTimeLimit > FlyTime)
        {
            FlyTime += Time.deltaTime;
            if(FlyTimeLimit < FlyTime)
            {
                FlyTime = FlyTimeLimit;
            }
        }
        else
        {
            FlyTime = 0;
        }

        // 左右で移動が変わる
        if (isLR)
        {
            rb.velocity = new Vector2(5.0f, 0.0f);
        }
        else
        {
            rb.velocity = new Vector2(-5.0f, 0.0f);
        }

        // 上下の揺れ運動
        float X = this.transform.position.x;
        float time = FlyTime / FlyTimeLimit;

        float Y = Mathf.Sin(2 * Mathf.PI * T * time) + StartY;

        this.transform.position = new Vector3(X, Y, 0.0f);
    }

    void Attack()
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
                        aStatus = ATK_STATUS.fall;
                        AtkFlg = true;
                    }
                }
                break;

            // 攻撃中のジャンプ(攻撃の内容)
            case ATK_STATUS.fall:
                {
                    if (AtkFlg)
                    {
                        AtkFlg = false;

                        if (isLR)
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
                        status = STATUS.fly;
                        aStatus = ATK_STATUS.stay;
                    }
                }
                break;

            case ATK_STATUS.rise:
                {

                }
                break;
        }

    }


    /* Actionの中身 end */
}
