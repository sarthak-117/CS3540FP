using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    Text interactText;
    Text speech;
    public float interactDistance = 3f;
    bool talking = false;
    bool doneTalking = false;
    public string speechText;
    string[] phrases;
    Image background;
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (interactText == null)
        {
            interactText = GameObject.FindGameObjectWithTag("interact").GetComponent<Text>();

        }

        if (speech == null)
        {
            speech = GameObject.FindGameObjectWithTag("Speech").GetComponent<Text>();

        }

        phrases = speechText.Split('.');
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < interactDistance
            && !talking)
        {
            interactText.enabled = true;
            background.enabled = true;
        }
        else
        {
            interactText.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.I) && !talking && interactText.enabled)
        {
            talking = true;
            speech.enabled = true;
            interactText.enabled = false;
            StartCoroutine(Speaking());
        }
    }

    IEnumerator Speaking()  //  <-  its a standalone method
    {
        foreach (string s in phrases)
        {
            speech.text = s;
            yield return new WaitForSeconds(6);
        }
        speech.enabled = false;
        
        /*
        speech.text = "Computer: Oh hi... You're awake!";
        yield return new WaitForSeconds(3);
        speech.text = "Computer: I probably should report you, but its \n" +
            "so boring around here and I want to see what happens";
        yield return new WaitForSeconds(5);
        speech.text = "Computer: Now if I were you, I would avoid those other robots\n" +
            "at all costs. A boost pack could help you with that. Good luck";
        yield return new WaitForSeconds(7);
        speech.enabled = false;*/
    }
 }
