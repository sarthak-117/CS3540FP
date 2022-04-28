using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public BossFightManager bfm;
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0 && currentHealth <= 100 && (!LevelManager.isGameOver && !BossFightManager.isGameOver))
        {
            currentHealth -= damageAmount;
            Debug.Log("health: " + currentHealth);
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100);
        }

        if (currentHealth <= 0)
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
        if (bfm == null)
        {
            FindObjectOfType<LevelManager>().LevelLost();
        }
        else
        {
            FindObjectOfType<BossFightManager>().LevelLost();
        }        
    }
}



