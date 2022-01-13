using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    public Joystick MoveJoystick { get { return GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>(); } private set { } }
    public Joystick ShootJoystick { get; private set; }

    private void Awake()
    {
        ShootJoystick = GameObject.Find("ShootJoystick")?.GetComponent<FixedJoystick>();
    }
}
