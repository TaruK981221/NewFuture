using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移時のフェードイン・アウトする処理用クラス
/// FadeManager.(関数名)で呼び出せます。
/// </summary>
public class FadeManager : MonoBehaviour
{
    //フェード用のCanvasとImage
    private static Canvas fadeCanvas;
    private static Image fadeImage;

    //フェード用Imageの透明度
    private static float alpha = 0.0f;

    //フェードインアウトのフラグ
    public static bool isFadeIn = false;
    public static bool isFadeOut = false;

    //フェードしたい時間（単位は秒）
    private static float fadeTime = 2.0f;

    //遷移先のシーン番号指定用(BuildSettingの番号指定)
    private static int nextScene_index = -1;

    //遷移先のシーン名指定用
    private static string nextScene_str = null;

    //フェード用のCanvasとImage生成
    static void Init()
    {
        //フェード用のCanvas生成
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<FadeManager>();

        //最前面になるよう適当なソートオーダー設定
        fadeCanvas.sortingOrder = 100;

        //フェード用のImage生成
        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;

        //Imageサイズは適当に大きく設定してください
        fadeImage.rectTransform.sizeDelta = new Vector2(9999, 9999);
    }

    //フェードイン開始(時間指定の初期値は2秒)
    public static void FadeIn(float _time = 2.0f)
    {
        if (fadeImage == null) Init();
        // fadeImage.color = Color.black;
        alpha = 1.0f;
        fadeTime = _time;//フェードタイムの設定
        isFadeIn = true;
    }

    //フェードアウト開始(BuildSettingの番号指定)
    public static void FadeOut(int _sceneNo, float _time = 2.0f)
    {
        if (fadeImage == null) Init();
        //遷移先シーンのセット
        nextScene_index = _sceneNo;
        FadeOutSetting(_time);
    }
    //フェードアウト開始(シーン名の指定)
    public static void FadeOut(string _sceneName, float _time = 2.0f)
    {
        if (fadeImage == null) Init();
        //遷移先シーンのセット
        nextScene_str = _sceneName;
        FadeOutSetting(_time);
    }
    //フェードアウトの準備をする
    private static void FadeOutSetting(float _time)
    {
        fadeImage.color = Color.clear;
        fadeTime = _time;//フェードタイムの設定
        fadeCanvas.enabled = true;
        isFadeOut = true;
    }
    //フェードキャンバスのみ表示（真っ黒画面を出すだけ）
    public static void BlackOut()
    {
        if (fadeImage == null) Init();
        fadeImage.color = Color.black;
        fadeCanvas.enabled = true;


    }

    //フェード中かどうか(フェード中ならfalse)
    public static bool IsFade()
    {
        if (!isFadeIn && !isFadeOut)
        {
            return true;
        }
        return false;
    }


    //更新処理
    void Update()
    {
        //フラグ有効なら毎フレームフェードイン/アウト処理
        if (isFadeIn)
        {
            //フェードイン中の処理
            FadingIn();
        }
        else if (isFadeOut)
        {
            //フェードアウト中の処理
            FadingOut();
        }
    }// Update END

    //フェードイン中の処理
    private void FadingIn()
    {
        //経過時間から透明度計算
        alpha -= Time.deltaTime / fadeTime;

        //フェードイン終了判定
        if (alpha <= 0.0f)
        {
            isFadeIn = false;
            alpha = 0.0f;
            fadeCanvas.enabled = false;
        }

        //フェード用Imageの色・透明度設定
        fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);

    }
    //フェードアウト中の処理
    private void FadingOut()
    {
        //経過時間から透明度計算
        alpha += Time.deltaTime / fadeTime;

        //フェードアウト終了判定
        if (alpha >= 1.0f)
        {
            isFadeOut = false;
            alpha = 1.0f;

            //次のシーンへ遷移
            if (nextScene_index < 0)
            {
                //遷移先のシーンを名前指定
                SceneManager.LoadScene(nextScene_str);
            }
            else
            {
                //遷移先のシーンをインデックス指定
                SceneManager.LoadScene(nextScene_index);
            }
        }

        //フェード用Imageの色・透明度設定
        fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);

    }
}//public class FadeManager : MonoBehaviour END
