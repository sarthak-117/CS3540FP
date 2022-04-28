using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public GameObject smallSmoke;
    public GameObject bigSmoke;

    GameObject boss;
    string[] lines = {"You're Back", "Don't try to stop us", "A fleshy human is no\nmatch for our perfection", 
        "Only one switch?\nYou don't stand a chance", "We will destroy you", "STOP DOING THAT", "ARGgdjfl(*DHC*DU@#$%^&*\n!@#$%^&*(*%$#@#$%^&%^&*()(*&^^&*()(*@*!#@"};
    Text textField;
    bool start;
    bool smallSmokeInst;
    string name = "The Architect: ";
    int currentLine;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        textField =  GameObject.FindGameObjectWithTag("BossLines").GetComponent<Text>();
        start = true;
        smallSmokeInst = false;
        currentLine = 3;
    }

    // Update is called once per frame
    void Update()
    {

        if(currentLine == 7)
        {
            Destroy(gameObject);
            Destroy(boss, 3);
        }
    }

    private void OnDestroy()
    {
        Instantiate(bigSmoke, boss.transform.position - boss.transform.up, boss.transform.rotation);
    }


    //For entering the room and starting the first lines
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Player") && start)
        {
            StartCoroutine(firstLines());
            start = false;
        }
    }

    IEnumerator firstLines()  //  <-  its a standalone method
    {
        textField.enabled = true;
        textField.text = name + lines[0];
        yield return new WaitForSeconds(3);
        textField.text = name + lines[1];
        yield return new WaitForSeconds(3);
        textField.text = name + lines[2];
        yield return new WaitForSeconds(4);
        textField.enabled = false;
    }

    public IEnumerator nextLine()
    {
        if (currentLine == 5 && !smallSmokeInst)
        {
            Instantiate(smallSmoke, boss.transform.position, boss.transform.rotation);
            smallSmokeInst = true;
        }
        textField.enabled = true;
        textField.text = name + lines[currentLine];
        yield return new WaitForSeconds(4);
        textField.enabled = false;
        currentLine++;
    }
}
