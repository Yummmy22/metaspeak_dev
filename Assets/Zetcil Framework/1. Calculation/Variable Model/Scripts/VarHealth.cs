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
    public class VarHealth : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        [ConditionalField("isEnabled")] public float CurrentValue;
        [ConditionalField("isEnabled")] public bool Constraint;
        [ConditionalField("Constraint")] public float MinValue;
        [ConditionalField("Constraint")] public float MaxValue;

        void Start()
        {
            if (MaxValue == 0)
            {
                MaxValue = CurrentValue;
            }
        }

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


        public float GetMinValue()
        {
            return MinValue;
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


        public float GetCurrentValue()
        {
            return CurrentValue;
        }

        public void GetCurrentValue(InputField aValue)
        {
            aValue.text = CurrentValue.ToString();
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

        public void SetCurrentValue(float aValue)
        {
            CurrentValue = aValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SetCurrentValue(VarFloat aValue)
        {
            CurrentValue = aValue.CurrentValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SetCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.DecimalNumber)
            {
                CurrentValue = float.Parse(aValue.text);
            }
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void AddToCurrentValue(float aValue)
        {
            CurrentValue += aValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
        }

        public void AddToCurrentValue(VarFloat aValue)
        {
            CurrentValue += aValue.CurrentValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void AddToCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.DecimalNumber)
            {
                CurrentValue += float.Parse(aValue.text);
            }
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SubFromCurrentValue(float aValue)
        {
            CurrentValue -= aValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SubtractFromCurrentValue(VarFloat aValue)
        {
            CurrentValue -= aValue.CurrentValue;
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public void SubtractFromCurrentValue(InputField aValue)
        {
            if (aValue.contentType == InputField.ContentType.DecimalNumber)
            {
                CurrentValue -= float.Parse(aValue.text);
            }
            if (Constraint && CurrentValue >= MaxValue) CurrentValue = MaxValue;
            if (Constraint && CurrentValue <= MinValue) CurrentValue = MinValue;
        }

        public bool IsShutdown()
        {
            return CurrentValue <= 0;
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