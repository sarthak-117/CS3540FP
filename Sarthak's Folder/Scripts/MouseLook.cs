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
    private RaycastHit hit;
    private Vector3 cameraOffset;
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = transform.parent.transform;
        cameraOffset = transform.localPosition;
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
        //transform.Rotate(transform.right * moveY * acceleration);
        //yaw += moveX;
        //Debug.Log("cancer" + moveX + "Cancer 2" + moveY);
        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, 10, 25);
        

        transform.localRotation = Quaternion.Euler(pitch, 0, 0);

        
        //  else
        // {
        //transform.localPosition = cameraOffset;
        //  }
        // Gizmos.DrawLine(transform.position, playerBody.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        //transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        //ebug.Log("Changed");
    }

    private void OnCollisionExit(Collision other)
    {
        //transform.localPosition = cameraOffset;
    }
    
    void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, playerBody.position);
    }
    
}
