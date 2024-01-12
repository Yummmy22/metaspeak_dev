/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menunjukkan deteksi input dalam Unity 
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class UnityInput : MonoBehaviour
    {

        [System.Serializable]
        public class CKeyboardArray
        {
            public KeyCode InputKeyDown;
            public UnityEvent KeyDownEvent;

            [Space(10)]
            public KeyCode InputKeyPress;
            public UnityEvent KeyEvent;

            [Space(10)]
            public KeyCode InputKeyUp;
            public UnityEvent KeyUpEvent;
        }

        public bool isEnabled;
        public List<CKeyboardArray> KeyboardArray;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                for (int i = 0; i < KeyboardArray.Count; i++)
                {
                    if (Input.GetKeyDown(KeyboardArray[i].InputKeyDown))
                    {
                        KeyboardArray[i].KeyDownEvent.Invoke();
                    }
                    if (Input.GetKey(KeyboardArray[i].InputKeyPress))
                    {
                        KeyboardArray[i].KeyEvent.Invoke();
                    }
                    if (Input.GetKeyUp(KeyboardArray[i].InputKeyUp))
                    {
                        KeyboardArray[i].KeyUpEvent.Invoke();
                    }
                }
            }
        }
    }
}
