using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public Transform player;
    public Text interactText;
    public float interactDistance = 3f;
    bool pressed;
    GameObject bossControl;
   // public Canvas interact;
    bool flip;

    Canvas current;
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        flip = false;
        bossControl = GameObject.FindGameObjectWithTag("BossControl");

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
            flip = false;
        }

        if (!flip && Vector3.Distance(player.position, transform.position) > interactDistance)
        {
            interactText.enabled = false;
            flip = true;
        }


        if (Input.GetKeyDown(KeyCode.I) && Vector3.Distance(player.position, transform.position) < interactDistance)
        {
            interactText.enabled = false;
            pressed = true;
           
            BossFightManager.switchesLeft--;
            FindObjectOfType<BossFightManager>().UpdateSwitches();
            StartCoroutine(bossControl.GetComponent<BossController>().nextLine());
        }
    }
}
