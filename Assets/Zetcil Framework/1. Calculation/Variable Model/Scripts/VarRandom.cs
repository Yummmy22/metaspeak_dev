using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class VarRandom : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Random Settings")]
        public int RandomMin;
        public int RandomMax;

        [Header("Result Settings")]
        public VarInteger RandomResult;

        // Start is called before the first frame update
        public void ExecuteRandom()
        {
            RandomResult.CurrentValue = Random.Range(RandomMin, RandomMax);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
