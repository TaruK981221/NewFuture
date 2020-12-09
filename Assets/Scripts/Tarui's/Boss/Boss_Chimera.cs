using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Chimera : MonoBehaviour
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
        stay = 0,
        Fire,
        Jump,
        Dash,
        ShockWave,

        end
    }
    
    STATUS status;
    ATKSTATUS atk_Status;


    Rigidbody2D rb;
    SpriteRenderer sr;


    float stayTime = 0;
    [SerializeField]
    float stayTimeLimit=2;

    float walkTime=0;
    [SerializeField]
    float walkTimeLimit=2;

    float atkTime=0;
    [SerializeField]
    float atkTimeLimit=2;

    // 強攻撃用
    float atkS_Time = 0;
    [SerializeField]
    float atkS_TimeLimit = 30;

    bool isAtk;
    public bool IsAtk
    {
        get
        {
            return isAtk;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        status = STATUS.stay;
        atk_Status = ATKSTATUS.stay;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
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

        // 一定時間経過後に強攻撃
        atkS_Time += Time.deltaTime;
        if(atkS_Time >= atkS_TimeLimit &&
            (status == STATUS.stay || status == STATUS.walk))
        {
            status = STATUS.atk;
            atk_Status = ATKSTATUS.Dash;
            if(status == STATUS.stay)
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
        if (atkTime < atkTimeLimit)
        {
            if (atkTime == 0)
            {
                stayTime = 0;
                walkTime = 0;
            }

            atkTime += Time.deltaTime;
        }
        else
        {
            atkTime = 0;
            status = STATUS.stay;
        }
    }

    public void AtkCol(int sts)
    {
        // sts = 1 : とびかかり
        if(sts == 1)
        {
            atk_Status = ATKSTATUS.Jump;
        }
        // sts = 2 : 火炎放射
        else if(sts == 2)
        {
            atk_Status = ATKSTATUS.Fire;
        }
    }
}
