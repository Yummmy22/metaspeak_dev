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
using TechnomediaLabs;

namespace Zetcil
{
    public class VarMana : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        [ConditionalField("isEnabled")] public float CurrentValue;
        [ConditionalField("isEnabled")] public bool Constraint;
        public float MinValue;
        public float MaxValue;
        [Header("Regeneration Settings")]
        public bool usingRegeneration;
        public float RegenerationValue;
        public float RepeatRate;

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


        void Start()
        {
            if (MaxValue == 0)
            {
                MaxValue = CurrentValue;
            }
            if (usingRegeneration)
            {
                InvokeRepeating("ExecuteRegeneration", 1, RepeatRate);
            }
        }

        public void ExecuteRegeneration()
        {
            AddToCurrentValue(RegenerationValue);
        }

        public float GetCurrentValue()
        {
            return CurrentValue;
        }

        public float GetMinValue()
        {
            return MinValue;
        }

        public float GetRegenerationValue()
        {
            return RegenerationValue;
        }

        public void SetRegenerationValue(float aValue)
        {
            RegenerationValue = aValue;
        }

        public float GetMaxValue()
        {
            return MaxValue;
        }

        public void SetMinValue(float aValue)
        {
            MinValue = aValue;
        }

        public void SetMaxValue(float aValue)
        {
            MaxValue = aValue;
        }
        public void SetCurrentValue(float aValue)
        {
            CurrentValue = aValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void AddToCurrentValue(float aValue)
        {
            CurrentValue += aValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
        }

        public void SubtractFromCurrentValue(float aValue)
        {
            CurrentValue -= aValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SetCurrentValue(VarFloat aValue)
        {
            CurrentValue = aValue.CurrentValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void AddToCurrentValue(VarFloat aValue)
        {
            CurrentValue += aValue.CurrentValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
        }

        public void SubtractFromCurrentValue(VarFloat aValue)
        {
            CurrentValue -= aValue.CurrentValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public bool IsShutdown()
        {
            return CurrentValue <= 0;
        }

        void Update()
        {
        }

        public void InputToCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.DecimalNumber)
            {
                CurrentValue = float.Parse(aValue.text);
            }
            else
            {
                Debug.Log("Error type: Invalid InputField.ContentType.DecimalNumber");
            }
        }
    }
}

