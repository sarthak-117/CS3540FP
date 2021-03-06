using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public float acceleration = 100f;
    float pitch = 0;
    //float yaw = 0;
    Transform playerBody;
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = transform.parent.transform;
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
        float moveY = Input.GetAxis("Mouse Y") * acceleration * Time.deltaTime;

        playerBody.Rotate(Vector3.up * moveX );
        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, 0, 15);
        

        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
