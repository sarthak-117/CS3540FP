using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonMouseLook : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Walking,
        Jumping,
        Dashing
    }
    FSMStates currState;
    Vector3 input, moveDirection;
    public float moveSpeed = 10f;
    CharacterController _controller;
    Animator _anim;
    public float boostSpeed = 50f;
    public float boostHeight = 10f;
    public float gravity = 9.81f;
    public float jumpHeight = 10f;
    bool jetpackUsed;
    public bool jetpackEnabled = true;
    bool movementSet;
    public GameObject boostSFX;
    public AudioClip jumpSFX;
    public AudioClip dashSFX;
    public int deathTimeOut = 10;
    float timeElapsed = 0;

    public float maxFuel = 2f;
    public float thrustForce = 0.5f;
    public ParticleSystem effect;
    public float currFuel;
    public Slider fuelSlider;
    float currThrust;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        //_anim.SetInteger("state", 0);
        currState = FSMStates.Idle;
        currFuel = maxFuel;
        currThrust = thrustForce;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!LevelManager.isGameOver)
        {
            CharacterMovement();
        }
        else if (transform.position.y < 0)
        {
            _anim.SetInteger("state", 4);
        }
        else if (GetComponent<PlayerHealth>().currentHealth > 0)
        {
            _anim.SetInteger("state", 0);
        }
        else
        {
            _anim.SetInteger("state", 4);
        }
        Debug.Log(_controller.isGrounded);
        if (jetpackEnabled)
        {
            //Debug.Log(currFuel);
            fuelSlider.value = currFuel;
        }
    }

    void CharacterMovement()
    {
        movementSet = false;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        input *= moveSpeed;

        //Debug.Log(_controller.isGrounded);

        if (_controller.isGrounded)
        {
            jetpackUsed = false;
            currThrust = thrustForce;
            if (Input.GetKey(KeyCode.LeftShift) && jetpackEnabled && currFuel > 0)
            {
                input += Vector3.up * (gravity * Time.deltaTime + 0.5f);
                AudioSource.PlayClipAtPoint(dashSFX, Camera.main.transform.position);
                jetpackUsed = true;
                currFuel -= Time.deltaTime;
                _anim.SetInteger("state", 3);
                movementSet = true;
                Vector3 posn = transform.position;
                GameObject boost = Instantiate(boostSFX, posn, transform.rotation);
                boost.transform.parent = gameObject.transform;
                boost.transform.Rotate(new Vector3(0, 180, 0));
                Destroy(boost, 1);
            }
            moveDirection = input;

            if (Input.GetButton("Jump"))
            {
                // jump = 1/2(V/g)
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
                //Debug.Log("jump");
                //Debug.Log("Y" + moveDirection.y);
                _anim.SetInteger("state", 2);
                currState = FSMStates.Jumping;
                Debug.Log(currState);
                movementSet = true;
                AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position);
            }
            else if (!jetpackUsed || !jetpackEnabled)
            {
                moveDirection.y = -1 * gravity * Time.deltaTime;
            }
            

            if (currFuel < maxFuel && !jetpackUsed && jetpackEnabled)
            {
                currFuel += Time.deltaTime / 2;
                Debug.Log("Fueling");
            }
        }
        else
        {
            //mid-air controls
            if (Input.GetKey(KeyCode.LeftShift) && jetpackEnabled && currFuel > 0)
            {
                jetpackUsed = true;
                //input += Vector3.up * currThrust;
                moveDirection.y += (gravity * Time.deltaTime + thrustForce);
                currThrust += 0.2f;
                AudioSource.PlayClipAtPoint(dashSFX, Camera.main.transform.position);
                currFuel -= Time.deltaTime;
                _anim.SetInteger("state", 3);
                movementSet = true;
                Vector3 posn = transform.position;
                GameObject boost = Instantiate(boostSFX, posn, transform.rotation);
                boost.transform.parent = gameObject.transform;
                boost.transform.Rotate(new Vector3(0, 180, 0));
                Destroy(boost, 1);
            }
            
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, Time.deltaTime);

            // works like a jetpack, need to find a way to set a boost limit
        }

        // Debug.Log(movementSet);

        moveDirection.y -= gravity * Time.deltaTime;
        if (transform.position.y < 0)
        {
            FindObjectOfType<LevelManager>().LevelLost();
        }
        if (input.magnitude > 0.01f)
        {
            float cameraYawRotation = Camera.main.transform.eulerAngles.y;
            Quaternion newRotation = Quaternion.Euler(0f, cameraYawRotation, 0.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10);
        }

        if (moveDirection.x == 0.0f & moveDirection.z == 0.0f & !movementSet & _controller.isGrounded)
        {
            _anim.SetInteger("state", 0);
            currState = FSMStates.Idle;
            movementSet = true;
        }
        else if (!movementSet && _controller.isGrounded)
        {
            _anim.SetInteger("state", 1);
            currState = FSMStates.Walking;
        }
        _controller.Move(moveDirection * Time.deltaTime);
        //Debug.Log(timeElapsed);
        if (timeElapsed > deathTimeOut)
        {
            _anim.SetInteger("state", 4);

            FindObjectOfType<LevelManager>().LevelLost();
        }
        //transform.Translate(input);
    }
}
