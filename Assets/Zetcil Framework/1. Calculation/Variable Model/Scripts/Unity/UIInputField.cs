using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{

    public class UIInputField : MonoBehaviour
    {
        public InputField TargetInputField;

        public void SetText(Text aValue)
        {
            TargetInputField.text = aValue.text;
        }

        public void SetText(VarString aValue)
        {
            TargetInputField.text = aValue.CurrentValue;
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
