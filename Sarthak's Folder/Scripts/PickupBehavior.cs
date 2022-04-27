using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.CompareTag("Player"))
        {
            Animator anim = gameObject.GetComponent<Animator>();
            anim.SetBool("Collected", true);
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 1.3f);
        }
    }
}
