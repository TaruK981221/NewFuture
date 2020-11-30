using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_ghost : MonoBehaviour
{
    enum STATUS
    {
        stay = 0,
        attack,
        walk,
        transparency,
        Damage,

        end
    }
    enum ATK_STATUS
    {
        stay = 0,
        attack,

        end
    }

    // こいつの状態
    STATUS status = STATUS.stay;
    ATK_STATUS aStatus = ATK_STATUS.stay;

    // 攻撃対象
    GameObject player = null;

    enemy_GroundCollision col = null;

    // 使用する予定のコンポーネント
    Rigidbody2D rb;
    SpriteRenderer sprite;
    //Animator animator;

    //float AnimSpeed = 0.0f;

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
    bool isWalk = false;

    // 待機の際に使用
    float StayTime = 0;
    [SerializeField]
    float StayTimeLimit = 2;

    // 待機の際に使用
    float TransparencyTime = 0;
    [SerializeField]
    float TransparencyTimeLimit = 1;
    [SerializeField, Header("透明化するときの透明度"), Range(0.01f, 1.0f)]
    float TransparencyPara = 0.4f;
    bool isTransparency = false;

    [SerializeField]
    float speed = 10.0f, jumpSpeed = 10.0f;

    //[SerializeField]
    //Sprite stay = null, landing = null;

    // false : 左    true : 右
    bool isLR = false;

    Vector3 startPos = Vector3.zero;
    Transform child;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponentInChildren<SpriteRenderer>();
        col = this.GetComponent<enemy_GroundCollision>();

        child = this.transform.GetChild(1);
        startPos = child.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Action();

        fuwafuwa();

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
        
    }

    void Action()
    {
        switch (status)
        {
            case STATUS.stay:
                {
                    Stay();
                }
                break;
            case STATUS.attack:
                {
                    Attack();
                }
                break;
            case STATUS.walk:
                {
                    Walk();
                }
                break;
            case STATUS.transparency:
                {
                    Transparency();
                }
                break;
            case STATUS.Damage:
                {
                    Damage();
                }
                break;
        }
    }

    void Stay()
    {
        if (StayTimeLimit > StayTime)
        {
            StayTime += Time.deltaTime;
        }
        else
        {
            StayTime = 0;

            isWalk = !isWalk;

            if (isWalk)
            {
                status = STATUS.walk;
            }
            else
            {
                status = STATUS.transparency;
            }

        }
        
        // 左右の方向を決める
        if (player.transform.position.x >= this.transform.position.x)
        {
            isLR = false;
        }
        else
        {
            isLR = true;
        }
    }

    void Attack()
    {

    }

    void Walk()
    {
        // 左右で移動が変わる
        if (isLR)
        {
            rb.velocity = new Vector2(-speed, 0.0f);
        }
        else
        {
            rb.velocity = new Vector2(speed, 0.0f);
        }
        // 壁にめり込んでいたら進まない
        if (col.IsWall)
        {
            rb.velocity = Vector2.zero;
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

    void Transparency()
    {
        if (TransparencyTimeLimit > TransparencyTime)
        {
            TransparencyTime += Time.deltaTime;
        }
        else
        {
            TransparencyTime = 0;
            status = STATUS.stay;
            isTransparency = !isTransparency;
        }

        // 透明度を変える
        if(isTransparency)
        {
            // 透明化　解除
            float time = TransparencyTime / TransparencyTimeLimit;
            Color color = sprite.color;
            color.a = Mathf.Lerp(TransparencyPara, 1.0f, time);
            sprite.color = color;
        }
        else
        {
            // 透明化
            float time = TransparencyTime / TransparencyTimeLimit;
            Color color = sprite.color;
            color.a = Mathf.Lerp(1.0f, TransparencyPara, time);
            sprite.color = color;
        }
    }

    void Damage()
    {

    }

    void fuwafuwa()
    {
        child.localPosition =
            new Vector3(
                startPos.x,
                Mathf.Sin(2 * Mathf.PI * 0.5f * Time.time),
                startPos.z);
    }
}
