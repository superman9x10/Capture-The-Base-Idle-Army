using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : Character
{
    
    public static event Action<float> playerMove;

    [Header("Player Config")]
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] float movementSpeed;

    public float velocity;

    private void Update()
    {
        movement();
    }


    void movement()
    {
        float xDir = joystick.Horizontal;
        float zDir = joystick.Vertical;

        if (xDir != 0 || zDir != 0)
        {
            Vector3 moveDir = new Vector3(xDir, 0, zDir);
            velocity = Vector3.SqrMagnitude(moveDir);
            transform.position += moveDir * movementSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 15f * Time.deltaTime);
        } else
        {
            velocity = 0;
        }
    }
}
