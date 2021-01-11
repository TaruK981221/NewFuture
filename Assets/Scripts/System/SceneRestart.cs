using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamProject
{

    public class SceneRestart : MonoBehaviour
    {
        private string currentSceneName = null;
        public float fadeOutTime;
        private bool sceneTransFlag = true;
        // Start is called before the first frame update
        void Start()
        {
            sceneTransFlag = true;
            currentSceneName = SceneManager.GetActiveScene().name;
        }

        // Update is called once per frame
        void Update()
        {
            if (sceneTransFlag)
            {
                //メニューボタンでシーンのリスタート
                if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Menu))
                {
                    sceneTransFlag = false;
                    FadeManager.FadeOut(currentSceneName, fadeOutTime);
                }
            }
        }
    }
}