using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class EnemyController : MonoBehaviour
{

    //Components
    public GameObject tpPointer;
    public NavMeshAgent agent;
    private Transform playerPos;
    [SerializeField] public ParticleSystem tpCloudE;

    //Variables
    [SerializeField] public int enemyLife = 3;
    [SerializeField] public LayerMask groundLayer, playerLayer;
    public float rayDist = 5f;

    bool playerVisible;
    bool playerReachable;

    float lastSeen;
    float seenTimer = 5f;

    //Patrol
    [SerializeField] public Transform waypoints;
    int waypointIndex;
    float waitTime = 2f;
    float timer = 0f;

    //States
    public float sightRange, attackRange;
    public bool playerInRange, attackInRange;



    public float animDelay = 0.5f;
   
    void Start()
    {
        tpPointer.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
        playerVisible = false;
        playerInRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        attackInRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (playerInRange)//si detecta jugador = jugador es visible
        {
            Vector3 dir = (playerPos.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position + Vector3.up, dir, out RaycastHit hit, sightRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player is within sight");
                    playerVisible = true;
                }
                else
                {
                    Debug.Log("Can't see player");
                }
            }
        }

        if (playerVisible) //si jugador es visible = jugador se puede alcanzar
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(playerPos.transform.position, path);//calcula camino hacia pos de jugador

            if (path.status == NavMeshPathStatus.PathComplete) 
            {
                Debug.Log("Player can be reached");
                playerReachable = true;
            }
            else
            {
                Debug.Log("Can't reach player");
            }
                
        }

        if (playerVisible && playerReachable && !attackInRange) //si jugador es visible y se puede alcanzar
        {
            lastSeen = 0f;
            Chasing();//empezar a perseguir
        }
        else
        {
            lastSeen += Time.deltaTime;
            if (lastSeen >= seenTimer)
            {
                Patroling();
            }
        }

        if (!playerInRange && !attackInRange) Patroling();
        //if (playerInRange && !attackInRange) Chasing();
        

    }


    void Attacking()
    {
        Debug.Log("Player in range");
    }

    void Chasing()
    {
        agent.SetDestination(playerPos.position);
    }

    void Patroling()
    {
        if (agent.remainingDistance <= 2f)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                waypointIndex++;
                timer = 0f;
                if (waypointIndex >= waypoints.childCount)
                {
                    waypointIndex = 0;

                }

                //Debug.Log("ENEMY HEADING TOWARDS " + waypoints.GetChild(waypointIndex));
                agent.SetDestination(waypoints.GetChild(waypointIndex).position);
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    public void TakeDamage()
    {
        enemyLife -= 1;
        
    }

    public void PlayTPAnim()
    {
        if (tpCloudE != null)
        {
            tpCloudE.Play();
            //Debug.Log("ENEMY : PLAY ANIM");

        }
        
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Card")
        {
            tpPointer.SetActive(true);          


        }
        if (collision.gameObject.tag == "Player" && attackInRange)
        {
            Attacking();
        }
    }

}
