using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    Transform player;
    Text interactText;
    public float interactDistance = 3f;
    bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (interactText == null)
        {
            interactText = GameObject.FindGameObjectWithTag("interactButton").GetComponent<Text>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < interactDistance
            && !pressed)
        {
            interactText.enabled = true;
        }
        else
        {
            interactText.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.I) && interactText.enabled)
        {
            interactText.enabled = false;
            pressed = true;
        }

    }
}
