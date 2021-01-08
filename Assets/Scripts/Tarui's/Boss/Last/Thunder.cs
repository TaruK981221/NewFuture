using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    enum Status
    {
        Circle = 0,
        Light,
        Thunder,
        Destroy,

        end
    }

    Status sts = Status.Circle;

    GameObject c_Magic;
    GameObject c_Thunder;
    GameObject c_Light;

    Animator anim;
    SpriteRenderer sr_light;

    [SerializeField]
    float MagicCircleTimeLimit = 1;
    [SerializeField]
    float LightTimeLimit = 0.5f;
    [SerializeField]
    float DestroyTimeLimit = 1;
    [SerializeField,Header("回転する角度")]
    float RotSpeed = 120.0f;

    float actionTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        c_Magic = transform.GetChild(0).gameObject;
        c_Thunder = transform.GetChild(1).gameObject;
        c_Light = transform.GetChild(2).gameObject;
    }

    private void Start()
    {
        sr_light = c_Light.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (sts)
        {
            case Status.Circle:
                Circle();
                break;
            case Status.Light:
                Light();
                break;
            case Status.Thunder:
                Atk();
                break;
            case Status.Destroy:
                Des();
                break;
        }
    }

    void Circle()
    {
        if(actionTime < MagicCircleTimeLimit)
        {
            actionTime += Time.deltaTime;

            float scale = actionTime / MagicCircleTimeLimit;
            float theta = scale * RotSpeed + 360.0f - RotSpeed;

            c_Magic.transform.localScale =
                new Vector3(scale, scale, 1);

            c_Magic.transform.localEulerAngles =
                new Vector3(75.0f, 0, theta);
        }
        else
        {
            actionTime = 0;
            c_Magic.transform.localScale = Vector3.one;
            c_Magic.transform.localEulerAngles =
                new Vector3(75.0f, 0, 0);

            sts = Status.Light;
        }
    }

    void Light()
    {
        if(actionTime < LightTimeLimit)
        {
            actionTime += Time.deltaTime;

            float a = actionTime / LightTimeLimit;
            Color color = sr_light.color;
            color.a = a;
            sr_light.color = color;
        }
        else
        {
            actionTime = 0;
            Color color = sr_light.color;
            color.a = 1;
            sr_light.color = color;

            sts = Status.Thunder;

            c_Thunder.SetActive(true);
            anim = this.GetComponentInChildren<Animator>();
        }
    }

    void Atk()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
        {
            float time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float a = 1.0f - (time/* / 1.0f*/ - 0.9f) * 10.0f;
            Color color = sr_light.color;
            color.a = a;
            sr_light.color = color;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Destroy(c_Thunder);
            Destroy(c_Light);

            sts = Status.Destroy;
        }
    }

    private void Des()
    {
        if(actionTime < DestroyTimeLimit)
        {
            actionTime += Time.deltaTime;

            float scale = actionTime / MagicCircleTimeLimit;
            float theta = -scale * RotSpeed + 360.0f;

            c_Magic.transform.localScale =
                new Vector3(1-scale, 1-scale, 1);

            c_Magic.transform.localEulerAngles =
                new Vector3(75.0f, 0, theta);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
