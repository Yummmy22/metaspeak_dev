using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarVector3 : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;

        [Header("Variables Settings")]
        [ConditionalField("usingVector3")] public Vector3 CurrentValue;
        [ConditionalField("usingVector3")] public VarFloat Vector3X;
        [ConditionalField("usingVector3")] public VarFloat Vector3Y;
        [ConditionalField("usingVector3")] public VarFloat Vector3Z;

        [Header("Output Settings")]
        [ConditionalField("usingVector3")] public bool Position;
        [ConditionalField("usingVector3")] public bool Rotation;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3X.CurrentValue = CurrentValue.x;
            Vector3Y.CurrentValue = CurrentValue.y;
            Vector3Z.CurrentValue = CurrentValue.z;
            if (Rotation)
            {
                transform.localRotation = Quaternion.Euler(CurrentValue);
            }
            if (Position)
            {
                transform.position = CurrentValue;
            }
        }

        public float GetCurrentValueX()
        {
            return Vector3X.CurrentValue;
        }

        public float GetCurrentValueY()
        {
            return Vector3Y.CurrentValue;
        }

        public float GetCurrentValueZ()
        {
            return Vector3Z.CurrentValue;
        }

        public Vector3 GetCurrentValue()
        {
            return CurrentValue;
        }

        public void SetCurrentValue(Vector3 aValue)
        {
            CurrentValue = aValue;
        }

        public void SetCurrentValueX(float aValue)
        {
            CurrentValue.x = aValue;
        }

        public void SetCurrentValueY(float aValue)
        {
            CurrentValue.y = aValue;
        }

        public void SetCurrentValueZ(float aValue)
        {
            CurrentValue.z = aValue;
        }

        public void AddToCurrentValue(Vector3 aValue)
        {
            CurrentValue += aValue;
        }

        public void AddToCurrentValueX(float aValue)
        {
            CurrentValue.x += aValue;
        }

        public void AddToCurrentValueY(float aValue)
        {
            CurrentValue.y += aValue;
        }

        public void AddToCurrentValueZ(float aValue)
        {
            CurrentValue.z += aValue;
        }

        public void SubFromCurrentValue(Vector3 aValue)
        {
            CurrentValue -= aValue;
        }

        public void SubFromCurrentValueX(float aValue)
        {
            CurrentValue.x -= aValue;
        }

        public void SubFromCurrentValueY(float aValue)
        {
            CurrentValue.y -= aValue;
        }

        public void SubFromCurrentValueZ(float aValue)
        {
            CurrentValue.z -= aValue;
        }
    }
}
