using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class VarPivot : MonoBehaviour
    {
        public enum CAnchorType { PositionOnly, RotationOnly, PositionAndRotation }
        
        [Space(10)]
        public bool isEnabled;

        [Header("Target Object")]
        public CAnchorType AnchorType;
        public GameObject TargetObject;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (AnchorType == CAnchorType.PositionOnly)
                {
                    TargetObject.transform.position = this.transform.position;
                }
                if (AnchorType == CAnchorType.RotationOnly)
                {
                    TargetObject.transform.rotation = this.transform.rotation;
                }
                if (AnchorType == CAnchorType.PositionAndRotation)
                {
                    TargetObject.transform.position = this.transform.position;
                    TargetObject.transform.rotation = this.transform.rotation;
                }
            }
        }
    }
}
