using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Boss_Chimera : MonoBehaviour
{
    enum JUMP
    {
        stay=0,
        jump,
        fall,

        end
    }
    enum FIRE
    {
        stay=0,
        fire,

        end
    }
    enum DASH
    {
        stay=0,
        jump,
        fall,
        dash,
        return_fall,

        end
    }

    [SerializeField]
    JUMP jump = JUMP.stay;
    [SerializeField]
    FIRE fire = FIRE.stay;
    [SerializeField]
    DASH dash = DASH.stay;

    [SerializeField, Header("ジャンプ攻撃 時間or速さで管理")]
    float JStayTimeLimit = 2.0f;
    [SerializeField]
    float JJumpTimeLimit = 2.0f;
    [SerializeField]
    float JFallSpeed = 1.0f;
    [SerializeField]
    Vector2 JumpSpeed = new Vector2(2.0f, 5.0f);

    float JTime = 0;
    float JStartY = 0;
    float FStartY = 0;

    [SerializeField, Header("火炎放射")]
    float FStayTimeLimit = 2.0f;
    [SerializeField]
    float FFireTimeLimit = 2.0f;

    float FTime = 0;

    [SerializeField, Header("突進攻撃")]
    float DStayTimeLimit = 2.0f;
    [SerializeField]
    float DJumpTimeLimit = 3.0f;
    [SerializeField]
    float DFallTimeLimit = 3.0f;
    [SerializeField]
    float DDashTimeLimit = 5.0f;
    [SerializeField]
    float DReFallTimeLimit = 5.0f;

    float DTime = 0;

    bool isDash = false;

    void UninitAtk()
    {
        switch (atk_Status)
        {
            case ATKSTATUS.Fire:
                {
                    fire = FIRE.stay;
                    FTime = 0.0f;
                }
                break;
            case ATKSTATUS.Jump:
                {
                    jump = JUMP.stay;
                    JTime = 0.0f;
                }
                break;
            case ATKSTATUS.Dash:
                {
                    dash = DASH.stay;
                    DTime = 0.0f;
                }
                break;
        }

        atk_Status = ATKSTATUS.end;

        status = STATUS.stay;

        isAtkOK = false;
        isAtk = false;
    }

    void ATK_Jump()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;

        switch (jump)
        {
            case JUMP.stay:
                JumpStay();
                break;
            case JUMP.jump:
                JumpJump();
                break;
            case JUMP.fall:
                JumpFall();
                break;
        }
    }

    void JumpStay()
    {
        if(JTime < JStayTimeLimit)
        {
            JTime += Time.deltaTime;
        }
        else
        {
            JTime = 0;

            jump = JUMP.jump;
            JStartY = this.transform.position.y;
        }
    }

    void JumpJump()
    {
        float time = JTime / JJumpTimeLimit;

        float X;

        if(isLR)
        {
            X = JumpSpeed.x/* * this.transform.lossyScale.x*/ + transform.position.x;
        }
        else
        {
            X = -JumpSpeed.x/* * this.transform.lossyScale.x*/ + transform.position.x;
        }

        float Y = Mathf.Sin(2 * Mathf.PI * 0.25f * time) *
            JumpSpeed.y * this.transform.lossyScale.x + JStartY;

        this.transform.position =
            new Vector3(X, Y, 0.0f);

        if (JTime < JJumpTimeLimit)
        {
            JTime += Time.deltaTime;
        }
        else
        {
            JTime = 0;
            FStartY = this.transform.position.y;

            jump = JUMP.fall;
        }
    }

    void JumpFall()
    {
        //float time = JTime / JFallTimeLimit;

        Vector3 pos = Vector3.zero;
        pos.y = JFallSpeed/* * this.transform.localScale.x*/;

        if (isLR && !wCol[1].IsWall)
        {
            pos.x = -JFallSpeed/* * this.transform.localScale.x*/;
        }
        else if (!isLR && !wCol[0].IsWall)
        {
            pos.x = JFallSpeed/* * this.transform.localScale.x*/;
        }

        this.transform.position -= pos;

        //if (JTime < JFallTimeLimit)
        //{
        //    JTime += Time.deltaTime;
        //}
        //else
        //{
        //    JTime = 0;

        //    jump = JUMP.stay;
        //    status = STATUS.stay;
        //}
    }

    void ATK_Fire()
    {
        switch (fire)
        {
            case FIRE.stay:
                FireStay();
                break;
            case FIRE.fire:
                FireFire();
                break;
        }
    }

    void FireStay()
    {
        if (FTime < FStayTimeLimit)
        {
            FTime += Time.deltaTime;
        }
        else
        {

        }
    }

    void FireFire()
    {
        if (FTime < FFireTimeLimit)
        {
            FTime += Time.deltaTime;
        }
        else
        {

        }
    }

    void ATK_Dash()
    {
        switch (dash)
        {
            case DASH.stay:
                DashStay();
                break;
            case DASH.jump:
                DashJump();
                break;
            case DASH.fall:
                DashFall();
                break;
            case DASH.dash:
                DashDash();
                break;
            case DASH.return_fall:
                DashReturn();
                break;
        }
    }

    void DashStay()
    {
        if (DTime < DStayTimeLimit)
        {
            DTime += Time.deltaTime;
        }
        else
        {

        }
    }

    void DashJump()
    {
        if (DTime < DJumpTimeLimit)
        {
            DTime += Time.deltaTime;
        }
        else
        {

        }
    }

    void DashFall()
    {
        if (DTime < DFallTimeLimit)
        {
            DTime += Time.deltaTime;
        }
        else
        {

        }
    }

    void DashDash()
    {
        if (DTime < DDashTimeLimit)
        {
            DTime += Time.deltaTime;
        }
        else
        {

        }
    }

    void DashReturn()
    {
        if (DTime < DReFallTimeLimit)
        {
            DTime += Time.deltaTime;
        }
        else
        {

        }
    }

    public void AtkCol(int sts)
    {
        if (isAtkOK)
        {
            if (status == STATUS.walk)
            {
                walkTime = 0;
            }
            else
            {
                stayTime = 0;
            }

            status = STATUS.atk;
            // sts = 1 : とびかかり
            if (sts == 1)
            {
                atk_Status = ATKSTATUS.Jump;
            }
            // sts = 2 : 火炎放射
            else if (sts == 2)
            {
                atk_Status = ATKSTATUS.Fire;
            }

            isAtk = true;
        }
    }
}
