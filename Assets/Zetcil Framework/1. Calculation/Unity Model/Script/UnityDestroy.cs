/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menunjukkan dasar-dasar Destroy pada Unity 
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class UnityDestroy : MonoBehaviour
    {
        public bool isEnabled;
        [ConditionalField("isEnabled")] public GameObject TargetObject;
        [ConditionalField("isEnabled")] public float Delay = -1;
        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                if (TargetObject == null)
                {
                    TargetObject = this.gameObject;
                }
                if (Delay > 0)
                {
                    Destroy(TargetObject.gameObject, Delay);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ExecuteDestroy()
        {
            Destroy(TargetObject.gameObject, Delay);
        }

        public void InvokeDestroy()
        {
            Destroy(TargetObject.gameObject, Delay);
        }
    }
}
