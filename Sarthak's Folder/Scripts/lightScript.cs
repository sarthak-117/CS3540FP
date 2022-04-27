using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var light = gameObject.GetComponent<Light>();
        light.intensity = 0.5f +  2 * Mathf.PingPong(Time.time, 0.5f);
    }
}
