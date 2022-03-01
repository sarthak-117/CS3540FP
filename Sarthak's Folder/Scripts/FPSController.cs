using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 input, moveDirection;
    public float moveSpeed = 10f;
    CharacterController _controller;
    public float boostSpeed = 50f;
    public float boostHeight = 10f;
    public float gravity = 9.81f;
    public float jumpHeight = 10f;
    bool jetpackUsed = false;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
    }

    void CharacterMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        input *= moveSpeed;

        //Debug.Log(_controller.isGrounded);

        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                input *= boostSpeed;
            }
            moveDirection = input;
            jetpackUsed = false;
            if (Input.GetButton("Jump"))
            {
                // jump = 1/2(V/g)
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
                //Debug.Log("jump");
                //Debug.Log("Y" + moveDirection.y);
            }
            else
            {
               moveDirection.y = 0.0f;
            }
        }
        else
        {
            //mid-air controls
            if (Input.GetKeyDown(KeyCode.C))
            {
                input *= boostSpeed;
                if (!jetpackUsed)
                {
                    moveDirection = Vector3.Lerp(moveDirection, moveDirection + Vector3.up * boostHeight, Time.deltaTime * boostSpeed);
                    jetpackUsed = true;
                }
            }
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, Time.deltaTime);

            // works like a jetpack, need to find a way to set a boost limit
            
        }



        moveDirection.y -= gravity * Time.deltaTime;

        _controller.Move(moveDirection * Time.deltaTime);
        //transform.Translate(input);
    }
}
