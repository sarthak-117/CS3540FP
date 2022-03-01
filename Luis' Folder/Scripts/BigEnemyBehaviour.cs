using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 5F;
    public GameObject player;
    public Transform playerT;


    // Start is called before the first frame update
    void Start()
    {
        // if player isn't specified in the inspector
        // try to find it.
        if (player == null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // move toward the player smoothly
        // moveSpeed should be adjustable through the inspector
        transform.position = Vector3.MoveTowards(
            transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
