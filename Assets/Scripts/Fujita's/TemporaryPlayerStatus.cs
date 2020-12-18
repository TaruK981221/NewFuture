using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlayerStatus : MonoBehaviour
{
    // モード関連
    // モード名(テスト用)
    private enum ModeNames {
        alpha = 0,
        beta,
        gamma
    }
    ModeNames Mode = 0;
 
    // モード切替関数実行用
    [SerializeField] private string ModeInfo = "Mode";
    GameObject ModeUI;

    
    // 体力関連
    [SerializeField] public int MaxHP = 7;     // 最大体力
    [SerializeField] public int HP = 4;        // 現在体力

    
    void Start()
    {
        ModeUI = GameObject.Find(ModeInfo).gameObject;
    }


    void Update()
    {
        // 体力加算処理
        if (Input.GetKeyDown(KeyCode.Z)) HP++;
        if (Input.GetKeyDown(KeyCode.X)) HP += 2;

        // 体力減算処理
        if (Input.GetKeyDown(KeyCode.C)) HP--;
        if (Input.GetKeyDown(KeyCode.V)) HP -= 2;

        if (HP > MaxHP) HP = MaxHP;
        if (HP < 0) HP = 0;


        // モード切替関連
        if (!(ModeUI.GetComponent<RotateMode>().RotateFlag))
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (Mode == ModeNames.beta || Mode == ModeNames.gamma) Mode--;
                else Mode = ModeNames.gamma;
                ModeUI.GetComponent<RotateMode>().ReadyRotate("right");
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                if (Mode == ModeNames.alpha || Mode == ModeNames.beta) Mode++;
                else Mode = ModeNames.alpha;
                ModeUI.GetComponent<RotateMode>().ReadyRotate("left");
            }
        }
    }
}
