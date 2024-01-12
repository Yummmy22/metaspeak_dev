/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script Hello World
 **************************************************************************************************************/

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil {
    public class UnityHello : MonoBehaviour {

    public bool isEnabled;
    [ConditionalField("isEnabled")] public string helloWorld = "Hello World";

    // Use this for initialization
    void Awake()
    {
        Debug.Log("Awake: " + helloWorld);
    }

    void Start () {
        Debug.Log("Start: " + helloWorld);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Update: " + helloWorld);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Fixed Update: " + helloWorld);
    }

    void LateUpdate()
    {
        Debug.Log("Late Update: " + helloWorld);
    }

    public void ShowHelloUnity()
    {
        Debug.Log("Event: " + helloWorld);
    }
}
}
