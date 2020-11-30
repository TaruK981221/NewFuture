using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_gazer : MonoBehaviour
{
    enum STATUS
    {
        stay = 0,
        attack,
        rotate,
        Damage,

        end
    }
    enum ATK_STATUS
    {
        stay = 0,
        Atk,

        end
    }
    enum Rotate_STATUS
    {
        left=0,
        center,
        right,

        end
    }
    
    // こいつの状態
    [SerializeField]
    STATUS status = STATUS.stay;
    ATK_STATUS aStatus = ATK_STATUS.stay;
    Rotate_STATUS rStatus = Rotate_STATUS.left;

    // 攻撃対象
    GameObject player = null;

    // 当たり判定
    [SerializeField,Header("当たり判定")]
    GameObject childCol = null;
    GameObject child = null;

    [SerializeField]
    GameObject beam;

    // 使用する予定のコンポーネント
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;

    float AnimSpeed = 0.0f;

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

    // 歩行の際に使用
    float WalkTime = 0;
    [SerializeField]
    float WalkTimeLimit = 2;

    // 待機の際に使用
    float StayTime = 0;
    [SerializeField]
    float StayTimeLimit = 2;

    [SerializeField]
    Vector3 LeftCol  = new Vector3(-4.6f, -8.65f),
        CenterCol    = new Vector3(0.25f, -11.0f),
        RightCol     = new Vector3(5.2f, -8.75f);

    // false : 左    true : 右
    bool isLR = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb = this.gameObject.GetComponent<Rigidbody2D>();

        sprite = this.GetComponent<SpriteRenderer>();

        animator = this.GetComponent<Animator>();
        AnimSpeed = animator.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 行動の処理
        Action();
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

            //animator.SetBool("Atk", true);
        }
    }

    void Action()
    {
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

            case STATUS.rotate:
                {
                    Rotate();
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
        if(StayTime == 0)
        {
            isAttackOK = true;

            switch (rStatus)
            {
                case Rotate_STATUS.left:
                    {
                        animator.SetTrigger("Left");

                        child =
                            Instantiate(
                                childCol,
                                LeftCol * this.transform.localScale.x +
                                new Vector3(LeftCol.x * 2.5f, LeftCol.y * 1.4f) +
                                this.transform.position,
                                Quaternion.Euler(0, 0, -45.0f),
                                this.transform);
                    }
                    break;
                case Rotate_STATUS.center:
                    {
                        animator.SetTrigger("Center");

                        child =
                            Instantiate(
                                childCol,
                                CenterCol * this.transform.localScale.x +
                                new Vector3(LeftCol.x * 2.5f, LeftCol.y * 1.4f) +
                                this.transform.position,
                                Quaternion.Euler(0, 0, 0),
                                this.transform);
                    }
                    break;
                case Rotate_STATUS.right:
                    {
                        animator.SetTrigger("Right");

                        child =
                            Instantiate(
                                childCol,
                                RightCol * this.transform.localScale.x +
                                new Vector3(LeftCol.x * 2.5f, LeftCol.y * 1.4f) +
                                this.transform.position,
                                Quaternion.Euler(0, 0, 45.0f),
                                this.transform);
                    }
                    break;
                default:
                    break;
            }
        }

        if(animator)

        if (StayTimeLimit > StayTime)
        {
            StayTime += Time.deltaTime;
        }
        else
        {
            StayTime = 0;
            status = STATUS.rotate;

            animator.SetTrigger("Close");

            Destroy(child);

            isAttackOK = false;
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
                        aStatus = ATK_STATUS.Atk;
                        AtkFlg = true;

                        //animator.enabled = true;
                        //animator.speed = AnimSpeed;
                        //animator.SetBool("Jump", true);
                    }
                }
                break;

            // 攻撃中のジャンプ(攻撃の内容)
            case ATK_STATUS.Atk:
                {
                    if(AtkTime == 0)
                    {
                        switch (rStatus)
                        {
                            case Rotate_STATUS.left:
                                {
                                    Instantiate(beam, LeftCol, Quaternion.identity, this.transform);
                                }
                                break;
                            case Rotate_STATUS.center:
                                {
                                    Instantiate(beam, CenterCol, Quaternion.identity, this.transform);
                                }
                                break;
                            case Rotate_STATUS.right:
                                {
                                    Instantiate(beam, RightCol, Quaternion.identity, this.transform);
                                }
                                break;
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

                        //animator.SetBool("Jump", false);
                        //animator.speed = 0;
                        //animator.enabled = false;
                        //sprite.sprite = stay;
                    }
                }
                break;
        }

    }

    void Rotate()
    {
        if (WalkTime == 0)
        {
            isAttackOK = false;

            switch (rStatus)
            {
                case Rotate_STATUS.left:
                    {
                        rStatus = Rotate_STATUS.center;
                    }
                    break;
                case Rotate_STATUS.center:
                    {
                        if (isLR)
                        {
                            rStatus = Rotate_STATUS.right;
                        }
                        else
                        {
                            rStatus = Rotate_STATUS.left;
                        }
                        isLR = !isLR;
                    }
                    break;
                case Rotate_STATUS.right:
                    {
                        rStatus = Rotate_STATUS.center;
                    }
                    break;
                default:
                    break;
            }
        }

        // 歩行する時間の管理
        if (WalkTimeLimit > WalkTime)
        {
            WalkTime += Time.deltaTime;
        }
        else
        {
            WalkTime = 0.0f;
            status = STATUS.stay;
        }
    }

    void Damage()
    {

    }

    /* Actionの中身 end */
}
