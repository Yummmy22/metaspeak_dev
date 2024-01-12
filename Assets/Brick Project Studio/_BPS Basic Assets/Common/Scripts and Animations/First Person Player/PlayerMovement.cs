using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SojaExiles

{
    public class PlayerMovement : MonoBehaviour
    {
        
        public CharacterController controller;

        public float speed = 5f;
        public float gravity = -15f;

        public GameObject skin;

        Vector3 velocity;

        bool isGrounded;

        // Update is called once per frame
        void Update()
        {

            float x = Input.GetAxis("Vertical");
            float z = Input.GetAxis("Horizontal");

            Vector3 move = transform.right * x + transform.forward * -z;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            if (move != new Vector3(0,0,0) ) {
                skin.transform.rotation = Quaternion.Slerp(skin.transform.rotation, Quaternion.Euler(0, Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg, 0), 6 * Time.deltaTime);
            }

            controller.Move(velocity * Time.deltaTime);

        }

    }
}