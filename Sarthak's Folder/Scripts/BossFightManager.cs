using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossFightManager : MonoBehaviour
{
    float countDown;
    public int levelDuration = 100;
    public Text scoreText;
    public Text timerText;
    public Text stateText;
    public static bool isGameOver = false;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    bool triggeredBoss;
    public static int switchesLeft = 4;
    List<GameObject> enemiesList;
    public GameObject chargingEnemy;
    public GameObject floatingEnemy;

    void Start()
    {
        isGameOver = false;
        countDown = levelDuration;
        
        // set countDown to level duration specified in the inspector
        countDown = levelDuration;
        
        // set up variables to indicate the bossfight state
        triggeredBoss = false;
        timerText.GetComponent<Text>().enabled = false;
        scoreText.GetComponent<Text>().enabled = false;

        // initialize the switches count to the number of switches in the level
        switchesLeft = 4;
        enemiesList = new List<GameObject>();
        LevelManager.isGameOver = false;
        //playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // decrement countDown value while it's not zero
        if (!isGameOver)
        {
            if (countDown > 0)
            {
                if (triggeredBoss)
                {
                    countDown -= Time.deltaTime;
                }
            }
            else
            {
                countDown = 0.0f;
                LevelLost();

            }
            // once boss is triggered, start the countdown
            if (triggeredBoss)
            {
                SetTimerText();
                SetScoreText();
            }            
        }
    }

    void SetTimerText()
    {
        /// <summary>method to set timerText to current countDown value</summary>
        timerText.text = countDown.ToString("0.00");
    }

    void SetScoreText()
    {
        /// <summary>method to set timerText to current countDown value</summary>
        scoreText.text =
            "Buttons left: " + switchesLeft;
    }

    void SetGameOverStatus(string gameTextMessage, AudioClip statusSFX)
    {
        /// <summary>method to set GameOver status</summary>
        /// 

        // set isGameOver 
        isGameOver = true;
        // update UI component with appropriate message and activate it
        // message is received as an argument
        stateText.text = gameTextMessage;
        stateText.GetComponent<Text>().enabled = true;
        scoreText.GetComponent<Text>().enabled = false;
        timerText.GetComponent<Text>().enabled = false;
        // Pause current background music
        FindObjectOfType<Camera>().GetComponent<AudioSource>().Stop();
        // play level status sfx
        AudioSource.PlayClipAtPoint(statusSFX, Camera.main.transform.position);
        // sfx clip is received as an argument

    }
    // Call this method when the player runs out of time or is dead.
    public void LevelLost()
    {
        // call SetGameOverStatus with "GAME OVER!"
        SetGameOverStatus("YOU LOSE!", loseSFX);
        // slow down background music
        //Camera.main.GetComponent<AudioSource>().pitch = 0.5f;
        Invoke("LoadCurrentLevel", 3);
        //Invoke("LoadNextLevel", 5);
    }
    // Used to reload the current level a player is on
    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // can use getActiveScene to get currently loaded scene, can be used to specify next scene via index through build
    }

    // Used to progress the player to the next level if they win using build index variables
    public void LoadNextLevel()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        //level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // can use getActiveScene to get currently loaded scene, can be used to specify next scene via index through build
    }

    public void LevelBeat()
    { 
        // call SetGameOverStatus with "YOU WIN!"
        SetGameOverStatus("YOU WIN!", winSFX);
        // speed up background music
        //Camera.main.GetComponent<AudioSource>().pitch = 2;
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            Invoke("LoadNextLevel", 5);
        }
    }

    public void StartBossBattle()
    {
        triggeredBoss = true;
        timerText.GetComponent<Text>().enabled = true;
        scoreText.GetComponent<Text>().enabled = true;
        UpdateSwitches();
    }

    // manages the final state of the boss fight by handling any changes to the remaining button count
    public void UpdateSwitches()
    {
        if (switchesLeft == 4)
        {            
            for (int i = 0; i < 5; i++)
            {
                GameObject enemy = Instantiate(chargingEnemy, new Vector3(-2.11f - i * 7, 0, 50.0f), Quaternion.identity);
                Debug.Log("Enemy made");
                if (i % 4 == 0)
                {
                    enemy.GetComponent<SmartEnemy>().WanderPointTag = "WanderPoint";
                    Debug.Log("Enemy target set");
                } 
                else
                {
                    enemy.GetComponent<SmartEnemy>().WanderPointTag = "WanderPointp" + i % 4;
                    Debug.Log("Enemy target set");
                }

                enemiesList.Add(enemy);
                Debug.Log("Enemy added");
            }

        } 
        else if (switchesLeft == 3)
        {
            foreach (GameObject enemy in enemiesList) {
                Destroy(enemy);
            }

            for (int i = 0; i < 7; i++)
            {
                GameObject enemy = Instantiate(chargingEnemy, new Vector3(-2.11f - i * 8, 0, 33.0f), Quaternion.identity);
                Debug.Log("Enemy made");
                if (i % 4 == 0)
                {
                    enemy.GetComponent<SmartEnemy>().WanderPointTag = "WanderPoint";
                    Debug.Log("Enemy target set");
                }
                else
                {
                    enemy.GetComponent<SmartEnemy>().WanderPointTag = "WanderPointp" + i % 4;
                    Debug.Log("Enemy target set");
                }

                enemiesList.Add(enemy);
                Debug.Log("Enemy added");
            }

            for (int i = 0; i < 5; i++)
            {
                GameObject enemy = Instantiate(floatingEnemy, new Vector3(-2.11f - i * 8, 8, 65.0f), Quaternion.identity);
                Debug.Log("Enemy made");
                
                enemiesList.Add(enemy);
                Debug.Log("Enemy added");
            }
        }
        else if (switchesLeft == 2)
        {
            foreach (GameObject enemy in enemiesList)
            {
                Destroy(enemy);
            }

            for (int i = 0; i < 5; i++)
            {
                GameObject enemy = Instantiate(chargingEnemy, new Vector3(-2.11f - i * 7, 0, 70.0f), Quaternion.identity);
                Debug.Log("Enemy made");
                if (i % 4 == 0)
                {
                    enemy.GetComponent<SmartEnemy>().WanderPointTag = "WanderPoint";
                    Debug.Log("Enemy target set");
                }
                else
                {
                    enemy.GetComponent<SmartEnemy>().WanderPointTag = "WanderPointp" + i % 4;
                    Debug.Log("Enemy target set");
                }

                enemiesList.Add(enemy);
                Debug.Log("Enemy added");
            }

            for (int i = 0; i < 10; i++)
            {
                GameObject enemy = Instantiate(floatingEnemy, new Vector3(-2.11f - i * 8, 8 + i * 2, 25.0f), Quaternion.identity);
                Debug.Log("Enemy made");


                enemiesList.Add(enemy);
                Debug.Log("Enemy added");
            }

        }
        else if (switchesLeft == 1)
        {
            foreach (GameObject enemy in enemiesList)
            {
                Destroy(enemy);
            }

            for (int i = 0; i < 15; i++)
            {
                GameObject enemy = Instantiate(floatingEnemy, new Vector3(-2.11f - i * 8, 8 + i * 2, 65.0f), Quaternion.identity);
                Debug.Log("Enemy made");
               
                enemiesList.Add(enemy);
                Debug.Log("Enemy added");
            }
        }
        else
        {
            LevelBeat();
        }
    }
}
