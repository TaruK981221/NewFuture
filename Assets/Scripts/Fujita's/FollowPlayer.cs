using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(1)]              // Script実行順(数値が小さい程先に実行)

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private string PlayerName = "PlayerObj";    // 追従するオブジェクト名を格納
    [SerializeField]
    private int KeepFlame = 1;                  // 何フレーム前まで保存するか(最小値1)

    private Transform Player;                   // プレイヤー座標保存用
    private Vector3[] KeepPos;                  // 数フレーム前のプレイヤー座標

    // Start is called before the first frame update
    void Start()
    {
        // 追従するオブジェクトを指定し、情報を格納
        Player = GameObject.Find(PlayerName).transform;

        KeepPos = new Vector3[KeepFlame];

        // 配列の初期化
        for (int i = 0; i < (KeepFlame - 1); i++)
        {
            KeepPos[i] = Player.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 数フレーム前のプレイヤー位置保存
        for (int i = (KeepFlame - 1); i > 0; i--)
        {
            KeepPos[i] = KeepPos[i - 1];
        }
        KeepPos[0] = Player.transform.position;

        // カメラ位置更新
        this.transform.position = new Vector3(KeepPos[KeepFlame - 1].x, 0, -10);
    }
}