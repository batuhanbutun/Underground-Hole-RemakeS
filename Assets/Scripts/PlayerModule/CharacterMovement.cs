using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Joystick myJoystick;
    [SerializeField] public float movementSpeed;
    private Vector3 moveDir;
    private Vector3 lookDir;
    private bool isActive = false;
    private bool isRunning = false;
    
    [SerializeField] private float positionLimitValueZ;
    [SerializeField] private float positionLimitValueZNegative;
    [SerializeField] private float positionLimitValueX;
    [SerializeField] private float positionLimitValueXNegative;

    [SerializeField] private Animator myAnimator;
    private float moveSpeedAnimMultiplier = 1;
    private void Start()
    {
        GameManager.toPlayerControl += MovementActivate;
        GameManager.toHoleControl += MovementCancel;
        isActive = false;
    }
    
    private void Update()
    {
        if (isActive)
        {
            Movement();
            AnimationControl();
        }
    }

    private void Movement()
    {
        moveDir = new Vector3(myJoystick.Horizontal, 0, myJoystick.Vertical);
        lookDir = new Vector3(transform.position.x + myJoystick.Horizontal, transform.position.y,
            transform.position.z + myJoystick.Vertical);
        transform.LookAt(lookDir);
        //MovementLimitControl();
        transform.position += moveDir * Time.deltaTime * movementSpeed;
    }

    private void AnimationControl()
    {
        if (moveDir.x == 0 && moveDir.z == 0)
        {
            if (isRunning)
            {
                isRunning = false;
                myAnimator.SetBool("isRunning",false);
            }
        }
        else 
        {
            if (!isRunning)
            {
                isRunning = true;
                myAnimator.SetBool("isRunning",true); 
            }
        }
    }
    
    private void MovementCancel()
    {
        if (this != null)
        {
            moveDir = Vector3.zero;
            isActive = false;
            myAnimator.SetBool("isRunning",false);
            transform.position = new Vector3(-1.32948649f, transform.position.y, -3.50835443f);
        }
    }

    private void MovementActivate()
    {
        if (this != null)
        {
            StartCoroutine(ActiveMovementDelay());
        }
    }

    IEnumerator ActiveMovementDelay()
    {
        yield return new WaitForSeconds(1f);
        isActive = true;
        isRunning = false;
    }

    public void Cheer()
    {
        myAnimator.SetTrigger("cheer");
    }

    public void IncreaseSpeed()
    {
        movementSpeed += 0.25f;
        moveSpeedAnimMultiplier += 0.1f;
        myAnimator.SetFloat("moveSpeed",moveSpeedAnimMultiplier);
    }

}
