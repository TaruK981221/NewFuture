using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpManager : MonoBehaviour
{
    // HP描画時の色味
    [SerializeField] private Vector4 OnColor = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
    [SerializeField] private Vector4 OffColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);

    // HPを参照するオブジェクト名
    [SerializeField] private string HpObjectName = "PlayerObj";
    // HP保有オブジェクト参照用
    private GameObject SetHpObject;
    // 子オブジェクト格納用
    private List<GameObject> ChildList = new List<GameObject>();

    private int MaxHP;
    private int KeepHP = 0;


    // Start is called before the first frame update
    void Start()
    {
        SetHpObject = GameObject.Find(HpObjectName);                        // HP(UI)をいじりたいオブジェクトへの参照保存
        MaxHP = SetHpObject.GetComponent<TemporaryPlayerStatus>().MaxHP;    // キャラクターの最大HPが記載されたスクリプトの変数参照

        AddHP();
    }


    // Update is called once per frame
    void Update()
    {
        // 体力値が変わっていれば実行(キャラクターの現在HPが記載されたスクリプトの変数参照)
        if (KeepHP != SetHpObject.GetComponent<TemporaryPlayerStatus>().HP)
        {
            KeepHP = SetHpObject.GetComponent<TemporaryPlayerStatus>().HP;
            UpdateColor();      // 色変更処理
        }
    }


    /*-------- 最大体力値分子オブジェクトを生成 --------*/
    void AddHP()
    {
        // 子オブジェクトの横幅計算
        float Width = gameObject.GetComponent<RectTransform>().sizeDelta.x / MaxHP;
        // 最左位置を計算
        float Left = -(gameObject.GetComponent<RectTransform>().sizeDelta.x / 2) + Width / 2;

        // hps生成処理
        for(int i = 0; i < MaxHP; i++)
        {
            GameObject hps = (GameObject)Resources.Load("hps");

            // hpsプレハブを元にインスタンスを生成
            Instantiate(hps, new Vector3(Left, 0.0f, 0.0f), Quaternion.identity);
            Left += Width;      // 生成位置をずらす
        }

        // 横幅変更, リストに子オブジェクトを格納
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, child.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            ChildList.Add(child.gameObject);
        }
    }


    /*-------- 体力値に応じて色味を変更する --------*/
    void UpdateColor()
    {
        // 最大体力値分ループ
        for (int i = 0; i < MaxHP; i++) {
            // 現在体力値分ループし、色をOnColorに変更
            for(; i < KeepHP; i++)
            {
                ChildList[i].GetComponent<Image>().color = OnColor;
            }

            // 色をOffColorに変更
            ChildList[i].GetComponent<Image>().color = OffColor;
        }
    }
}
