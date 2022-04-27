using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLightScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Light>().intensity = Mathf.PingPong(Time.time, 3);
    }
}
