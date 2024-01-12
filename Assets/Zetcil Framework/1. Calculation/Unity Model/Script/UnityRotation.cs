/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menunjukkan dasar-dasar pergerakan dalam Unity yang terdiri dari Position, Rotation, & Scale.
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class UnityRotation : MonoBehaviour
    {
        public bool isEnabled;
        [ConditionalField("isEnabled")] public GameObject TargetObject;

        [Header("Rotation Settings")]
        public bool usingRotation;
        [ConditionalField("usingRotation")] public Vector3 RotationVector;

        [Header("Quaternion Settings")]
        public bool usingQuaternion;
        [ConditionalField("usingQuaternion")] public Vector3 QuaternionVector;

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                if (TargetObject == null)
                {
                    TargetObject = this.gameObject;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (usingRotation)
                {
                    TargetObject.transform.Rotate(RotationVector * Time.deltaTime);
                }
                if (usingQuaternion)
                {
                    TargetObject.transform.localRotation = Quaternion.Euler(QuaternionVector); 
                }
            }
        }

        public void SetRotation()
        {
            TargetObject.transform.Rotate(RotationVector);
        }

        public void SetRotationX(float aX)
        {
            TargetObject.transform.Rotate(aX, 0, 0);
        }

        public void SetRotationY(float aY)
        {
            TargetObject.transform.Rotate(0, aY, 0);
        }

        public void SetRotationZ(float aZ)
        {
            TargetObject.transform.Rotate(0, 0, aZ);
        }

        public void SetQuaternionX(float aX)
        {
            TargetObject.transform.localRotation = Quaternion.Euler(aX, 0, 0);
        }

        public void SetQuaternionY(float aY)
        {
            TargetObject.transform.localRotation = Quaternion.Euler(0, aY, 0);
        }

        public void SetQuaternionZ(float aZ)
        {
            TargetObject.transform.localRotation = Quaternion.Euler(0, 0, aZ);
        }

    }
}
