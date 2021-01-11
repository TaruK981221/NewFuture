using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamProject
{
    //ステージをワンボタンで強制遷移させる
    public class SceneTranslate : MonoBehaviour
    {
        public float fadeOutTime;
        //ステージ数
        private static int STAGENUM = 4;
        //ステージシーン名
        private enum STAGE_NO
        {
            STAGE01 = 0, STAGE02, STAGE03, STAGE04,
            STAGE_NUM//ステージの総数
        }
        STAGE_NO stageNo= STAGE_NO.STAGE_NUM;
        private string currentSceneName = null;
        private bool sceneTransFlag = true;

        Dictionary<STAGE_NO, string> stageName = new Dictionary<STAGE_NO, string>() {
        {STAGE_NO.STAGE01, "Stage1"},
        {STAGE_NO.STAGE02, "Stage2"},
        {STAGE_NO.STAGE03, "Stage3"},
        {STAGE_NO.STAGE04, "Stage4"}
    };
        // Start is called before the first frame update
        void Start()
        {
            sceneTransFlag = true;
            currentSceneName = SceneManager.GetActiveScene().name;
            for (int i = 0; i < STAGENUM; i++)
            {
                if (currentSceneName == stageName[(STAGE_NO)i])
                {
                    stageNo = (STAGE_NO)i;
                }
            }
            if (stageNo==STAGE_NO.STAGE_NUM)
            {
                Debug.Log("ここは正規のステージシーンではありません。破棄されます。");
                Destroy(this.gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (sceneTransFlag)
            {
                //1つ前のシーンに遷移
                if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.L1))
                {
                    switch (stageNo)
                    {
                        case STAGE_NO.STAGE01:
                            break;
                        case STAGE_NO.STAGE02:
                        case STAGE_NO.STAGE03:
                        case STAGE_NO.STAGE04:
                            sceneTransFlag = false;
                            stageNo -= 1;
                        FadeManager.FadeOut(stageName[stageNo], fadeOutTime);
                            break;
                    }
                }
                //1つ後のシーンに遷移
               else if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.L2))
                {
                    switch (stageNo)
                    {
                        case STAGE_NO.STAGE01:
                        case STAGE_NO.STAGE02:
                        case STAGE_NO.STAGE03:
                            sceneTransFlag = false;
                            stageNo += 1;
                        FadeManager.FadeOut(stageName[stageNo], fadeOutTime);
                            break;
                        case STAGE_NO.STAGE04:
                            break;
                    }
                }

            }

        }
    }
}