using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Boss_Last_Main : MonoBehaviour
{
    enum STATUS
    {
        Stay = 0,
        Warp,
        Atk,

        end
    }
    enum ATK_STATUS
    {
        //FullSpeedWarp = 0,
        Sword = 0,
        Light,
        Thunder,

        end
    }

    STATUS sts = STATUS.Stay;
    ATK_STATUS atkSts = ATK_STATUS.end;

    GameObject c_Sprite;
    SpriteRenderer c_Sprite_sr;
    GameObject c_Warp;
    BoxCollider2D c_WarpCol;
    Transform c_ThunderY;

    GameObject Player;

    [SerializeField]
    float StayTimeLimit = 2.0f;
    [SerializeField]
    float WarpTimeLimit = 0.1f;


    int WarpCount = 0;
    [SerializeField]
    int WarpCountLimit = 3;

    float actionTime = 0.0f;

    bool isFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        c_Sprite     = this.transform.GetChild(0).gameObject;
        c_Sprite_sr  = this.GetComponentInChildren<SpriteRenderer>();
        c_Warp       = this.transform.GetChild(1).gameObject;
        c_WarpCol    = c_Warp.GetComponent<BoxCollider2D>();

        c_ThunderY   = this.transform.GetChild(2);

        Player = GameObject.FindGameObjectWithTag("Player");

        if (c_Sprite.name != "Sprite")
        {
            GameObject obj;
            obj = c_Sprite;
            c_Sprite = c_Warp;
            c_Warp = obj;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFlg)
        {
            Action();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Player == collision.gameObject)
        {
            isFlg = true;
        }
    }

    void Action()
    {
        switch (sts)
        {
            case STATUS.Stay:
                Stay();
                break;
            case STATUS.Warp:
                Warp();
                break;
            case STATUS.Atk:
                Atk();
                break;
        }
    }

    void Stay()
    {
        if(actionTime < StayTimeLimit)
        {
            actionTime += Time.deltaTime;
        }
        else
        {
            actionTime = 0.0f;

            sts = STATUS.Warp;
        }
    }

    void Warp()
    {
        Vector2 pos = Vector2.zero;
        
        pos.x = Random.Range(c_WarpCol.bounds.min.x, c_WarpCol.bounds.max.x);
        pos.y = Random.Range(c_WarpCol.bounds.min.y, c_WarpCol.bounds.max.y);

        c_Sprite.transform.position = pos;
        if(Player.transform.position.x >= pos.x)
        {
            c_Sprite_sr.flipX = false;
        }
        else
        {
            c_Sprite_sr.flipX = true;
        }

        c_Sprite.SetActive(false);

        if(actionTime < WarpTimeLimit)
        {
            actionTime += Time.deltaTime;
        }
        else
        {
            actionTime = 0.0f;
            c_Sprite.SetActive(true);

            WarpCount += 1;

            if(WarpCountLimit == WarpCount)
            {
                WarpCount = 0;

                sts = STATUS.Atk;

                float ran = Random.Range(0, 600.0f);

                if(ran <= 200.0f)
                {
                    atkSts = ATK_STATUS.Sword;
                }
                else if(ran <= 400.0f)
                {
                    atkSts = ATK_STATUS.Light;
                }
                else
                {
                    atkSts = ATK_STATUS.Thunder;
                }
            }
            else
            {
                sts = STATUS.Stay;
            }
        }
    }

    void Atk()
    {
        switch (atkSts)
        {
            //case ATK_STATUS.FullSpeedWarp:
            //    break;
            case ATK_STATUS.Sword:
                Sword();
                break;
            case ATK_STATUS.Light:
                Light();
                break;
            case ATK_STATUS.Thunder:
                Thunder();
                break;
        }
    }
}
