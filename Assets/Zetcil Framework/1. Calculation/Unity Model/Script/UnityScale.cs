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
    public class UnityScale : MonoBehaviour
    {
        public enum CTranslate { None, VectorUp, VectorDown, VectorForward, VectorBack, VectorLeft, VectorRight }

        public bool isEnabled;
        [ConditionalField("isEnabled")] public GameObject TargetObject;

        [Header("Scale Settings")]
        public bool usingScale;
        [ConditionalField("usingScale")] public Vector3 ScaleVector;

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
                if (usingScale)
                {
                    TargetObject.transform.localScale += (ScaleVector * Time.deltaTime);
                }
            }
        }

        public void SetScale()
        {
            TargetObject.transform.localScale += ScaleVector;
        }

        public void SetScaleX(float aX)
        {
            TargetObject.transform.localScale += new Vector3(aX, 0, 0);
        }

        public void SetScaleY(float aY)
        {
            TargetObject.transform.localScale += new Vector3(0, aY, 0);
        }

        public void SetScaleZ(float aZ)
        {
            TargetObject.transform.localScale += new Vector3(0, 0, aZ);
        }

    }
}
