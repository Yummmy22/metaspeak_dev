/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk membuat gerakan pingpong bolak-balik
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class UnityPingPong : MonoBehaviour
    {
        public bool isEnabled;
        [ConditionalField("isEnabled")] public GameObject TargetObject;
        [ConditionalField("isEnabled")] public float PingPongSpeed;

        [Header("Ping Pong Settings")]
        public bool usingPingPongPosition;
        [ConditionalField("usingPingPongPosition")] public Vector3 PingPongPosition;

        public bool usingPingPongRotation;
        [ConditionalField("usingPingPongRotation")] public Vector3 PingPongRotation;

        public bool usingPingPongScale;
        [ConditionalField("usingPingPongScale")] public Vector3 PingPongScale;

        Vector3 StartPosition;
        Vector3 EndPosition;

        Vector3 StartRotation;
        Vector3 EndRotation;

        Vector3 StartScale;
        Vector3 EndScale;

        void InvokePingPongPosition()
        {
            float pingPong = Mathf.PingPong(Time.time * PingPongSpeed, 1);
            EndPosition = StartPosition + PingPongPosition;
            TargetObject.transform.position = Vector3.Lerp(StartPosition, EndPosition, pingPong);
        }

        void InvokePingPongRotation()
        {
            float pingPong = Mathf.PingPong(Time.time * PingPongSpeed, 1);
            EndRotation = StartRotation + PingPongRotation;
            TargetObject.transform.rotation = Quaternion.Euler(Vector3.Lerp(StartRotation, EndRotation, pingPong));
        }

        void InvokePingPongScale()
        {
            float pingPong = Mathf.PingPong(Time.time * PingPongSpeed, 1);
            EndScale = StartScale + PingPongScale;
            TargetObject.transform.localScale = Vector3.Lerp(StartScale, EndScale, pingPong);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                if (TargetObject == null)
                {
                    TargetObject = this.gameObject;
                }
                StartPosition = TargetObject.transform.position;
                StartRotation = TargetObject.transform.localRotation.eulerAngles;
                StartScale = TargetObject.transform.localScale;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (usingPingPongPosition)
                {
                    InvokePingPongPosition();
                }
                if (usingPingPongRotation)
                {
                    InvokePingPongRotation();
                }
                if (usingPingPongScale)
                {
                    InvokePingPongScale();
                }
            }
        }

    }
}
