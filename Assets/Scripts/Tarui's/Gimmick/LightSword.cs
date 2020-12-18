using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSword : MonoBehaviour
{
    enum STATUS
    {
        Rot=0,
        back,
        attack,

        end
    }

    STATUS sts= STATUS.Rot;

    SpriteRenderer sprite;
    
    GameObject player;

    [SerializeField,Header("回転速度")]
    float RotSpeed = 1.0f;

    [SerializeField, Header("回転回数")]
    int RotNum = 3;

    float   rotNow = 0.0f;
    bool    isRot = false;
    bool    isRotStart = false;

    float theta, theta2;
    float deg, deg2;

    [SerializeField, Header("速さ")]
    float speed = 5.0f;

    [SerializeField, Header("後退時間")]
    float BackTimeLimit = 0.5f;
    [SerializeField, Header("後退速度")]
    float BackSpeed = 3.0f;
    float BackTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Action();
    }

    void Action()
    {
        switch (sts)
        {
            case STATUS.Rot:
                Rot();
                break;
            case STATUS.back:
                Back();
                break;
            case STATUS.attack:
                Atk();
                break;
        }
    }

    void Rot()
    {
        if (!isRot)
        {
            transform.Rotate(0, 0, RotSpeed);

            rotNow += RotSpeed;

            Color color = sprite.color;

            color.a = rotNow / RotNum / 360.0f;

            sprite.color = color;

            if (rotNow / RotNum >= 360.0f)
            {
                transform.localEulerAngles = Vector3.zero;
                isRot = true;
                isRotStart = true;

                rotNow = 0.0f;
            }
        }
        else
        {
            if(isRotStart)
            {
                Vector2 xy;

                xy = player.transform.position - this.transform.position;

                theta = Mathf.Atan2(xy.y, xy.x);
                deg = theta * Mathf.Rad2Deg;
            }

            transform.Rotate(0, 0, RotSpeed);

            rotNow += RotSpeed;

            if(rotNow >= deg)
            {
                transform.localEulerAngles =
                    new Vector3(0, 0, deg);

                sts = STATUS.back;
            }
        }
    }

    void Back()
    {
        if (BackTime <= BackTimeLimit)
        {
            BackTime += Time.deltaTime;

            //this.transform.Translate(
            //    speed * Mathf.Cos(theta),
            //    speed * Mathf.Sin(theta),
            //    0);

            this.transform.position
                += new Vector3(-BackSpeed * Mathf.Cos(theta),
                               -BackSpeed * Mathf.Sin(theta),
                               0);
        }
        else
        {
            BackTime = 0.0f;

            sts = STATUS.attack;
        }
    }

    void Atk()
    {
        //this.transform.Translate(
        //    -speed * Mathf.Cos(theta),
        //    -speed * Mathf.Sin(theta),
        //    0);

        this.transform.position
            += new Vector3(speed * Mathf.Cos(theta),
                           speed * Mathf.Sin(theta),
                           0);
    }
}
