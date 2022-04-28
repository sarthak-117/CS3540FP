using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightAction : MonoBehaviour
{
    public float start, strength;
    Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = start + strength * Mathf.PingPong(Time.time, 0.5f);
    }
}
