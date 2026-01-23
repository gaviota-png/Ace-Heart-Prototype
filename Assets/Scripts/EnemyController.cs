using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject tpPointer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tpPointer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Card")
        {
            tpPointer.SetActive(true);
        }
    }

}
