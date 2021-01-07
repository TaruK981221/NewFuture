using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Boss_Chimera : MonoBehaviour
{
    enum STATUS
    {
        stay = 0,
        walk,
        atk,

        end
    }

    public enum ATKSTATUS
    {
        Fire = 0,
        Jump,
        Dash,

        end
    }

    [SerializeField]
    STATUS status;
    [SerializeField]
    ATKSTATUS atk_Status;


    Rigidbody2D rb;
    SpriteRenderer sr;


    enemy_GroundCollision gCol;
    enemy_WallCollision[] wCol;
    bool isGround = false;

    GameObject player;

    float stayTime = 0;
    [SerializeField]
    float stayTimeLimit = 2;

    float walkTime = 0;
    [SerializeField]
    float walkTimeLimit = 2;

    float atkOKTime = 0;
    [SerializeField]
    float atkOKTimeLimit = 2;

    bool isAtkOK = true;

    // 強攻撃用
    float atkS_Time = 0;
    [SerializeField]
    float atkS_TimeLimit = 30;

    [SerializeField]
    float speed = 10.0f;

    bool isAtk = false;
    public bool IsAtk
    {
        get
        {
            return isAtk;
        }
    }


    // false : 左    true : 右
    bool isLR = false;
    public bool IsLR
    {
        get
        {
            return isLR;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        status = STATUS.stay;
        atk_Status = ATKSTATUS.end;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");

        gCol = this.transform.GetComponentInChildren<enemy_GroundCollision>();
        wCol = this.transform.GetComponentsInChildren<enemy_WallCollision>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Action();

        GroundCol();
    }

    void GroundCol()
    {
        if (gCol.IsGround)
        {
            if (!isGround)
            {
                isGround = true;

                UninitAtk();
            }
        }
        else
        {
            isGround = false;
        }
    }

    void Action()
    {
        switch (status)
        {
            case STATUS.stay:
                Stay();
                break;
            case STATUS.walk:
                Walk();
                break;
            case STATUS.atk:
                Attack();
                break;
        }

        // 次に攻撃ができるまでのクールタイム
        AtkOK();

        // 左右の向き変更
        sr.flipX = !isLR;

        // 一定時間経過後に強攻撃
        atkS_Time += Time.deltaTime;
        if (atkS_Time >= atkS_TimeLimit &&
            (status == STATUS.stay || status == STATUS.walk))
        {
            status = STATUS.atk;
            atk_Status = ATKSTATUS.Dash;
            if (status == STATUS.stay)
            {
                stayTime = 0;
            }
            else
            {
                walkTime = 0;
            }
        }
    }

    void Stay()
    {
        if (stayTime < stayTimeLimit)
        {
            stayTime += Time.deltaTime;
        }
        else
        {
            stayTime = 0;
            status = STATUS.walk;
        }
    }

    void Walk()
    {
        // 移動の直前に主人公の方向を向く
        if (walkTime == 0)
        {
            if (player.transform.position.x <= this.transform.position.x)
            {
                isLR = false;
            }
            else
            {
                isLR = true;
            }
        }

        if (isLR && !wCol[1].IsWall)
        {
            this.transform.position +=
                new Vector3(speed, 0.0f)/* * this.transform.lossyScale.x*/;
        }
        else if (!isLR && !wCol[0].IsWall)
        {
            this.transform.position +=
                new Vector3(-speed, 0.0f)/* * this.transform.lossyScale.x*/;
        }

        if (walkTime < walkTimeLimit)
        {
            walkTime += Time.deltaTime;
        }
        else
        {
            walkTime = 0;
            status = STATUS.stay;
        }
    }

    void Attack()
    {
        switch (atk_Status)
        {
            case ATKSTATUS.Fire:
                ATK_Fire();
                break;
            case ATKSTATUS.Jump:
                ATK_Jump();
                break;
            case ATKSTATUS.Dash:
                ATK_Dash();
                break;
        }


        //if (atkTime < atkTimeLimit)
        //{
        //    if (atkTime == 0)
        //    {
        //        stayTime = 0;
        //        walkTime = 0;
        //    }

        //    atkTime += Time.deltaTime;
        //}
        //else
        //{
        //    atkTime = 0;
        //    status = STATUS.stay;
        //}
    }

    void AtkOK()
    {
        if (!isAtkOK)
        {
            if (atkOKTime <= atkOKTimeLimit)
            {
                atkOKTime += Time.deltaTime;
            }
            else
            {
                isAtkOK = true;
                atkOKTime = 0;
            }
        }
    }
}
