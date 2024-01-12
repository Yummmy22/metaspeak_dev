using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    public class UIButtonField : MonoBehaviour
    {

        public InputField TargetInputField;

        public void SetText()
        {
            TargetInputField.text = GetComponentInChildren<Text>().text;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
