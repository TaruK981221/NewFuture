using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bard : MonoBehaviour
{
    enum STATUS
    {
        fly = 0,
        attack,
        Damage,

        end
    }
    enum ATK_STATUS
    {
        stay=0,
        attack,

        end
    }

    // こいつの状態
    STATUS status = STATUS.fly;
    ATK_STATUS aStatus = ATK_STATUS.stay;

    // 使用する予定のコンポーネント
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    // 攻撃対象
    GameObject player = null;

    // 連続で攻撃しないようにする
    bool isAttackOK = false;
    float AtkOKTime = 0;
    [SerializeField]
    float AtkOKTimeLimit = 2;

    // 攻撃の際に使用
    float AtkTime = 0;
    bool isAtkStart = false;
    [SerializeField]
    float AtkStayTimeLimit = 2, AtkFallTimeLimit = 2;
    bool AtkFlg = false;
    float AtkX = 0.0f;

    // 飛行の際に使用
    [SerializeField, Header("縦の揺れの幅(周波数)")]
    float T = 1.0f;
    float FlyTime = 0;

    // 攻撃の際に使用
    [SerializeField]
    float MoveSpeed = 5.0f;
    [SerializeField]
    Vector2 AtkMoveSpeed = new Vector2(5.0f, 5.0f);

    // false : 左    true : 右
    bool isLR = false;

    float StartY = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");

        StartY = this.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Action();

        if(isLR)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player && !isAttackOK)
        {
            status = STATUS.attack;
            isAttackOK = true;

            rb.velocity = Vector2.zero;
            animator.SetBool("Gatsuga", true);
        }
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

            case STATUS.Damage:
                {
                    Damage();
                }
                break;
        }
    }

    /* Actionの中身 */

    void Fly()
    {
        // 左右で移動が変わる
        if (isLR)
        {
            rb.velocity = new Vector2(MoveSpeed, 0.0f);
        }
        else
        {
            rb.velocity = new Vector2(-MoveSpeed, 0.0f);
        }

        FlyTime += Time.deltaTime;

        // 上下の揺れ運動
        float X = this.transform.position.x;

        float Y = Mathf.Sin(2 * Mathf.PI * T * FlyTime) + StartY;

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
                        aStatus = ATK_STATUS.attack;
                        AtkFlg = true;
                    }
                }
                break;

            // 攻撃中のジャンプ(攻撃の内容)
            case ATK_STATUS.attack:
                {
                    if(!isAtkStart)
                    {
                        isAtkStart = true;

                        AtkX = this.transform.position.x;
                    }

                    float time = AtkTime / AtkFallTimeLimit;
                    float X;
                    float Y = Mathf.Sin(2 * Mathf.PI * 0.5f * (time+1)) * AtkMoveSpeed.y + StartY;
                    
                    if (isLR)
                    {
                        X = -Mathf.Sin(2 * Mathf.PI * 0.5f * (time + 0.5f)) * AtkMoveSpeed.x + AtkX + AtkMoveSpeed.x;
                    }
                    else
                    {
                        X = Mathf.Sin(2 * Mathf.PI * 0.5f * (time + 0.5f)) * AtkMoveSpeed.x + AtkX - AtkMoveSpeed.x;
                    }

                    this.transform.position = new Vector3(X, Y, 0.0f);
                    this.transform.localEulerAngles = new Vector3(0, 0, (AtkTime / AtkFallTimeLimit * -180.0f + 90.0f));

                    if (AtkFallTimeLimit > AtkTime)
                    {
                        AtkTime += Time.deltaTime;
                    }
                    else
                    {
                        AtkTime = 0;
                        status = STATUS.fly;
                        aStatus = ATK_STATUS.stay;
                        animator.SetBool("Gatsuga", false);

                        this.transform.localEulerAngles = Vector3.zero;

                        FlyTime = 0.0f;
                    }
                }
                break;
        }

    }
    
    void Damage()
    {

    }

    /* Actionの中身 end */
}
