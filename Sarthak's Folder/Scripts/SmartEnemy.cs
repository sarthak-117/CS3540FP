using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartEnemy : MonoBehaviour
{
    public FSMStates currentState;
    public Animator anim;
    NavMeshAgent agent;
    CharacterController _controller;
    GameObject[] wanderPoints;
    Vector3 nextDestination;
    int currentDestinationIndex = 0;
    public Transform enemyEyes;
    public string WanderPointTag = "WanderPoint";
    public float chaseDistance = 10;
    public float fieldOfView = 45;
    public float attackDistance = 4;

    public int damageAmount = 45;

    float distanceToPlayer;

    float timeStamp = 0;

    public Transform player;
    public enum FSMStates
    {
        Patrol,
        Chase,
        Attack
    }

    // Start is called before the first frame update
    void Start()
    {

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        distanceToPlayer = Vector3.Distance
           (transform.position, player.transform.position);

        wanderPoints = GameObject.FindGameObjectsWithTag(WanderPointTag);
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        agent = GetComponent<NavMeshAgent>();
        currentState = FSMStates.Patrol;
        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        distanceToPlayer = Vector3.Distance
            (transform.position, player.position);

        if (!LevelManager.isGameOver && !BossFightManager.isGameOver)
        {
            switch (currentState)
            {
                case FSMStates.Patrol:
                    Patrol();
                    break;
                case FSMStates.Chase:
                    Chase();
                    break;
                case FSMStates.Attack:
                    Attack();
                    break;

            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        nextDestination = player.position;

        if(distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }
        agent.SetDestination(nextDestination);
        anim.SetInteger("changeAnimation", 2);
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //Debug.Log(distanceToPlayer);
        if (timeStamp <= Time.time)
        {
            if(distanceToPlayer < 4)
            {
                DamagePlayer();
            }
            timeStamp = Time.time + 1.56f;
        }

    }


    //Depends on player
    void DamagePlayer()
    {
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        var playerHealth = playerObj.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(damageAmount);
        
    }
    void Chase()
    {
        anim.SetInteger("changeAnimation", 1);
        nextDestination = player.position;
        agent.stoppingDistance = attackDistance;
        agent.speed = 5;
        
        if (distanceToPlayer <= attackDistance)
        {
            timeStamp = Time.time + 0.9f;
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }
        
        agent.SetDestination(nextDestination);
    }

    void Patrol()
    {
        
        anim.SetInteger("changeAnimation", 0);

        /*
        agent.stoppingDistance = 0;
        agent.speed = 3.5f;
        */
      

        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        
        if (IsPlayerInClearFOV())
        {
            currentState = FSMStates.Chase;
            Debug.Log("Chase");
        }
        

        //FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }

    void FindNextPoint()
    {
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        //agent.SetDestination(nextDestination);
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position + Vector3.up;
        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                //Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }

}
