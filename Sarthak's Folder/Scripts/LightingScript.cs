using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingScript : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var lights = FindObjectsOfType<Light>();
        foreach (Light l in lights)
        {
            if (l != null)
            {
                if (l.transform.parent != null && !l.transform.parent.CompareTag("Enemy"))
                {
                    l.color = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time, 1));
                }
            }           
            //l.intensity = Mathf.PingPong(Time.time, 5);
        }
        
    }
}
