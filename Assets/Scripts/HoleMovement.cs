using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    [SerializeField] private Joystick myJoystick;
    [SerializeField] private GameObject myTimer;
    [SerializeField] public float movementSpeed;
    private Vector3 moveDir;
    private bool isActive = true;
    private bool isMoved = false;
    
    [SerializeField] private float positionLimitValueZ;
    [SerializeField] private float positionLimitValueZNegative;
    [SerializeField] private float positionLimitValueX;
    [SerializeField] private float positionLimitValueXNegative;

    private Vector3 startPos;
    private void Start()
    {
        GameManager.toPlayerControl += MovementCancel;
        startPos = transform.position;
    }

    private void Update()
    {
        Movement();
    }
    

    private void Movement()
    {
        if (isActive)
        {
            moveDir = new Vector3(myJoystick.Horizontal, 0, myJoystick.Vertical);
            MovementLimitControl();
            transform.position += moveDir * Time.deltaTime * movementSpeed;
        }

        if (moveDir.magnitude > 0 && !isMoved)
        {
            myTimer.SetActive(true);
            HoleManager.Instance.TimerStart();
            isMoved = true;
        }
    }

    private void MovementLimitControl()
    {
        if (transform.position.x >= positionLimitValueX && moveDir.x > 0)
            moveDir.x = 0;
        else if (transform.position.x <= positionLimitValueXNegative && moveDir.x < 0)
            moveDir.x = 0;
        if (transform.position.z >= positionLimitValueZ && moveDir.z > 0)
            moveDir.z = 0;
        else if (transform.position.z <= positionLimitValueZNegative && moveDir.z < 0)
            moveDir.z = 0;
    }

    private void MovementCancel()
    {
        if (this != null)
        {
            moveDir = Vector3.zero;
            isActive = false;
            isMoved = false;
            transform.position = startPos;
        }
    }

    public void MovementActivate()
    {
        isActive = true;
    }

    
}
