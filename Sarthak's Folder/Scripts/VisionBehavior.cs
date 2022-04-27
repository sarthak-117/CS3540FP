using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Light light = gameObject.GetComponent<Light>();
        float distance = gameObject.GetComponentInParent<SmartEnemy>().chaseDistance;
        float angle = gameObject.GetComponentInParent<SmartEnemy>().fieldOfView;
        light.range = distance;
        light.spotAngle = angle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
