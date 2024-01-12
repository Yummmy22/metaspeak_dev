using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

public class VarKey : MonoBehaviour
{

    [Space(10)]
    public bool isEnabled;

    // Use this for initialization
    void Start()
    {
        string[] names = Input.GetJoystickNames();
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].Length > 0)
            {
                Debug.Log("Joystick" + (i + 1) + " = " + names[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            Debug.Log("Joystick Horizontal: " + Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            Debug.Log("Joystick Vertical: " + Input.GetAxis("Vertical"));
        }
        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            Debug.Log("JoystickButtonPressed: " + KeyCode.Joystick1Button0);
        }
        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            Debug.Log("JoystickButtonPressed: " + KeyCode.Joystick1Button1);
        }
        if (Input.GetKey(KeyCode.Joystick1Button2))
        {
            Debug.Log("JoystickButtonPressed: " + KeyCode.Joystick1Button2);
        }
        if (Input.GetKey(KeyCode.Joystick1Button3))
        {
            Debug.Log("JoystickButtonPressed: " + KeyCode.Joystick1Button3);
        }
        if (Input.GetKey(KeyCode.Joystick1Button4))
        {
            Debug.Log("JoystickButtonPressed: " + KeyCode.Joystick1Button4);
        }
        if (Input.GetKey(KeyCode.Joystick1Button5))
        {
            Debug.Log("JoystickButtonPressed: " + KeyCode.Joystick1Button5);
        }
        if (Input.GetKey(KeyCode.Joystick1Button6))
        {
            Debug.Log("JoystickButtonPressed: " + KeyCode.Joystick1Button6);
        }
        if (Input.GetKey(KeyCode.Joystick1Button7))
        {
            Debug.Log("JoystickButtonPressed: " + KeyCode.Joystick1Button7);
        }
        if (Input.GetKey(KeyCode.Joystick1Button8))
        {
            Debug.Log("JoystickButtonPressed: " + KeyCode.Joystick1Button8);
        }
    }

    void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            Debug.Log("KeyPressed: " + Event.current.keyCode);
        }
    }
}
