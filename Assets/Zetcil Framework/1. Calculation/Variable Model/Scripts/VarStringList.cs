using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{
    public class VarStringList : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        public VarString TargetString;

        [Header("String List")]
        public int CurrentIndex;
        public List<string> StringListValue;

        // Start is called before the first frame update

        public void SetPrefCurrentIndex()
        {
            CurrentIndex = PlayerPrefs.GetInt("CurrentLevel");
        }

        public void SetCurrentIndex(VarInteger aValue)
        {
            CurrentIndex = aValue.CurrentValue;
        }

        void Start()
        {
            //TargetString.CurrentValue = StringListValue[CurrentIndex];
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
