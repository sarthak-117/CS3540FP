using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    int currentHealth;
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("health: " + currentHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }
        else
        {
            PlayerDies();
        }
    }

    void PlayerDies()
    {
        //Debug.Log("Player dies");
        //Destroy(gameObject);
        Animator anim = GetComponent<Animator>();
        anim.SetInteger("state", 4);
        // game over using level manager here
    }
}
