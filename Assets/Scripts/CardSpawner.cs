using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] public GameObject cardPrefab;
    [SerializeField] private Transform originPoint;
    [SerializeField] float distance = 3f;
    public Card card;

    public IEnumerator cardRoutine;

    //public List<Card> cardList;
    [SerializeField] public Queue<Card> cardQueue;

    public int listIndex = 3;
    private Vector3 endPos;//vector x dist
    public bool isShooting = false;
    public bool cardStopMoving = false;
    
    
    public float cooldown = 1.4f;

    private void Start()
    {
        //cardList = new List<Card>();
        cardQueue = new Queue<Card>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            FireCard();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PrintQueue();
        }
    }

    public void AddtoList(Card obj)
    {
        //cardList.Add(obj);     
        Debug.Log("ADDED CARD");
        cardQueue.Enqueue(obj);
        
    }

    public void PrintQueue()
    {
        foreach (Card temp in cardQueue)
        {
            Debug.Log(temp);
            
        }
    }

    public void DestroyCard()
    {
        
        if (cardQueue != null)
        {
            Card exCard = cardQueue.Peek();

            //Debug.Log("REMOVED CARD");
            cardQueue.Dequeue();
            Destroy(exCard.gameObject);
            Debug.Log("DESTROYED CARD");
        }
        else
        {
            Debug.Log("NO CARDS IN QUEUE/QUEUE NULL");
        }
        
        
        
 
    }
    void FireCard()
    {

        if (cardQueue.Count > listIndex)
        {
            Debug.Log("LIST IS FULL");
            DestroyCard();
            //Debug.Log("DELETED CARD IN QUEUE");
        }
        else
        {
            endPos = originPoint.transform.position + originPoint.transform.forward * distance;//distancia final de movimiento de pelota al disparar
            if (cardPrefab && originPoint)
            {
                cardRoutine = CardPosition(originPoint, endPos, cooldown);
                StartCoroutine(cardRoutine);
            }

        }

    }

    IEnumerator CardPosition(Transform pos, Vector3 target, float timer)
    {
        float newTimer = 0f;
        isShooting = true;
        GameObject savedCard = Instantiate(cardPrefab, pos.position, pos.rotation);
        
        Vector3 startpos = savedCard.transform.position;//pos carta al inicio de disparo
        card = savedCard.GetComponent<Card>();//posicion de carta
        AddtoList(card);//se añade carta a queue

        card.SetCardSpawn(this);//pasar ref de spawner a card
        
        savedCard.transform.parent = null;//separar card de padre
        while (newTimer < timer && savedCard!= null && !cardStopMoving)
        {
            savedCard.transform.position = Vector3.Lerp(startpos, target, (newTimer / timer));
            newTimer += Time.deltaTime;
            yield return null;
            
        }
        cardStopMoving = false;
        isShooting = false;
        if (savedCard != null)
        {   
            savedCard.transform.position = target;
            
            Card moving = savedCard.GetComponent<Card>();
            if (moving)//si hay carta
            {
                
                if (moving.cardMoving == true)//si la carta se mueve
                {
                    moving.cardMoving = false;//carta deja de moverse
                                              
                    if (moving.gameObject.tag == "Card")//marcar carta
                    {
                        Debug.Log("Card Marked");
                        moving.gameObject.tag = "Marked";
                        moving.tpPointer.SetActive(true);

                    }
                }

            }

        }
        else
        {
            Debug.Log("Carta destruida antes de finalizar recorrido");
        }
   

    }

}
