/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk membuat instansiasi dari sebuah objek/class
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class UnityInstantiate : MonoBehaviour
    {
        public enum CInstantiateType { None, Start, Update }

        public bool isEnabled;
        public CInstantiateType InstantiateType;

        public GameObject TargetParent;
        public GameObject TargetPrefab;
        public bool DetachFromParent;

        public void InvokeInstantiateGameObject()
        {
            GameObject temp = Instantiate(TargetPrefab, TargetParent.transform.position, TargetParent.transform.rotation, TargetParent.transform);
            if (DetachFromParent)
            {
                temp.transform.parent = null;
            }
        }

        public void ExecuteInstantiateGameObject()
        {
            GameObject temp = Instantiate(TargetPrefab, TargetParent.transform.position, TargetParent.transform.rotation, TargetParent.transform);
            if (DetachFromParent)
            {
                temp.transform.parent = null;
            }
        }

        public void InstantiateGameObject()
        {
            GameObject temp = Instantiate(TargetPrefab, TargetParent.transform.position, TargetParent.transform.rotation, TargetParent.transform);
            if (DetachFromParent)
            {
                temp.transform.parent = null;
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            if (InstantiateType == CInstantiateType.Start)
            {
                InstantiateGameObject();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (InstantiateType == CInstantiateType.Update)
            {
                InstantiateGameObject();
            }
        }
    }
}
