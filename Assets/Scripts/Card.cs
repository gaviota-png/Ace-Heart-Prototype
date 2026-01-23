using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] float cardSpeed = 5f;
    public GameObject tpPointer;

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
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Marked Enemy");
            collision.gameObject.tag = "EnemyMarked";
            
            Destroy(gameObject);

        }

        if (collision.gameObject.tag == "EnemyMarked" || collision.gameObject.tag == "Marked" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }
}
