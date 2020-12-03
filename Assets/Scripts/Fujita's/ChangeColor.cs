using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    // プレイヤー体力値(仮)
    [SerializeField] private int MaxHP = 5;
    [SerializeField] private int PlayerHP = 3;

    // 子オブジェクト格納用
    private GameObject[] ChildObjects;

    // カラー値
    [SerializeField] private Vector4 OnColor = new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
    [SerializeField] private Vector4 OffColor = new Vector4(255.0f, 0.0f, 0.0f, 255.0f);


    // Start is called before the first frame update
    void Start()
    {
        int test = 0;

        // このオブジェクトの子オブジェクトに対して処理を行う
        foreach(Transform child in this.gameObject.transform)
        {
            ChildObjects[test] = child.gameObject;
            test++;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // 体力加算処理
        if (Input.GetKeyDown(KeyCode.Z))    PlayerHP++;
        if (Input.GetKeyDown(KeyCode.X))    PlayerHP += 2;

        // 体力減算処理
        if (Input.GetKeyDown(KeyCode.C))    PlayerHP--;
        if (Input.GetKeyDown(KeyCode.V))    PlayerHP -= 2;

        // 体力値に応じた色変更処理
        UpdateColor();
    }


    /*-------- 体力値に応じて色味を変更する --------*/
    void UpdateColor()
    {
        // 最大体力値分ループ
        for(int i = 0; i < MaxHP; i++) {
            // 現在体力値分ループし、色をOnColorに変更
            for(; i < PlayerHP; i++)
            {
                ChildObjects[i].GetComponent<Image>().color = OnColor;
            }

            // 色をOffColorに変更
            ChildObjects[i].GetComponent<Image>().color = OffColor;
        }
    }
}
