/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk membuat pergerakan antara 2 vector
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class UnityLerp : MonoBehaviour
    {
        public bool isEnabled;
        public GameObject TargetObject;
        public Transform EndPosition;
        public float LerpSpeed;
        Transform StartPosition;

        void InvokeLerp()
        {
            StartPosition.position = Vector3.Lerp(StartPosition.position, EndPosition.position, LerpSpeed * Time.deltaTime);
            TargetObject.transform.position = StartPosition.position;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                if (TargetObject == null)
                {
                    TargetObject = this.gameObject;
                }
                StartPosition = TargetObject.transform;
                StartPosition.position = TargetObject.transform.position;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                InvokeLerp();
            }
        }

    }
}
