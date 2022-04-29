using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookTopDown : MonoBehaviour
{

    // Start is called before the first frame update
    public float acceleration = 100f;
    Transform playerBody;
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = transform.parent.transform;
        //cameraOffset = transform.localRotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            MouseMovement();
        }
    }

    void MouseMovement()
    {
        float moveX = Input.GetAxis("Mouse X") * acceleration * Time.deltaTime;
        playerBody.Rotate(Vector3.up * moveX);
        transform.localRotation = Quaternion.Euler(50, moveX, 0);
    }
}
