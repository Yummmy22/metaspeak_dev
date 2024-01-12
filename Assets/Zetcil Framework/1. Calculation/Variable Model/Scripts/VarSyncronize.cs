using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class VarSyncronize : MonoBehaviour
    {
        public enum CVariableType { timeVar, healthVar, manaVar, expVar, scoreVar, intVar, floatVar, stringVar, boolVar, objectVar };

        [Space(10)]
        public bool isEnabled;

        [Header("Search Settings")]
        public string VariableName;

        [Header("Target Variable")]
        public CVariableType VariableType;
        public VarTime TimeVariables;
        public VarHealth HealthVariables;
        public VarMana ManaVariables;
        public VarExp ExpVariables;
        public VarScore ScoreVariables;
        public VarInteger IntegerVariables;
        public VarFloat FloatVariables;
        public VarString StringVariables;
        public VarBoolean BooleanVariables;
        public VarObject ObjectVariables;
        // Start is called before the first frame update

        [HideInInspector]
        public GameObject TargetVariable; 
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (TargetVariable == null)
            {
                TargetVariable = GameObject.Find(VariableName);
            }    

            if (TargetVariable)
            {
                if (VariableType == CVariableType.timeVar)
                {
                    TimeVariables.CurrentValue = TargetVariable.GetComponent<VarTime>().CurrentValue;
                }
                if (VariableType == CVariableType.healthVar)
                {
                    HealthVariables.CurrentValue = TargetVariable.GetComponent<VarHealth>().CurrentValue;
                }
                if (VariableType == CVariableType.manaVar)
                {
                    ManaVariables.CurrentValue = TargetVariable.GetComponent<VarMana>().CurrentValue;
                }
                if (VariableType == CVariableType.expVar)
                {
                    ExpVariables.CurrentValue = TargetVariable.GetComponent<VarExp>().CurrentValue;
                }
                if (VariableType == CVariableType.scoreVar)
                {
                    ScoreVariables.CurrentValue = TargetVariable.GetComponent<VarScore>().CurrentValue;
                }
                if (VariableType == CVariableType.intVar)
                {
                    IntegerVariables.CurrentValue = TargetVariable.GetComponent<VarInteger>().CurrentValue;
                }
                if (VariableType == CVariableType.floatVar)
                {
                    FloatVariables.CurrentValue = TargetVariable.GetComponent<VarFloat>().CurrentValue;
                }
                if (VariableType == CVariableType.stringVar)
                {
                    StringVariables.CurrentValue = TargetVariable.GetComponent<VarString>().CurrentValue;
                }
                if (VariableType == CVariableType.boolVar)
                {
                    BooleanVariables.CurrentValue = TargetVariable.GetComponent<VarBoolean>().CurrentValue;
                }
                if (VariableType == CVariableType.objectVar)
                {
                    ObjectVariables.CurrentValue = TargetVariable.GetComponent<VarObject>().CurrentValue;
                }
            }
        }
    }

}
