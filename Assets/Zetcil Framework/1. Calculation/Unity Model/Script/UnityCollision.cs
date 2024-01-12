/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menunjukkan dasar-dasar deteksi tabrakan GameObject pada Unity
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

public class UnityCollision : MonoBehaviour
{
    public Rigidbody TargetRigidbody;
    public string TargetTag;

    public bool usingCollisionType;
    public UnityEvent CollisionEvent;

    public bool usingTriggerType;
    public UnityEvent TriggerEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == TargetTag)
        {
            CollisionEvent.Invoke();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == TargetTag)
        {
            TriggerEvent.Invoke();
        }
    }


}
