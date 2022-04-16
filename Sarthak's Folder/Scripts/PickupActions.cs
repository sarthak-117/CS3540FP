using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupActions : MonoBehaviour
{
    // Start is called before the first frame update
    public int lootValue = 1;
    //float doubleLootValue;
    public static int pickupCount = 0;
    public AudioClip pickupsfx;

    void Start()
    {
        pickupCount++;
        //doubleLootValue *= lootValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver)
        {
            pickupCount = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit object with " + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other) //used for kinematic rigidbody
    {
        //Debug.Log("I got triggered by " + other.gameObject.name);
        if (!LevelManager.isGameOver)
        {

            if (other.gameObject.CompareTag("Player"))
            {
                pickupCount--;
                AudioSource.PlayClipAtPoint(pickupsfx, Camera.main.transform.position);

                Destroy(gameObject);
                //Debug.Log("after:" + pickupCount);
                LevelManager.playerScore += lootValue;
                if (pickupCount <= 0)
                {
                    FindObjectOfType<LevelManager>().LevelBeat();
                   // Debug.Log("You Win!");
                }
            }

        }
    }
}