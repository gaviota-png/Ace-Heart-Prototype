using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject tpPointer;
    [SerializeField] public ParticleSystem tpCloudE;
    [SerializeField] public int enemyLife = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tpPointer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        enemyLife -= 1;

        if (enemyLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Attacking()
    {
        Debug.Log("Hitting Player");
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
