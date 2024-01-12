    /**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menampung nilai global variabel
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarTime : MonoBehaviour
    {
        public enum CTimeCalculation { Increment, Decrement }

        [Space(10)]
        public bool isEnabled;
        [ConditionalField("isEnabled")] public int CurrentValue;
        [ConditionalField("isEnabled")] public bool Constraint;
        [ConditionalField("Constraint")] public int MinValue;
        [ConditionalField("Constraint")] public int MaxValue;

        [Header("Time Settings")]
        [ConditionalField("isEnabled")] public CTimeCalculation TimeCalculation;

        public void SaveVariable()
        {
            PlayerPrefs.SetInt(transform.gameObject.name, CurrentValue);
        }

        public void SaveVariableDebug()
        {
            PlayerPrefs.SetInt(transform.gameObject.name, CurrentValue);
            Debug.Log("Save Variable [" + transform.gameObject.name + "]: " + CurrentValue.ToString());
        }

        public void LoadVariable()
        {
            CurrentValue = PlayerPrefs.GetInt(transform.name);
        }

        public void LoadVariableDebug()
        {
            CurrentValue = PlayerPrefs.GetInt(transform.name);
            Debug.Log("Load Variable [" + transform.gameObject.name + "]: " + CurrentValue.ToString());
        }


        // Use this for initialization
        void Start()
        {
            if (isEnabled)
            {
                ActivateTimer();
            }
        }

        void ExecuteIncrement()
        {
            if (isEnabled)
            {
                CurrentValue = CurrentValue + 1;
                if (Constraint && CurrentValue >= MaxValue)
                {
                    isEnabled = false;
                    CancelInvoke();
                }
            }
        }

        void ExecuteDecrement()
        {
            if (isEnabled)
            {
                CurrentValue = CurrentValue - 1;
                if (Constraint && CurrentValue <= MinValue)
                {
                    isEnabled = false;
                    CancelInvoke();
                }
            }
        }


        public void OutputFromCurrentValue(InputField aValue)
        {
            aValue.text = CurrentValue.ToString();
        }

        public void OutputFromCurrentValue(Text aValue)
        {
            aValue.text = CurrentValue.ToString();
        }

        public void OutputFromCurrentValue(TextMesh aValue)
        {
            aValue.text = CurrentValue.ToString();
        }
        // Update is called once per frame
        void Update()
        {
        }

        void ActivateTimer()
        {
            if (MaxValue == 0)
            {
                MaxValue = CurrentValue;
            }

            if (TimeCalculation == CTimeCalculation.Increment)
            {
                InvokeRepeating("ExecuteIncrement", 1, 1);
            }
            if (TimeCalculation == CTimeCalculation.Decrement)
            {
                InvokeRepeating("ExecuteDecrement", 1, 1);
            }
        }

        public void StartTimer()
        {
            isEnabled = true;
            ActivateTimer();
        }

        public void StopTimer()
        {
            isEnabled = false;
            CancelInvoke();
        }

        public int GetCurrentValue()
        {
            return CurrentValue;
        }

        public void SetCurrentValue(int aValue)
        {
            CurrentValue = aValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void AddToCurrentValue(int aValue)
        {
            CurrentValue += aValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
        }

        public void SubFromCurrentValue(int aValue)
        {
            CurrentValue -= aValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public bool IsShutdown()
        {
            return CurrentValue <= 0;
        }

        public void InputToCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.IntegerNumber)
            {
                CurrentValue = int.Parse(aValue.text);
            }
            else
            {
                Debug.Log("Error type: Invalid InputField.ContentType.IntegerNumber");
            }
        }

    }

}
