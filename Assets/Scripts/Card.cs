using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] public ParticleSystem tpCloudC;
    [SerializeField] public Transform raycast;
    [SerializeField] float cardSpeed = 5f;
    public GameObject tpPointer;

    public float rayDist = 5f;
    public LayerMask groundLayer;

    public Vector3 finalPos;//pos final de carta
    public bool isMarked = false;
    public GameObject markedObject = null;
    public bool cardMoving = true;
    // Update is called once per frame
    private void Start()
    {
        tpPointer.SetActive(false);

    }

    void Update()
    {
        if (!cardMoving) return;
        MoveCard();
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
            Debug.Log("Looking at Ground");
            
            Debug.DrawRay(origin, downDir * rayDist, Color.red);
            finalPos = hit.point;//pos final carta = raycast
            

        }
        else
        {
            Debug.Log("NOT Looking at ground");
        }
        
        
        if (Physics.Raycast(ray, out RaycastHit hitt, rayDist))
        {
            Debug.DrawRay(origin, forwardDir * rayDist, Color.green);
            if (hitt.transform.CompareTag("Wall"))
            {
                Debug.Log("Looking at Wall");
                
            }
            
        }
        else
        {
            Debug.Log("NOT Looking at Wall");
        }


        

        //Debug.DrawRay(origin, downDir * rayDist, Color.red);

    }

    public void DestroyCard()
    {

        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Marked Enemy");
            collision.gameObject.tag = "EnemyMarked";
            
            Destroy(gameObject);

        }

        if (collision.gameObject.tag == "EnemyMarked" || collision.gameObject.tag == "Marked")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Wall" && cardMoving == true) //si choca con pared y se esta moviendo
        {
            //carta deja de moverse
            Debug.Log("CARD STOPS MOVING");
            cardMoving = false;
            tpPointer.SetActive(true);

        }

    }
}
