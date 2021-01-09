using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Boss_Last_Main : MonoBehaviour
{
    [SerializeField]
    GameObject A_Sword,A_Light,A_Thunder;

    [SerializeField, Header("攻撃中のスキの時間")]
    float SwordTimeLimit = 5.0f;
    [SerializeField]
    float LightTimeLimit = 5.0f;
    [SerializeField]
    float ThunderTimeLimit = 5.0f;

    int SwordNum = 3;
    int ThunderNum = 10;

    int actionNum = 0;

    [SerializeField, Header("攻撃用のスプライト")]
    Sprite s_Stay, s_Sword, s_Light, s_Thunder;

    void Sword()
    {
        if (actionTime == 0)
        {
            c_Sprite_sr.sprite = s_Sword;

            Instantiate(A_Sword,
                c_Sprite.transform.position +
                new Vector3(0, c_Sprite.transform.lossyScale.y * 6),
                Quaternion.identity);

            Instantiate(A_Sword,
                c_Sprite.transform.position +
                new Vector3(0, c_Sprite.transform.lossyScale.y * 3f),
                Quaternion.identity);

            if (c_Sprite_sr.flipX)
            {
                Instantiate(A_Sword,
                    c_Sprite.transform.position +
                    new Vector3(c_Sprite.transform.lossyScale.x * 2, c_Sprite.transform.lossyScale.y * 2.0f),
                    Quaternion.identity);

                Instantiate(A_Sword,
                    c_Sprite.transform.position +
                    new Vector3(c_Sprite.transform.lossyScale.y * 2, c_Sprite.transform.lossyScale.y * -2.0f),
                    Quaternion.identity);
            }
            else
            {
                Instantiate(A_Sword,
                    c_Sprite.transform.position +
                    new Vector3(c_Sprite.transform.lossyScale.x * -2, c_Sprite.transform.lossyScale.y * 2.0f),
                    Quaternion.identity);

                Instantiate(A_Sword,
                    c_Sprite.transform.position +
                    new Vector3(c_Sprite.transform.lossyScale.y * -2, c_Sprite.transform.lossyScale.y * -2.0f),
                    Quaternion.identity);
            }
        }

        if(actionTime < SwordTimeLimit)
        {
            actionTime += Time.deltaTime;
        }
        else
        {
            atkSts = ATK_STATUS.end;

            sts = STATUS.Stay;

            actionTime = 0.0f;
            c_Sprite_sr.sprite = s_Stay;
        }
    }

    void Light()
    {
        if (actionTime == 0)
        {
            c_Sprite_sr.sprite = s_Light;

            Instantiate(A_Light,
                c_Sprite.transform.position +
                new Vector3(0, c_Sprite.transform.lossyScale.y * 5),
                Quaternion.identity);
        }

        if (actionTime < SwordTimeLimit)
        {
            actionTime += Time.deltaTime;
        }
        else
        {
            atkSts = ATK_STATUS.end;

            sts = STATUS.Stay;

            actionTime = 0.0f;
            c_Sprite_sr.sprite = s_Stay;
        }
    }

    void Thunder()
    {
        if (actionTime == 0)
        {
            c_Sprite_sr.sprite = s_Thunder;
        }

        if (actionTime < SwordTimeLimit)
        {
            if ((float)actionNum / (float)ThunderNum * ThunderTimeLimit < actionTime)
            {
                actionNum += 1;

                Instantiate(A_Thunder,
                    new Vector3(7f * c_Sprite.transform.lossyScale.x * (float)actionNum -
                      7f * c_Sprite.transform.lossyScale.x * (float)ThunderNum / 2.0f +
                      c_Sprite.transform.position.x,
                      c_ThunderY.position.y),
                    Quaternion.identity);
            }

                actionTime += Time.deltaTime;
        }
        else
        {
            atkSts = ATK_STATUS.end;

            sts = STATUS.Stay;

            actionTime = 0.0f;
            actionNum = 0;
            c_Sprite_sr.sprite = s_Stay;
        }
    }
}
