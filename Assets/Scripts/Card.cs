using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] public ParticleSystem tpCloudC;
    [SerializeField] public Transform raycast;
    [SerializeField] float cardSpeed = 5f;
    public GameObject tpPointer;
    public GameObject objMarked;
    CardSpawner spawner;

    public float animDelay = 5f;
    public float rayDist = 5f;
    public LayerMask groundLayer;

    public Vector3 finalPos;//pos final de carta

    public bool cardMoving = true;
    public bool insideObj = false;

    // Update is called once per frame
    private void Start()
    {
        tpPointer.SetActive(false);
    }

    private void Awake()
    {
        spawner = FindAnyObjectByType<CardSpawner>();
    }

    void Update()
    {
        if (!cardMoving) return;
        MoveCard();
    }

    public void SetCardSpawn(CardSpawner spawn)
    {
        //guardar referencia de spawner
        spawner = spawn;
    }

    public void PlayTPAnim()
    {

        tpCloudC.transform.parent = null;
        tpCloudC.Play();
        Debug.Log("CARD : PLAYING ANIM");
        Destroy(tpCloudC.gameObject, animDelay);
        
       
    }

    void MoveCard()
    {
        
        transform.position = transform.position + (transform.forward * Time.deltaTime * cardSpeed);
        RaycastCheck();
    }

    void RaycastCheck()
    {
        //RaycastHit hit; //punto que colisiona el raycast
         
        Vector3 origin = raycast.position;//punto de origen de ray cast
        Vector3 downDir = raycast.transform.TransformDirection(Vector3.down * rayDist);//direccion de raycast /mirando hacia abajo
        Vector3 forwardDir = transform.TransformDirection(Vector3.forward * rayDist);//direccion de raycast /frente

        Ray ray = new Ray(origin, forwardDir);
        //finalPos = ray.GetPoint(rayDist);

        if (Physics.Raycast(origin, downDir, out RaycastHit hit, rayDist, groundLayer))
        {
            Debug.Log("CARD : Looking at Ground");
            
            Debug.DrawRay(origin, downDir * rayDist, Color.red);
            finalPos = hit.point;//pos final carta = raycast
            

        }
        else
        {
            Debug.Log("CARD : NOT Looking at ground");
        }
        
        
        if (Physics.Raycast(ray, out RaycastHit hitt, rayDist))
        {
            Debug.DrawRay(origin, forwardDir * rayDist, Color.green);
            if (hitt.transform.CompareTag("Wall"))
            {
                Debug.Log("CARD : Looking at Wall");
                
            }
            
        }
        else
        {
            Debug.Log("CARD : NOT Looking at Wall");
        }


    }

    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Marked Enemy");
            collision.gameObject.tag = "EnemyMarked";
            
            //carta dentro de enemigo
            gameObject.SetActive(false);
            gameObject.transform.SetParent(collision.gameObject.transform);
            insideObj = true;
            objMarked = collision.gameObject;
            
        }

        else if (collision.gameObject.tag == "EnemyMarked" || collision.gameObject.tag == "Marked")
        {
           if (insideObj == false && cardMoving == true)
            {
                Debug.Log("THERES A CARD INSIDE ENEMY");
                if (spawner != null)
                {
                    Debug.Log("SPAWNER NOT NULL");
                    spawner.cardStopMoving = true;

                }
            }
            else
            {
                Debug.Log("CARD HIT OBJ (but nothing happened)");
            }

            
        }



        if (collision.gameObject.tag == "Wall" && cardMoving == true) //si choca con pared y se esta moviendo
        {
            //carta deja de moverse si toca pared
            Debug.Log("CARD STOPS MOVING");
            if (spawner != null)
            {
                spawner.cardStopMoving = true;
                
            }

        }

    }
}
