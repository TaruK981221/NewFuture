using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour
{
    // 移行先のシーン名保存用
    [SerializeField] private string TransScene = "stage1_test";   // 移行先のシーン名入力

    // タイトルアニメーション用
    [SerializeField] private float PosY = 300.0f;       // タイトル名移動後の位置
    [SerializeField] private float Speed = 1.0f;        // タイトル名移動速度
    //[SerializeField] private float BlinkInterval = 1.0f;     // テキスト点滅速度
    private bool Ready = false;

    // 各オブジェクトへの参照保存用
    Transform TitleImage;   // タイトル画像
    Text TextObj;           // テキスト


    // Start is called before the first frame update
    void Start()
    {
        TitleImage = GameObject.Find("Title").transform;
        TextObj = GameObject.Find("Text").GetComponent<Text>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Ready)
        {
            // テキストの透明度変更
            //TextObj.color = new Vector4(1.0f, 1.0f, 1.0f, TextObj.color.a + (1 / 60) / BlinkInterval);
            //if (TextObj.color.a < 0.0f ||TextObj.color.a >= 1.0f) {
            //    BlinkInterval = -BlinkInterval;
            //}

            // いずれかのキーが押されたらシーン移動
            if (Input.anyKeyDown) {
                SceneManager.LoadScene(TransScene);     // シーン移行処理
            }
        }

        // タイトル名等移動処理
        if (!(Ready))
        {
            PhaseOne();
        }
    }


    //-------- タイトル名表示, 移動 / テキスト表示 --------*/
    void PhaseOne()
    {
        Vector3 pos = TitleImage.localPosition;

        TitleImage.localPosition = new Vector3(pos.x, pos.y + Speed, pos.z);

        // タイトル画像が規定値以上もしくはいずれかのキーが押されたら実行
        if(TitleImage.localPosition.y >= PosY || (Input.anyKeyDown))
        {
            // タイトル画像位置調整, テキストの透明度を無くす, 準備フラグON
            TitleImage.localPosition = new Vector3(pos.x, PosY, pos.z);
            TextObj.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            Ready = true;
        }
    }


    /*-------- チーム名表記等 --------*/
    //void PhaseZero(){}
}
