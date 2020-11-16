using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_wolf : MonoBehaviour
{
    enum STATUS
    {
        stay=0,
        attack,
        walk,
        Damage,

        end
    }
    enum ATK_STATUS
    {
        stay=0,
        jump,

        end
    }
    
    // こいつの状態
    STATUS status = STATUS.stay;
    ATK_STATUS aStatus = ATK_STATUS.stay;

    // 攻撃対象
    GameObject player = null;

    // 使用する予定のコンポーネント
    Rigidbody2D rb;
    SpriteRenderer sprite;

    // 連続で攻撃しないようにする
    bool isAttackOK = false;
    float AtkOKTime = 0;
    [SerializeField]
    float AtkOKTimeLimit = 2;

    // 攻撃の際に使用
    float AtkTime = 0;
    [SerializeField]
    float AtkStayTimeLimit = 2 , AtkJumpTimeLimit = 2;
    bool AtkFlg = false;

    // 歩行の際に使用
    float WalkTime = 0;
    [SerializeField]
    float WalkTimeLimit = 2;

    // 待機の際に使用
    float StayTime = 0;
    [SerializeField]
    float StayTimeLimit = 2;

    [SerializeField]
    float speed = 10.0f, jumpSpeed = 10.0f;

    // false : 左    true : 右
    bool isLR = false;

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
        // 行動の処理
        Action();

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
        Debug.Log(collision.tag);

        if(collision.tag == "Ground")
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
        }
    }

    // 子オブジェクト的な？の当たり判定を通した処理
    public void AtkCollision()
    {
        // 攻撃可能か
        if (isAttackOK)
        {
            isAttackOK = false;

            status = STATUS.attack;
            aStatus = ATK_STATUS.stay;

            rb.velocity = Vector2.zero;

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

    void Action()
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
                    Stay();
                }
                break;

            // 攻撃
            case STATUS.attack:
                {
                    Attack();
                }
                break;

            // 歩行
            case STATUS.walk:
                {
                    Walk();
                }
                break;

            case STATUS.Damage:
                {
                    Damage();
                }
                break;
        }
    }

    /* Actionの中身 */

    void Stay()
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
                        aStatus = ATK_STATUS.jump;
                        AtkFlg = true;

                        rb.bodyType = RigidbodyType2D.Dynamic;
                    }
                }
                break;

            // 攻撃中のジャンプ(攻撃の内容)
            case ATK_STATUS.jump:
                {
                    if (AtkFlg)
                    {
                        AtkFlg = false;

                        if (isLR)
                        {
                            rb.velocity = new Vector2(jumpSpeed * 1, jumpSpeed * 2);
                        }
                        else
                        {
                            rb.velocity = new Vector2(-jumpSpeed * 1, jumpSpeed * 2);
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

    void Walk()
    {
        // 左右で移動が変わる
        if (isLR)
        {
            rb.velocity = new Vector2(speed, 0.0f);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0.0f);
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

    void Damage()
    {

    }

    /* Actionの中身 end */
}
