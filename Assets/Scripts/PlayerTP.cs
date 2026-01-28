using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTP : MonoBehaviour
{
    CardSpawner cSpawn;
    CharacterMovement player;
    GameObject enemy;

    private void Start()
    {
        cSpawn = GetComponent<CardSpawner>();
        player = GetComponent<CharacterMovement>();     

    }
    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");//busca objecto con tag Enemy
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            
            //Debug.Log("Teleporting");

            if (cSpawn.cardQueue.Peek().insideObj == true && enemy != null)
            {
                EnemyController enem = enemy.GetComponent<EnemyController>();
                Debug.Log("Dentro de Enemigo");
                if (enem.tag == "EnemyMarked")
                {
                    StartCoroutine(TeleportEnemy());

                    Debug.Log("Teleported to enemy");
                    enem.tag = "Enemy";
                    enem.tpPointer.SetActive(false);

                    enem.PlayTPAnim();

                }
            }

            else if (cSpawn.cardQueue.Peek().tag == "Marked" && cSpawn.cardQueue.Peek() != null)//si obj carta tiene tag marked y existe carta ejecutar code
            {     
                
                StartCoroutine(TeleportCard());

                
                
            }
 
        }

    }

    IEnumerator TeleportCard()
    {
        player.playerDisabled = true;
        CharacterController p = GetComponent<CharacterController>();

        p.enabled = false;
        yield return new WaitForSeconds(0.1f);
        //Vector3 pos = cSpawn.card.finalPos; //pos final carta de raycast
        Vector3 pos = cSpawn.cardQueue.Peek().finalPos;

        cSpawn.cardQueue.Peek().PlayTPAnim();
        Debug.Log("TP : PLAY ANIM");
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
        //Vector3 oldEnemPos = enemy.transform.position;//pos actual enem
        Vector3 oldEnemPos = cSpawn.cardQueue.Peek().finalPos;

        //enemy pos -> old player pos
        //player new pos -> enemy old pos

        Vector3 newEnemyPos = new Vector3(oldPos.x, oldPos.y, oldPos.z);//pos nueva enem con pos antigua player
        Vector3 newPos = new Vector3(oldEnemPos.x, oldEnemPos.y, oldEnemPos.z);//pos nueva jugador con antigua enem

        enemy.transform.position = newEnemyPos;
        cSpawn.DestroyCard();
        
        gameObject.transform.position = newPos;
        

        p.enabled = true;
        //Debug.Log("Teleported to Enemy");
        yield return new WaitForSeconds(0.1f);
        
        player.playerDisabled = false;
        
    }
 

}
