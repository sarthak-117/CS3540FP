using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour
{
    public Color color1 = Color.blue;
    public Color color2 = Color.pink;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Light>() = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));
    }
}
