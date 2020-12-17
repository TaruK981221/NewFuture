using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMode : MonoBehaviour
{
    // 画像サイズ, ローカル位置, 必要フレーム数
    //[SerializeField] private Vector3 FrontSize = new Vector3(1.45f, 1.45f, 1.0f);           // 選択中モードの描画サイズ
    //[SerializeField] private Vector3 BackSize = new Vector3(0.865f, 0.865f, 1.0f);          // 回転時最小画像サイズ
    //[SerializeField] private Vector3 RocalPositions = new Vector3(-180.0f, 0.0f, 180.0f);   // 左, 中, 右の画像位置
    //[SerializeField] private int RotateFrame = 90;                                          // 回転完了までに必要なフレーム数

    // 子オブジェクト格納用
    private List<GameObject> ModeObjects = new List<GameObject>();

    // 回転方向
    private string Direction;
    // UI回転フラグ
    public bool RotateFlag = false;


    // Start is called before the first frame update
    void Start()
    {
    }
    
    void Update()
    {
        if (RotateFlag)
        {
            if (Direction == "right")
            {
                // 右UI移動 ++++++++++++
                Vector3 pos = ModeObjects[0].transform.localPosition;
                Vector3 scale = ModeObjects[0].transform.localScale;
                ModeObjects[0].transform.localPosition = new Vector3(pos.x - 4.0f, 0.0f, 0.0f);
                // 中心を超えたら処理変更
                if (pos.x < 0.0f)   ModeObjects[0].transform.localScale = new Vector3(scale.x - 0.003f, scale.y - 0.003f, 1.0f);
                else                ModeObjects[0].transform.localScale = new Vector3(scale.x + 0.003f, scale.y + 0.003f, 1.0f);

                // 左UI移動 ++++++++++++
                pos = ModeObjects[1].transform.localPosition;
                scale = ModeObjects[1].transform.localScale;
                ModeObjects[1].transform.localPosition = new Vector3(pos.x + 2.0f, 0.0f, 0.0f);
                ModeObjects[1].transform.localScale = new Vector3(scale.x + 0.005f, scale.y + 0.005f, 1.0f);

                // 中UI移動 ++++++++++++
                pos = ModeObjects[2].transform.localPosition;
                scale = ModeObjects[2].transform.localScale;
                ModeObjects[2].transform.localPosition = new Vector3(pos.x + 2.0f, 0.0f, 0.0f);
                ModeObjects[2].transform.localScale = new Vector3(scale.x - 0.005f, scale.y - 0.005f, 1.0f);

                // 終了処理 ++++++++++++
                if (pos.x >= 180.0f) {
                    ModeObjects[0].transform.localPosition = new Vector3(-180.0f, 0.0f, 0.0f);
                    ModeObjects[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    ModeObjects[1].transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    ModeObjects[1].transform.localScale = new Vector3(1.45f, 1.45f, 1.0f);
                    ModeObjects[1].transform.SetAsLastSibling();

                    ModeObjects[2].transform.localPosition = new Vector3(180.0f, 0.0f, 0.0f);
                    ModeObjects[2].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    ModeObjects[2].transform.SetAsFirstSibling();

                    ModeObjects.Remove(ModeObjects[2]);
                    ModeObjects.Remove(ModeObjects[1]);
                    ModeObjects.Remove(ModeObjects[0]);
                    RotateFlag = false;
                }
            }

            if (Direction == "left")
            {
                // 右UI移動 ++++++++++++
                Vector3 pos = ModeObjects[0].transform.localPosition;
                Vector3 scale = ModeObjects[0].transform.localScale;
                ModeObjects[0].transform.localPosition = new Vector3(pos.x - 2.0f, 0.0f, 0.0f);
                ModeObjects[0].transform.localScale = new Vector3(scale.x + 0.005f, scale.y + 0.005f, 1.0f);

                // 左UI移動 ++++++++++++
                pos = ModeObjects[1].transform.localPosition;
                scale = ModeObjects[1].transform.localScale;
                ModeObjects[1].transform.localPosition = new Vector3(pos.x + 4.0f, 0.0f, 0.0f);
                // 中心を超えたら処理変更
                if (pos.x > 0.0f) ModeObjects[1].transform.localScale = new Vector3(scale.x - 0.003f, scale.y - 0.003f, 1.0f);
                else ModeObjects[1].transform.localScale = new Vector3(scale.x + 0.003f, scale.y + 0.003f, 1.0f);

                // 中UI移動 ++++++++++++
                pos = ModeObjects[2].transform.localPosition;
                scale = ModeObjects[2].transform.localScale;
                ModeObjects[2].transform.localPosition = new Vector3(pos.x - 2.0f, 0.0f, 0.0f);
                ModeObjects[2].transform.localScale = new Vector3(scale.x - 0.005f, scale.y - 0.005f, 1.0f);

                // 終了処理 ++++++++++++
                if (pos.x <= -180.0f)
                {
                    ModeObjects[0].transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    ModeObjects[0].transform.localScale = new Vector3(1.45f, 1.45f, 1.0f);
                    ModeObjects[0].transform.SetAsLastSibling();

                    ModeObjects[1].transform.localPosition = new Vector3(180.0f, 0.0f, 0.0f);
                    ModeObjects[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    ModeObjects[2].transform.localPosition = new Vector3(-180.0f, 0.0f, 0.0f);
                    ModeObjects[2].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    ModeObjects.Remove(ModeObjects[2]);
                    ModeObjects.Remove(ModeObjects[1]);
                    ModeObjects.Remove(ModeObjects[0]);
                    RotateFlag = false;
                }
            }
        }
    }


    // モード切替処理実行時に一度だけ実行
    public void ReadyRotate(string setDirection)
    {
        // 回転向き保存
        Direction = setDirection;
        // UI回転フラグON
        RotateFlag = true;

        // 右, 左, 中の順番に子オブジェクト格納
        foreach (Transform child in this.gameObject.transform)
        {
            ModeObjects.Add(child.gameObject);
        }

        // 描画順変更
        if(Direction == "left") {
            ModeObjects[1].transform.SetAsFirstSibling();
        }
    }
}
