
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public GameObject gameController;

    public LayerMask levelLayer;
    public LayerMask playerLayer;

    //Patrol
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Checks
    public float sightRange;
    public bool playerInSightRange;
    public bool LOS;
    public bool lostSight = false;

    //Save position
    public Vector3 lastSeen;
    public bool angry;

    private float timer;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        LOS = LineOfSight();

        if (!playerInSightRange && !lostSight) 
        {
            Patroling();
            angry = false;
        }
        if (playerInSightRange && !LOS && !lostSight) 
        {
            Patroling();
            angry = false;
        }
        if (playerInSightRange && LOS)
        {
            ChasePlayer();
            angry = true;
        }
        if (lostSight && !LOS)
        {
            FindPlayer();
            angry = true;
        }

    }
    bool LineOfSight()
    {
        if (Physics.Linecast(transform.position, player.position, levelLayer)) //checks if there is anything between the player and monster
        {
            return false;
        }
        else if(gameController.GetComponent<gameController>().hidden)//if player is hidden return false even if in line of sight
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void Patroling()
    {
        //Debug.Log("patrol");
        agent.speed = 3.0f;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, levelLayer))
        {
            walkPointSet = true;
        }
            
    }

    void ChasePlayer()
    {
        //Debug.Log("chase");
        agent.SetDestination(player.position);
        agent.speed = 4.0f;

        //sets a flag when the player breaks line of sight
        lostSight = true;
        lastSeen = player.position;
        
    }
    void FindPlayer()
    {
        //Debug.Log("finding player");
        agent.speed = 3.0f;

        agent.SetDestination(lastSeen);

        Vector3 distanceTolastSeen = transform.position - lastSeen;

        if (distanceTolastSeen.magnitude < 2f)
        {
            lostSight = false;
            timer = 0;
        }
        if (LOS)
        {
            lostSight = false;
            timer = 0;
        }
        if (lostSight)
        {
            timer += Time.deltaTime;
            //debug.log
            if (timer > 6f) //sets a 6 second detection limit if monster doesnt reach the player's last found position in the next 5 seconds
            {
                lostSight = false;
                timer = 0;
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameController.GetComponent<gameController>().dead = true;
            gameController.GetComponent<gameController>().deathSceneRunner();
        }
    }
}
