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

    JUMP jump = JUMP.stay;
    FIRE fire = FIRE.stay;
    DASH dash = DASH.stay;

    void ATK_Jump()
    {
        switch (jump)
        {
            case JUMP.stay:
                break;
            case JUMP.jump:
                break;
            case JUMP.fall:
                break;
        }
    }

    void ATK_Fire()
    {
        switch (fire)
        {
            case FIRE.stay:
                break;
            case FIRE.fire:
                break;
        }
    }

    void ATK_Dash()
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
            atk_Status = ATKSTATUS.end;
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
        }
    }
}
