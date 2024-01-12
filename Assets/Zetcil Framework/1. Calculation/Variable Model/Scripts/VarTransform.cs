using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarTransform : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;

        [Header("Transform Settings")]
        public Transform TargetTransform;

        [Header("Position Settings")]
        public bool usingPosition;
        [ConditionalField("usingPosition")] public VarVector3 Position;

        [Header("Rotation Settings")]
        public bool usingRotation;
        [ConditionalField("usingRotation")] public VarVector3 Rotation;

        [Header("Scale Settings")]
        public bool usingScale;
        [ConditionalField("usingScale")] public VarVector3 Scale;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (usingPosition)
                {
                    Position.CurrentValue.x = TargetTransform.position.x;
                    Position.CurrentValue.y = TargetTransform.position.y;
                    Position.CurrentValue.z = TargetTransform.position.z;
                }
                if (usingRotation)
                {
                    Rotation.CurrentValue.x = TargetTransform.localEulerAngles.x;
                    Rotation.CurrentValue.y = TargetTransform.localEulerAngles.y;
                    Rotation.CurrentValue.z = TargetTransform.localEulerAngles.z;
                }
                if (usingScale)
                {
                    Scale.CurrentValue.x = TargetTransform.localScale.x;
                    Scale.CurrentValue.y = TargetTransform.localScale.y;
                    Scale.CurrentValue.z = TargetTransform.localScale.z;
                }
            }
        }
    }
}

