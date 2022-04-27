using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    float countDown;
    public int levelDuration = 100;
    int level = 0;
    public static int playerScore;
    public Text scoreText;
    public Text timerText;
    public Text stateText;
    public static bool isGameOver = false;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public bool timedLevel;

    private void Awake()
    {
        isGameOver = false;
        countDown = levelDuration;
        playerScore = 0;
    }

    void Start()
    {
        // set countDown to level duration specified in the inspector
        countDown = levelDuration;
        //set timer text
        if (timedLevel)
        {
            SetTimerText();
        }
        
        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // decrement countDown value while it's not zero
        if (!isGameOver)
        {
            if (countDown > 0)
            {
                if (timedLevel)
                {
                    countDown -= Time.deltaTime;
                }                
            }
            else
            {
                countDown = 0.0f;
                LevelLost();

            }
            if (timedLevel)
            {
                SetTimerText();
            }            
            SetScoreText();
        }


        //Debug.Log("Countdown" + countDown);
        // update timer text
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
            "Data Left: " + PickupActions.pickupCount;
    }

    void SetGameOverStatus(string gameTextMessage, AudioClip statusSFX)
    {
        /// <summary>method to set GameOver status</summary>
        /// 

        // set isGameOver 
        isGameOver = true;
        // update gameText UI component with appropriate message and activate it
        // message is received as an argument
        stateText.text = gameTextMessage;
        stateText.GetComponent<Text>().enabled = true;
        scoreText.GetComponent<Text>().enabled = false;
        timerText.GetComponent<Text>().enabled = false;
        // play level status sfx
        AudioSource.PlayClipAtPoint(statusSFX, Camera.main.transform.position);
        // sfx clip is received as an argument

    }
    public void LevelLost()
    {
        /// <summary>method to specify what happens when the level is lost</summary>
        /// 

        // call SetGameOverStatus with "GAME OVER!"
        SetGameOverStatus("YOU LOSE!", loseSFX);
        // slow down background music
        //Camera.main.GetComponent<AudioSource>().pitch = 0.5f;
        Invoke("LoadCurrentLevel", 3);
        //Invoke("LoadNextLevel", 5);
    }

    public float getCurrentTime()
    {
        return countDown;
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // can use getActiveScene to get currently loaded scene, can be used to specify next scene via index through build
    }

    public void LoadNextLevel()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // can use getActiveScene to get currently loaded scene, can be used to specify next scene via index through build
    }

    public void LevelBeat()
    {
        /// <summary>method to specify what happens when the level is won</summary>
        ///

        // call SetGameOverStatus with "YOU WIN!"
        SetGameOverStatus("YOU WIN!", winSFX);
        // speed up background music
        //Camera.main.GetComponent<AudioSource>().pitch = 2;
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            Invoke("LoadNextLevel", 5);
        }
    }


}