/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menampung nilai global variabel
 **************************************************************************************************************/

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class VarExp : MonoBehaviour
    {
        [System.Serializable]
        public class CExpSettings
        {
            public float MinValue;
            public float MaxValue;
            [Header("Exp Events")]
            public bool usingExpEvent;
            public UnityEvent ExpEvent;
        }

        [Space(10)]
        public bool isEnabled;
        public float CurrentValue;
        public bool Configuration;
        [Header("Exp Hierarchy")]
        public VarInteger ExpLevel;
        public List<CExpSettings> ExpSetting;

        [HideInInspector] public float MinValue;
        [HideInInspector] public float MaxValue;

        bool isActive;

        public void SaveVariable()
        {
            PlayerPrefs.SetFloat(transform.gameObject.name, CurrentValue);
        }

        public void SaveVariableDebug()
        {
            PlayerPrefs.SetFloat(transform.gameObject.name, CurrentValue);
            Debug.Log("Save Variable [" + transform.gameObject.name + "]: " + CurrentValue.ToString());
        }

        public void LoadVariable()
        {
            CurrentValue = PlayerPrefs.GetFloat(transform.name);
        }

        public void LoadVariableDebug()
        {
            CurrentValue = PlayerPrefs.GetFloat(transform.name);
            Debug.Log("Load Variable [" + transform.gameObject.name + "]: " + CurrentValue.ToString());
        }


        public void SetCurrentLevel(int aValue)
        {
            ExpLevel.CurrentValue = aValue;
        }

        public float GetCurrentLevel()
        {
            return ExpLevel.CurrentValue;
        }

        public float GetCurrentValue()
        {
            return CurrentValue;
        }

        public float GetTotalExperience()
        {
            float total = 0;
            for (int i=0; i < ExpLevel.CurrentValue; i++)
            {
                total = total + ExpSetting[ExpLevel.CurrentValue].MaxValue;
            }
            return total;
        }

        public void DebugGetTotalExperience()
        {
            float total = 0;
            for (int i = 0; i < ExpLevel.CurrentValue; i++)
            {
                total = total + ExpSetting[ExpLevel.CurrentValue].MaxValue;
            }
            Debug.Log(total);
        }

        public float GetMinValue()
        {
            return ExpSetting[ExpLevel.CurrentValue].MinValue;
        }

        public float GetMaxValue()
        {
            return ExpSetting[ExpLevel.CurrentValue].MaxValue;
        }

        public void SetMinValue(float aValue)
        {
            ExpSetting[ExpLevel.CurrentValue].MinValue = aValue;
            MinValue = aValue;
        }

        public void SetMaxValue(float aValue)
        {
            ExpSetting[ExpLevel.CurrentValue].MaxValue = aValue;
            MaxValue = aValue;
        }

        public void SetCurrentValue(float aValue)
        {
            CurrentValue = aValue;
            if (CurrentValue >= ExpSetting[ExpLevel.CurrentValue].MaxValue) CurrentValue = ExpSetting[ExpLevel.CurrentValue].MaxValue;
            if (CurrentValue <= ExpSetting[ExpLevel.CurrentValue].MinValue) CurrentValue = ExpSetting[ExpLevel.CurrentValue].MinValue;
        }

        public void AddExpMechanic(float aValue)
        {
            CurrentValue += aValue;
            if (CurrentValue >= ExpSetting[ExpLevel.CurrentValue].MaxValue)
            {
                float offset = CurrentValue - ExpSetting[ExpLevel.CurrentValue].MaxValue;
                if (ExpLevel.CurrentValue < ExpSetting.Count - 1)
                {
                    ExpLevel.CurrentValue++;
                    SetMinValue(ExpSetting[ExpLevel.CurrentValue].MinValue);
                    SetMaxValue(ExpSetting[ExpLevel.CurrentValue].MaxValue);
                    CurrentValue = offset;
                    if (ExpSetting[ExpLevel.CurrentValue].usingExpEvent)
                    {
                        ExpSetting[ExpLevel.CurrentValue].ExpEvent.Invoke();
                    }
                }
                else
                {
                    if (CurrentValue >= ExpSetting[ExpLevel.CurrentValue].MaxValue) CurrentValue = ExpSetting[ExpLevel.CurrentValue].MaxValue;
                }
            }
        }

        public void AddToCurrentValue(float aValue)
        {
            CurrentValue += aValue;
            if (CurrentValue >= ExpSetting[ExpLevel.CurrentValue].MaxValue) CurrentValue = ExpSetting[ExpLevel.CurrentValue].MaxValue;
        }

        public void SubtractFromCurrentValue(float aValue)
        {
            CurrentValue -= aValue;
            if (CurrentValue <= ExpSetting[ExpLevel.CurrentValue].MinValue) CurrentValue = ExpSetting[ExpLevel.CurrentValue].MinValue;
        }

        public void SetCurrentValue(VarFloat aValue)
        {
            CurrentValue = aValue.CurrentValue;
        }

        public void AddToCurrentValue(VarFloat aValue)
        {
            CurrentValue += aValue.CurrentValue;
        }

        public void SubtractFromCurrentValue(VarFloat aValue)
        {
            CurrentValue -= aValue.CurrentValue;
        }


        public bool IsShutdown()
        {
            return CurrentValue <= 0;
        }

        void Start()
        {
            if (ExpSetting[ExpLevel.CurrentValue].MaxValue == 0)
            {
                ExpSetting[ExpLevel.CurrentValue].MaxValue = CurrentValue;
            }

        }

        void Update()
        {
        }

       public void InputToCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.DecimalNumber)
            {
                CurrentValue = float.Parse(aValue.text);
            } else
            {
                Debug.Log("Error type: Invalid InputField.ContentType.DecimalNumber");
            }
        }


    }
}