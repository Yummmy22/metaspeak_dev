using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class VarPlay : MonoBehaviour
    {

        public enum CMovementType { GlobalTransform, LocalTransform }

        [System.Serializable]
        public class CKeyboardKey
        {
            [SearchableEnum] public KeyCode UpKey = KeyCode.UpArrow;
            [SearchableEnum] public KeyCode LeftKey = KeyCode.LeftArrow;
            [SearchableEnum] public KeyCode DownKey = KeyCode.DownArrow;
            [SearchableEnum] public KeyCode RightKey = KeyCode.RightArrow;
            [SearchableEnum] public KeyCode JumpKey;
            [SearchableEnum] public KeyCode RotateLeft = KeyCode.None;
            [SearchableEnum] public KeyCode RotateRight = KeyCode.None;
            [SearchableEnum] public KeyCode ShiftKey;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("Character Setting")]
        public CharacterController TargetController;
        public CMovementType MovementType;

        [Header("Movement Setting")]
        public float MoveSpeed = 20;
        public float jumpSpeed = 8.0F;
        public float RotateSpeed = 5.0F;
        public float gravity = 20.0F;
        Vector3 moveDirection;

        [Header("Keyboard Setting")]
        public CKeyboardKey KeyboardKey;
        public CKeyboardKey AltKeyboardKey;

        GameObject MainCamera;

        // Start is called before the first frame update
        void Start()
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
        }

        // Update is called once per frame
        void Update()
        {

            if (TargetController.isGrounded)
            {
                moveDirection = Vector3.zero;

                if (Input.GetKey(KeyboardKey.UpKey) || Input.GetKey(AltKeyboardKey.UpKey))
                {
                    if (MovementType == CMovementType.GlobalTransform)
                    {
                        moveDirection = Vector3.forward;
                    }
                    if (MovementType == CMovementType.LocalTransform)
                    {
                        moveDirection = MainCamera.transform.forward;
                    }
                    moveDirection = TargetController.transform.TransformDirection(moveDirection);
                    moveDirection *= MoveSpeed;
                }
                else if (Input.GetKeyUp(KeyboardKey.UpKey) || Input.GetKey(AltKeyboardKey.UpKey))
                {
                    moveDirection = Vector3.zero;
                }

                if (Input.GetKey(KeyboardKey.DownKey) || Input.GetKey(AltKeyboardKey.DownKey))
                {
                    if (MovementType == CMovementType.GlobalTransform)
                    {
                        moveDirection = Vector3.back;
                    }
                    if (MovementType == CMovementType.LocalTransform)
                    {
                        moveDirection = -MainCamera.transform.forward;
                    }
                    moveDirection = TargetController.transform.TransformDirection(moveDirection);
                    moveDirection *= MoveSpeed;
                }
                else if (Input.GetKeyUp(KeyboardKey.DownKey) || Input.GetKey(AltKeyboardKey.DownKey))
                {
                    moveDirection = Vector3.zero;
                }

                if (Input.GetKey(KeyboardKey.LeftKey) || Input.GetKey(AltKeyboardKey.LeftKey))
                {
                    if (MovementType == CMovementType.GlobalTransform)
                    {
                        moveDirection = Vector3.left;
                    }
                    if (MovementType == CMovementType.LocalTransform)
                    {
                        moveDirection = -MainCamera.transform.right;
                    }
                    moveDirection = TargetController.transform.TransformDirection(moveDirection);
                    moveDirection *= MoveSpeed;
                }
                else if (Input.GetKeyUp(KeyboardKey.LeftKey) || Input.GetKey(AltKeyboardKey.LeftKey))
                {
                    moveDirection = Vector3.zero;
                }

                if (Input.GetKey(KeyboardKey.RightKey) || Input.GetKey(AltKeyboardKey.RightKey))
                {
                    if (MovementType == CMovementType.GlobalTransform)
                    {
                        moveDirection = Vector3.right;
                    }
                    if (MovementType == CMovementType.LocalTransform)
                    {
                        moveDirection = MainCamera.transform.right;
                    }
                    moveDirection = TargetController.transform.TransformDirection(moveDirection);
                    moveDirection *= MoveSpeed;
                }
                else if (Input.GetKeyUp(KeyboardKey.RightKey) || Input.GetKey(AltKeyboardKey.RightKey))
                {
                    moveDirection = Vector3.zero;
                }

                if (Input.GetKey(KeyboardKey.RotateLeft) || Input.GetKey(AltKeyboardKey.RotateLeft))
                {
                    TargetController.transform.Rotate(0, -1 * RotateSpeed, 0);
                }
                else if (Input.GetKeyUp(KeyboardKey.RotateLeft) || Input.GetKey(AltKeyboardKey.RotateLeft))
                {
                    moveDirection = Vector3.zero;
                }

                if (Input.GetKey(KeyboardKey.RotateRight) || Input.GetKey(AltKeyboardKey.RotateRight))
                {
                    TargetController.transform.Rotate(0, 1 * RotateSpeed, 0);
                }
                else if (Input.GetKeyUp(KeyboardKey.RotateRight) || Input.GetKey(AltKeyboardKey.RotateRight))
                {
                    moveDirection = Vector3.zero;
                }

                if (Input.GetKey(KeyboardKey.JumpKey) || Input.GetKey(AltKeyboardKey.JumpKey))
                    moveDirection.y = jumpSpeed;

            }
            moveDirection.y -= gravity * Time.deltaTime;
            TargetController.Move(moveDirection * Time.deltaTime);

        }
    }
}
