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
    public class UnityPosition : MonoBehaviour
    {
        public enum CTranslate { None, VectorUp, VectorDown, VectorForward, VectorBack, VectorLeft, VectorRight }

        public bool isEnabled;
        [ConditionalField("isEnabled")] public GameObject TargetObject;

        [Header("Position Settings")]
        public bool usingPosition;
        [ConditionalField("usingPosition")] public Vector3 PositionVector;

        [Header("Translate Settings")]
        public bool usingTranslate;
        [ConditionalField("usingTranslate")] public CTranslate TranslateType;
        [ConditionalField("usingTranslate")] public float SpeedVector;

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
                if (usingPosition)
                {
                    TargetObject.transform.position += (PositionVector * Time.deltaTime);
                }
                if (usingTranslate)
                {
                    switch (TranslateType)
                    {
                        case CTranslate.VectorUp:
                            TargetObject.transform.Translate(Vector3.up * SpeedVector * Time.deltaTime);
                            break;
                        case CTranslate.VectorDown:
                            TargetObject.transform.Translate(Vector3.down * SpeedVector * Time.deltaTime);
                            break;
                        case CTranslate.VectorForward:
                            TargetObject.transform.Translate(Vector3.forward * SpeedVector * Time.deltaTime);
                            break;
                        case CTranslate.VectorBack:
                            TargetObject.transform.Translate(Vector3.back * SpeedVector * Time.deltaTime);
                            break;
                        case CTranslate.VectorLeft:
                            TargetObject.transform.Translate(Vector3.left * SpeedVector * Time.deltaTime);
                            break;
                        case CTranslate.VectorRight:
                            TargetObject.transform.Translate(Vector3.right * SpeedVector * Time.deltaTime);
                            break;
                    }
                }
            }
        }

        public void SetPosition()
        {
            TargetObject.transform.position += PositionVector;
        }

        public void SetPositionX(float aX)
        {
            TargetObject.transform.position += new Vector3(aX, 0, 0);
        }

        public void SetPositionY(float aY)
        {
            TargetObject.transform.position += new Vector3(0, aY, 0);
        }

        public void SetPositionZ(float aZ)
        {
            TargetObject.transform.position += new Vector3(0, 0, aZ);
        }

        public void SetTranslateVectorUp()
        {
            TargetObject.transform.Translate(Vector3.up * SpeedVector * Time.deltaTime);
        }

        public void SetTranslateVectorDown()
        {
            TargetObject.transform.Translate(Vector3.down * SpeedVector * Time.deltaTime);
        }

        public void SetTranslateVectorLeft()
        {
            TargetObject.transform.Translate(Vector3.left * SpeedVector * Time.deltaTime);
        }

        public void SetTranslateVectorRight()
        {
            TargetObject.transform.Translate(Vector3.right * SpeedVector * Time.deltaTime);
        }

        public void SetTranslateVectorForward()
        {
            TargetObject.transform.Translate(Vector3.forward * SpeedVector * Time.deltaTime);
        }

        public void SetTranslateVectorBack()
        {
            TargetObject.transform.Translate(Vector3.back * SpeedVector * Time.deltaTime);
        }
    }
}
