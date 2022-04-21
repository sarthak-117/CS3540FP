using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketItem : MonoBehaviour
{

    public int rotationScalar = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationScalar, 0 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        FindObjectOfType<LevelManager>().LevelBeat();
        //Application.LoadLevel("Level Ver2");
    }
}
