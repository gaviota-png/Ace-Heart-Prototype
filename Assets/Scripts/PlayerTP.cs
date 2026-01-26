using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTP : MonoBehaviour
{
    
    public List<GameObject> tpList;
    CardSpawner cSpawn;
    CharacterMovement player;
    GameObject enemy;
    GameObject cards;

    private int listIndex = 3; //maximo de obj en lista

    private void Start()
    {
        cSpawn = GetComponent<CardSpawner>();
        player = GetComponent<CharacterMovement>();
        tpList = new List<GameObject>();
       

    }
    private void Awake()
    {
        //for every enemy with tag -> add to list(?)
        enemy = GameObject.FindGameObjectWithTag("Enemy");//busca objecto con tag Enemy
        cards = GameObject.FindGameObjectWithTag("Card");
    }


    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            
            Debug.Log("Teleporting");
            if (cSpawn.card != null)
            {
  
                if (cSpawn.card.gameObject.tag == "Marked")//si obj carta tiene tag marked y existe carta ejecutar code
                {     
                    Debug.Log("Teleported to card");
                    StartCoroutine(TeleportCard());
                    //cSpawn.card.tpCloudC.Play();
                    cSpawn.cardList[0].tpCloudC.Play();
                }

            }
            
            else if (enemy != null)//si obj tiene tag enemymarked ejecutar code
            {
                EnemyController enem = enemy.GetComponent<EnemyController>();
                Debug.Log("Existe Enemigo");
                if (enem.tag == "EnemyMarked")
                {
                    StartCoroutine(TeleportEnemy());
                    
                    Debug.Log("Teleported to enemy");
                    enem.tag = "Enemy";
                    enem.tpPointer.SetActive(false);
                    enem.tpCloudE.Play();

                }

            }
        }

    }

    IEnumerator TeleportCard()
    {
        player.playerDisabled = true;
        CharacterController p = GetComponent<CharacterController>();

        p.enabled = false;
        yield return new WaitForSeconds(0.1f);

        //Vector3 pos = new Vector3(cSpawn.card.transform.position.x, 0f, cSpawn.card.transform.position.z); //pos final de carta
        //Vector3 pos = cSpawn.card.finalPos; //pos final carta de raycast
        Vector3 pos = cSpawn.cardList[0].finalPos;
        
        cSpawn.DestroyCard();
        gameObject.transform.position = pos;
        
        p.enabled = true;
        
        yield return new WaitForSeconds(0.1f);
        //Debug.Log("Player tp to " + pos);
        player.playerDisabled = false;

    }

    IEnumerator TeleportEnemy()
    {
        player.playerDisabled = true;
        CharacterController p = GetComponent<CharacterController>();
        p.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Vector3 oldPos = gameObject.transform.position;//pos actual jugador
        Vector3 oldEnemPos = enemy.transform.position;//pos actual enem

        //enemy pos -> old player pos
        //player new pos -> enemy old pos

        Vector3 newEnemyPos = new Vector3(oldPos.x, oldPos.y, oldPos.z);//pos nueva enem con pos antigua player
        Vector3 newPos = new Vector3(oldEnemPos.x, oldEnemPos.y, oldEnemPos.z);//pos nueva jugador con antigua enem

        enemy.transform.position = newEnemyPos;
        
        gameObject.transform.position = newPos;
        

        p.enabled = true;
        Debug.Log("Teleported to Enemy");
        yield return new WaitForSeconds(0.1f);
        
        player.playerDisabled = false;
        
    }
 

}
