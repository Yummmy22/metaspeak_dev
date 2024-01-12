/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menampung nilai global variabel
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class VarList : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        public VarInteger CurrentIndex;
        public string[] CurrentValue;

        void Update()
        {
        }

        public string GetCurrentValue()
        {
            if (CurrentIndex.CurrentValue < 0) CurrentIndex.CurrentValue = 0;
            if (CurrentIndex.CurrentValue > CurrentValue.Length-1) CurrentIndex.CurrentValue = CurrentValue.Length-1;
            return CurrentValue[CurrentIndex.CurrentValue];
        }

        public void SetCurrentValue(string aValue)
        {
            if (CurrentIndex.CurrentValue < 0) CurrentIndex.CurrentValue = 0;
            if (CurrentIndex.CurrentValue > CurrentValue.Length-1) CurrentIndex.CurrentValue = CurrentValue.Length - 1;
            CurrentValue[CurrentIndex.CurrentValue] = aValue;
        }

        public void AddToCurrentValue(string aValue)
        {
            if (CurrentIndex.CurrentValue < 0) CurrentIndex.CurrentValue = 0;
            if (CurrentIndex.CurrentValue > CurrentValue.Length-1) CurrentIndex.CurrentValue = CurrentValue.Length - 1;
            CurrentValue[CurrentIndex.CurrentValue] += aValue;
        }

    }

}