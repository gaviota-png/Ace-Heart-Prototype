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

    public List<Card> cardList;

    public int listIndex = 3;
    private Vector3 endPos;//vector x dist
    public bool isShooting = false;
    private Transform savedCard;
    
    public float cooldown = 1.4f;

    private void Start()
    {
        cardList = new List<Card>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            FireCard();
        }
    }

    public void AddtoList(Card obj)
    {
        cardList.Add(obj);     
        
    }

    public void DestroyCard()
    {
        if (card != null)
        {
            Destroy(cardList[0].gameObject);
            card = null;
        }
    }
    void FireCard()
    {
        

        if (cardList.Count > listIndex)
        {
            Debug.Log("AddToList : LIST IS FULL CANT SPAWN");
        }
        else
        {
            endPos = originPoint.transform.position + originPoint.transform.forward * distance;//distancia final de movimiento de pelota al disparar
            if (cardPrefab && originPoint)
            {
                StartCoroutine(CardPosition(originPoint, endPos, cooldown));
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
        AddtoList(card);
        //separar card de padre
        savedCard.transform.parent = null;
        

        while (newTimer < timer && savedCard!= null)
        {
            savedCard.transform.position = Vector3.Lerp(startpos, target, (newTimer / timer));
            newTimer += Time.deltaTime;
            yield return null;
        }
        isShooting = false;
        if (savedCard != null)
        {   
            savedCard.transform.position = target;
            
            Card moving = savedCard.GetComponent<Card>();
            if (moving)//si hay carta
            {
                if (moving.cardMoving == false)//si la carta no se mueve
                {
                    Debug.Log("Card Not Moving");
                    //la carta deja de moverse

                }
                else if (moving.cardMoving == true)//si la carta se mueve
                {
                    moving.cardMoving = false;//carta deja de moverse
                                              //Debug.Log("Card Not Moving");
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
