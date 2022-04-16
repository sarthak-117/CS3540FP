using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehavior : MonoBehaviour
{
    public GameObject projectile;
    public float ShootRate = 3f;
    public Transform player;
    public float rotateSpeed = 1f;
    public float rangeOfSight = 15f;
    public float moveSpeed = 0.5f;
    public float health = 100f;
    private bool closeEnough = false;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        InvokeRepeating("Shoot", 0, ShootRate);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks that target is within the range of sight
        if (!LevelManager.isGameOver)
        {

            if (Vector3.Distance(player.position, transform.position) < rangeOfSight)
            {
                if (Vector3.Distance(player.position, transform.position) > 7)
                {
                    Vector3 target = player.position;
                    target.y = 0;
                    transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
                }
                closeEnough = true;
                // Rotates the enemy towards the target
                var lookPos = player.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            }
            else
            {
                closeEnough = false;
            }

            if (health <= 0)
            {
                Destroy(gameObject);

                //add explosion effect or something 
            }
        }
    }

    void Shoot()
    {
        //Checks that player is within range and angle
        if (closeEnough & ((player.position - transform.position).normalized.z - transform.forward.normalized.z) < 10)
        {
            //Gets the vertical angle between forward vector and target position vector
            var forward = transform.forward;
            var target = player.position - transform.position;
            target.x = (forward.x / forward.z) * target.z;
            target.z = (forward.z / forward.x) * target.x;
            float angle = Vector3.Angle(forward, target);
            Quaternion aim = transform.rotation * Quaternion.Euler(-angle, 0, 0); ;
            //Shoot projectile
            Instantiate(projectile, transform.position + transform.forward + transform.up, aim);
        }
    }
}
