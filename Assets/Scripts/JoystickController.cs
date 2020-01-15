using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public GameObject JoystickTouch;
    Vector3 targetVector;
    public DemonController demon;

    void Start()
    {
        JoystickTouch.transform.position = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPos = Input.mousePosition;
            targetVector = touchPos - transform.position;

            if (targetVector.magnitude < 100)
            {
                JoystickTouch.transform.position = touchPos;
                demon.targetMove = targetVector;
            }
            else
            {
                JoystickTouch.transform.position = transform.position + targetVector.normalized * 100;
                demon.targetMove = targetVector.normalized * 100;
            }
        }
        else
        {
            JoystickTouch.transform.position = transform.position;
            demon.targetMove = new Vector3(0, 0, 0);
        }
    }
}
