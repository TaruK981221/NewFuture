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
        Dash,
        ShockWave,

        end
    }
    
    STATUS status;
    ATKSTATUS atk_Status;


    Rigidbody2D rb;
    SpriteRenderer sr;


    float stayTime;
    [SerializeField]
    float stayTimeLimit;

    float walkTime;
    [SerializeField]
    float walkTimeLimit;

    float atkTime;
    [SerializeField]
    float atkTimeLimit;




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
        // sts = 1 : 突進
        if(sts == 1)
        {
            atk_Status = ATKSTATUS.Dash;
        }
        // sts = 2 : 火炎放射
        else if(sts == 2)
        {
            atk_Status = ATKSTATUS.Fire;
        }
    }
}
