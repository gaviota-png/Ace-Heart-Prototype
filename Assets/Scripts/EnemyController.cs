using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyController : MonoBehaviour
{
    public GameObject tpPointer;
    
    [SerializeField] public ParticleSystem tpCloudE;
    [SerializeField] public int enemyLife = 3;
    [SerializeField] public Transform raycast;
    [SerializeField] public LayerMask groundLayer;
    public float animDelay = 0.5f;
    public float rayDist = 50f;
    public Vector3 finalPos;
   
    void Start()
    {
        tpPointer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyRaycast();
    }

    void EnemyRaycast()
    {
        Vector3 origin = raycast.position;//punto de origen de ray cast
        Vector3 downDir = raycast.transform.TransformDirection(Vector3.down * rayDist);//direccion de raycast /mirando hacia abajo
        //Vector3 forwardDir = transform.TransformDirection(Vector3.forward * rayDist);//direccion de raycast /frente

        //Ray ray = new Ray(origin, forwardDir);
        //finalPos = ray.GetPoint(rayDist);

        if (Physics.Raycast(origin, downDir, out RaycastHit hit, rayDist, groundLayer))
        {
            //Debug.Log("ENEMY : Looking at Ground");

            //Debug.DrawRay(origin, downDir * rayDist, Color.red);
            finalPos = hit.point;//pos final enem = raycast


        }
        else
        {
            Debug.Log("ENEMY : NOT Looking at ground");
        }
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

    void Attacking()
    {
        Debug.Log("Hitting Player");
    }

    void Chasing()
    {
        Debug.Log("Chasing Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Card")
        {
            tpPointer.SetActive(true);
        }

        if (collision.gameObject.tag == "Player")
        {
            Attacking();
        }

    }

}
