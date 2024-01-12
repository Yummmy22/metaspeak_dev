using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class VarStar : MonoBehaviour
    {
        [System.Serializable]
        public class CStarRange
        {
            public float MinRange;
            public float MaxRange;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("Main Settings")]
        public VarScore CurrentScore;
        public VarInteger CurrentStar;
        public CStarRange StarValue1;
        public CStarRange StarValue2;
        public CStarRange StarValue3;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (isEnabled)
            {
                if (CurrentScore.CurrentValue <= 0)
                {
                    CurrentStar.CurrentValue = 0;
                } else
                if (CurrentScore.CurrentValue >= StarValue1.MinRange && CurrentScore.CurrentValue <= StarValue1.MaxRange)
                {
                    CurrentStar.CurrentValue = 1;
                }
                else
                if (CurrentScore.CurrentValue >= StarValue2.MinRange && CurrentScore.CurrentValue <= StarValue2.MaxRange)
                {
                    CurrentStar.CurrentValue = 2;
                }
                else
                if (CurrentScore.CurrentValue >= StarValue3.MinRange && CurrentScore.CurrentValue <= StarValue3.MaxRange)
                {
                    CurrentStar.CurrentValue = 3;
                }
            }
        }
    }
}
