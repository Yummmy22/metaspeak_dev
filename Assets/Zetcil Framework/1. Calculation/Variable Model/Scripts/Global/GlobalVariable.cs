/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Deklarasi Global Variable 
 **************************************************************************************************************/

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{
    public class GlobalVariable : MonoBehaviour
    {
        public enum CInvokeType { OnAwake, OnStart, OnEvent, OnDelay, OnInterval, OnUpdate }
        public enum CVariableType {timeVar, healthVar, manaVar, expVar, scoreVar, intVar, floatVar, stringVar, boolVar, objectVar };
        public CVariableType VariableType;
       
        public VarTime TimeVariables;
        public VarHealth HealthVariables;
        public VarMana ManaVariables;
        public VarExp ExpVariables;
        public VarScore ScoreVariables;
        public VarInteger IntVariables;
        public VarFloat FloatVariables;
        public VarString StringVariables;
        public VarBoolean BoolVariables;
        public VarBoolean ObjectVariables;

    }

}