using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")
                .GetComponent<Transform>();
        }

        // get and store the offset from the camera to the player
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        // add the offset to the player's position
        // assign it to the camera's position

        transform.position = player.position + offset;

    }
}
