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
    public class VarBoolean : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        [ConditionalField("isEnabled")] public bool CurrentValue;

        public void SetPrefCurrentValue(string aID)
        {
            PlayerPrefs.SetString(aID, CurrentValue.ToString());
        }

        public void GetPrefCurrentValue(string aID)
        {
            CurrentValue = bool.Parse(PlayerPrefs.GetString(aID));
        }

        public bool GetCurrentValue()
        {
            return CurrentValue;
        }

        public void SetCurrentValue(bool aValue)
        {
            CurrentValue = aValue;
        }

        public void SetCurrentValue(VarBoolean aValue)
        {
            CurrentValue = aValue.CurrentValue;
        }

        public void InputCurrentValue(Toggle aValue)
        {
            CurrentValue = aValue.isOn;
        }

        void Update()
        {
        }
    }
}