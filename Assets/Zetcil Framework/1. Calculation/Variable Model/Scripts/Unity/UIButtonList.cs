using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    public class UIButtonList : MonoBehaviour
    {
        [Header("String List")]
        public VarStringList TargetStringList;

        [Header("Content List")]
        public GameObject TargetParent;
        public GameObject TargetButton;

        public void InitializeList()
        {
            for (int i = 0; i < TargetStringList.StringListValue.Count; i++)
            {
                GameObject temp = Instantiate(TargetButton, TargetParent.transform);
                string captionList = TargetStringList.StringListValue[i];
                string[] captionListArr = captionList.Split('.');
                if (captionListArr.Length > 0)
                {
                    if (captionListArr[0].Length > 0)
                    {
                        temp.GetComponentInChildren<Text>().text = captionListArr[0];
                    }
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Invoke("InitializeList", 1);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
