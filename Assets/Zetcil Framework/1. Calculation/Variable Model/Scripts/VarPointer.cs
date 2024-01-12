/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk melakukan konversi nilai mouse/touch pada koordinat 3D
 **************************************************************************************************************/

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class VarPointer : MonoBehaviour
    {
        public enum CPointerType { PointerDown, PointerPress, PointerUp }

        [Space(10)]
        public bool isEnabled;
        public CPointerType PointerType;
        [ConditionalField("isEnabled")] public Camera TargetCamera;

        [Header("Variables Settings")]
        public bool usingVector2;
        [ConditionalField("usingVector2")] public VarVector2 VarVector2D;
        [ConditionalField("usingVector2")] public VarFloat Vector2X;
        [ConditionalField("usingVector2")] public VarFloat Vector2Y;

        [Space(10)]
        public bool usingEventVector2;
        public UnityEvent EventVector2;

        [Space(10)]
        public bool usingVector3;
        [ConditionalField("usingVector3")] public VarVector3 VarVector3D;
        [ConditionalField("usingVector3")] public VarFloat Vector3X;
        [ConditionalField("usingVector3")] public VarFloat Vector3Y;
        [ConditionalField("usingVector3")] public VarFloat Vector3Z;

        [Space(10)]
        public bool usingEventVector3;
        public UnityEvent EventVector3;


        [System.Serializable]
        public class CPointerController
        {
            public bool isManualRotate;
            [ConditionalField("isManualRotate")] public KeyCode MouseLookButton = KeyCode.Mouse1;
            [ConditionalField("isManualRotate")] public float LookSpeed = 4f;
            [ConditionalField("isEnabled")] public GameObject TargetObject;
        }

        [System.Serializable]
        public class CRaycastController
        {
            public bool isRaycast;
            [ConditionalField("isRaycast")] public VarObject HitObject;
        }

        [Space(10)]
        public bool usingPointerRotation;
        [ConditionalField("usingPointerRotation")] public CPointerController PointerController;

        [Space(10)]
        public bool usingRaycastController;
        [ConditionalField("usingRaycastController")] public CRaycastController RaycastController;

        [Header("Readonly Value")]
        public bool isDebugReadOnly;
        [ReadOnly] public Vector2 Pointer2D;
        [ReadOnly] public Vector3 Pointer3D;

        [HideInInspector]
        public Vector2 CurrentPointerPosition2D;
        [HideInInspector]
        public Vector3 CurrentPointerPosition3D;
        [HideInInspector]
        public Vector3 CurrentScreenPosition3D;

        Quaternion targetRotation;
        float targetRotationY;
        float targetRotationX;

        public float speedH = 2.0f;
        public float speedV = 2.0f;

        // Use this for initialization
        void Start()
        {

        }

        public void ExecutePointer()
        {

            CurrentPointerPosition2D.x = Input.mousePosition.x;
            CurrentPointerPosition2D.y = Input.mousePosition.y;
            CurrentScreenPosition3D = TargetCamera.ScreenToWorldPoint(CurrentPointerPosition2D);

            //-- cek tabrakan dengan objeck 2d
            Ray ray = TargetCamera.ScreenPointToRay(CurrentPointerPosition2D);
            RaycastHit2D raycastHit2D = Physics2D.GetRayIntersection(ray);

            if (raycastHit2D.collider != null)
            {
                CurrentPointerPosition2D = raycastHit2D.point;
                VarVector2D.transform.position = raycastHit2D.point;
                if (usingEventVector2)
                {
                    EventVector2.Invoke();
                }
                if (usingRaycastController)
                {
                    RaycastController.HitObject.CurrentValue = raycastHit2D.collider.gameObject;
                }
            }
            else
            {
                //-- cek tabrakan dengan objeck 3d
                ray = TargetCamera.ScreenPointToRay(CurrentPointerPosition2D);
                RaycastHit raycastHit3D;

                if (Physics.Raycast(ray, out raycastHit3D))
                {
                    CurrentPointerPosition3D = raycastHit3D.point;
                    VarVector3D.transform.position = raycastHit3D.point;
                    if (usingEventVector3)
                    {
                        EventVector3.Invoke();
                    }
                    if (usingRaycastController)
                    {
                        RaycastController.HitObject.CurrentValue = raycastHit3D.collider.gameObject;
                    }
                }
            }

            if (usingVector2)
            {
                VarVector2D.CurrentValue = CurrentPointerPosition2D;
                Vector2X.CurrentValue = CurrentPointerPosition2D.x;
                Vector2Y.CurrentValue = CurrentPointerPosition2D.y;
            }

            if (usingVector3)
            {
                VarVector3D.CurrentValue = CurrentPointerPosition3D;
                Vector3X.CurrentValue = CurrentPointerPosition3D.x;
                Vector3Y.CurrentValue = CurrentPointerPosition3D.y;
                Vector3Z.CurrentValue = CurrentPointerPosition3D.z;
            }

            if (usingPointerRotation)
            {
                if (!PointerController.isManualRotate || Input.GetKey(PointerController.MouseLookButton))
                {
                    targetRotationX += Input.GetAxis("Mouse X") * PointerController.LookSpeed;
                    targetRotationY -= Input.GetAxis("Mouse Y") * PointerController.LookSpeed;
                    targetRotation = Quaternion.Euler(targetRotationY, targetRotationX, 0.0f);
                    PointerController.TargetObject.transform.rotation = targetRotation;
                }
            }

            if (isDebugReadOnly)
            {
                Pointer2D = CurrentPointerPosition2D;
                Pointer3D = CurrentPointerPosition3D;
            }

        }


    // Update is called once per frame
    void Update()
        {
            if (isEnabled)
            {

                if (PointerType == CPointerType.PointerUp) {
                    if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Mouse2) || Input.touchCount > 0)
                    {
                        ExecutePointer();
                    }
                }
                if (PointerType == CPointerType.PointerDown)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse2) || Input.touchCount > 0)
                    {
                        ExecutePointer();
                    }
                }
                if (PointerType == CPointerType.PointerPress)
                {
                    if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse2) || Input.touchCount > 0)
                    {
                        ExecutePointer();
                    }
                }
            }
        }

        public Vector2 GetCurrentPointerPosition2D()
        {
            return CurrentPointerPosition2D;
        }

        public Vector3 GetCurrentPointerPosition3D()
        {
            return CurrentPointerPosition3D;
        }

        public Vector3 GetCurrentScreenPosition3D()
        {
            return CurrentScreenPosition3D;
        }

    }
}
