using System.Collections;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private Transform cardPrefab;
    [SerializeField] private Transform originPoint;
    [SerializeField] float distance = 3f;


    private Vector3 endPos;//vector x dist lerp dur
    public bool isShooting = false;
    private Transform savedCard;
    
    public float cooldown = 1.4f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            FireCard();
        }
    }

    void FireCard()
    {
        
        endPos = originPoint.transform.position + originPoint.transform.forward * distance;
        if (cardPrefab && originPoint)
        {
            
            StartCoroutine(CardPosition(originPoint, endPos, cooldown));
                 

        }


        
    }

    IEnumerator CardPosition(Transform pos, Vector3 target, float timer)
    {
        float newTimer = 0f;
        isShooting = true;
        savedCard = Instantiate<Transform>(cardPrefab, pos.position, pos.rotation);
        Vector3 startpos = savedCard.position;
        //separar card de padre
        savedCard.transform.parent = null;
        while (newTimer < timer)
        {
            Debug.Log("Lerp TIme");
            savedCard.position = Vector3.Lerp(startpos, target, (newTimer / timer));
            newTimer += Time.deltaTime;
            yield return null;
        }
        isShooting = false;
        savedCard.position = target;

        Card moving = savedCard.GetComponent<Card>();
        if (moving)
        {
            moving.cardMoving = false;
        }

    }

}
