using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    /// <summary>
    /// キー入力時の状態（down,keep,up）を持つためのクラス
    /// </summary>
    enum KEYSTATE
    {
        DOWN = 0,//押し始めた時
        HOLD,//押したまま
        UP,//放した時
        ALL
    }
    public class InputFlag 
    {
        public bool[] m_keyFlag = new bool[(int)KEYSTATE.ALL];

        /// <summary>
        /// フラグを3つともfalseにする
        /// </summary>
        public void KeyFlagReset()
        {
            for (int i = 0; i < (int)KEYSTATE.ALL; i++)
            {
                m_keyFlag[i] = false;
            }
        }

        public void KeyFlagCheck(InputFlag _Key, InputManager.ButtonCode _buttonCode)
        {
            _Key.m_keyFlag[(int)KEYSTATE.DOWN] = InputManager.InputManager.Instance.GetKeyDown(_buttonCode);
            _Key.m_keyFlag[(int)KEYSTATE.HOLD] = InputManager.InputManager.Instance.GetKey(_buttonCode);
            _Key.m_keyFlag[(int)KEYSTATE.UP]   = InputManager.InputManager.Instance.GetKeyUp(_buttonCode);
        }
        public void KeyFlagCheck(InputFlag _Key, KeyCode _keyCode)
        {
            _Key.m_keyFlag[(int)KEYSTATE.DOWN] = Input.GetKeyDown(_keyCode);
            _Key.m_keyFlag[(int)KEYSTATE.HOLD] = Input.GetKey(_keyCode);
            _Key.m_keyFlag[(int)KEYSTATE.UP] = Input.GetKeyUp(_keyCode);
        }
    }
}
