using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    [SerializeField] private string CameraName = "Main Camera";     // このカメラ位置を参照して背景を移動させる
    [SerializeField] private float BGWidth = 20;                    // 背景画像の横幅

    private Transform CameraTra;    // カメラへの参照保存用
    private Vector2 BGPos;          // 背景画像の端位置

    // Start is called before the first frame update
    void Start()
    {
        // カメラへの参照先保存
        CameraTra = GameObject.Find(CameraName).transform;

        // 背景画像の端位置保存
        BGPos.x = this.transform.position.x + BGWidth / 2;      // 右端
        BGPos.y = this.transform.position.x - BGWidth / 2;      // 左端
    }

    // Update is called once per frame
    void Update()
    {

        // カメラ位置が自身の範囲内で無ければ
        if (!(CameraTra.position.x <= BGPos.x && CameraTra.position.x >= BGPos.y)) {
            // インスタンス生成
            Vector3 pos = this.transform.position;

            // カメラ位置に応じて自身の位置を右or左に移動させる
            if (CameraTra.position.x < pos.x - BGWidth) {
                this.transform.position = new Vector3(pos.x - BGWidth * 2, pos.y, pos.z);   // 位置更新
            }
            else if (CameraTra.position.x > pos.x + BGWidth) {
                this.transform.position = new Vector3(pos.x + BGWidth * 2, pos.y, pos.z);   // 位置更新
            }

            // 端位置更新
            BGPos.x = this.transform.position.x + BGWidth / 2;      // 右端
            BGPos.y = this.transform.position.x - BGWidth / 2;      // 左端
        }

    }
}
