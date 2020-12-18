using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TitleManager : MonoBehaviour
{
    // 移行先のシーン名保存用
    [SerializeField] private string TransScene = "stage1_test";   // 移行先のシーン名入力

    // タイトルアニメーション用
    [SerializeField] private float PosY = 300.0f;       // タイトル名移動後の位置
    [SerializeField] private float Speed = 1.0f;        // タイトル名移動速度
    [SerializeField] private int BlinkInterval = 120;   // テキスト点滅速度(フレーム単位)

    // 各オブジェクトへの参照保存用
    Transform TitleImage;       // タイトル画像
    Image PressAny;             // PressAnyButtonへの参照保存用
    GameObject SelectMenu;      // SelectMenuへの参照保存用
    GameObject NewGame;

    private int Phase = 0;


    // Start is called before the first frame update
    void Start()
    {
        TitleImage = GameObject.Find("Title").transform;
        PressAny = GameObject.Find("PressAnyButton").GetComponent<Image>();
        SelectMenu = GameObject.Find("SelectMenu");
        NewGame = GameObject.Find("NewGame");
        SelectMenu.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        switch (Phase)
        {
            case 0:
                PhaseZero();    // タイトル名移動, PressAnyButtonの表示
                break;

            case 1:
                PhaseOne();     // PressAnyButton点滅処理, TitleMenu3種表示
                break;

            case 2:
                
                break;
        }
    }

    //-------- タイトル移動～PressAnyButton表示 --------*/
    void PhaseZero()
    {
        Vector3 pos = TitleImage.localPosition;

        TitleImage.localPosition = new Vector3(pos.x, pos.y + Speed, pos.z);

        // タイトル画像が規定値以上もしくはいずれかのキーが押されたら実行
        if(TitleImage.localPosition.y >= PosY || (Input.anyKeyDown))
        {
            // タイトル画像位置調整, テキストの透明度を無くす, フェーズ移行
            TitleImage.localPosition = new Vector3(pos.x, PosY, pos.z);
            PressAny.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            Phase++;
        }
    }


    //-------- PressAnyButton点滅～Menu表示 --------*/
    void PhaseOne()
    {
        // テキストの透明度変更
        PressAny.color = new Vector4(1.0f, 1.0f, 1.0f, PressAny.color.a + (1.0f / BlinkInterval));
        if (PressAny.color.a < 0.0f)
        {
            PressAny.color = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
            BlinkInterval = -BlinkInterval;
        }
        if (PressAny.color.a > 1.0f)
        {
            PressAny.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            BlinkInterval = -BlinkInterval;
        }

        // いずれかのキーが押されたらフェーズ移行
        if (Input.anyKeyDown)
        {
            PressAny.gameObject.SetActive(false);       // PressAnyButtonを画面から消す
            SelectMenu.SetActive(true);                 // ActiveをOnにして画面に表示する
            EventSystem.current.SetSelectedGameObject(NewGame);     // NewGameを選択状態にする
            NewGame.GetComponent<Button>().OnSelect(null);          // ハイライト対策
            Phase++;
        }
    }


    public void TriggerNewGame()
    {
        SceneManager.LoadScene(TransScene);         // シーン移行処理
    }

    public void TriggerContinue()
    {
        SceneManager.LoadScene(TransScene);         // シーン移行処理
    }


    public void TriggerGameEnd()
    {

        UnityEditor.EditorApplication.isPlaying = false;    // ゲーム終了処理(開発画面の場合)
    }
}
