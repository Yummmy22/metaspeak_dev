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

    public class VarObject : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        [ConditionalField("isEnabled")] public GameObject CurrentValue;

        // Use this for initialization
        void Start()
        {

        }

        public string GetObjectName()
        {
            string objName = "";
            if (CurrentValue) objName = CurrentValue.name;
            return objName;
        }

        public string GetObjectTag()
        {
            string objTag = "";
            if (CurrentValue) objTag = CurrentValue.tag;
            return objTag;
        }

        public void ShowDebugObjectName()
        {
            if (CurrentValue) Debug.Log(CurrentValue.name);
        }

        public void ShowDebugObjectTag()
        {
            if (CurrentValue) Debug.Log(CurrentValue.tag);
        }

        public Vector3 GetObjectPosition()
        {
            Vector3 objPosition = Vector3.zero;
            if (CurrentValue) objPosition = CurrentValue.transform.position;
            return objPosition;
        }

        public Quaternion GetObjectRotation()
        {
            Quaternion objRotation = new Quaternion();
            if (CurrentValue) objRotation = CurrentValue.transform.rotation;
            return objRotation;
        }

        public void SetObjectActive(bool aValue) {
            if (CurrentValue) CurrentValue.SetActive(aValue);
        }

        public void SetObjectPosition(Vector3 aValue)
        {
            if (CurrentValue) CurrentValue.transform.position = aValue; 
        }

        public void SetCurrentValue(GameObject aValue)
        {
            CurrentValue = aValue;
        }

        public void ClearCurrentValue()
        {
            CurrentValue = null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
