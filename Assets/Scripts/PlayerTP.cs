using System.Collections;
using UnityEngine;

public class PlayerTP : MonoBehaviour
{

    CardSpawner cSpawn;
    CharacterMovement player;
    GameObject enemy;
    public GameObject feetPos;
    //Enemy enemy;

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
            Debug.Log("Teleporting");
            if (cSpawn.card != null)
            {
                if (cSpawn.card.gameObject.tag == "Marked")//si obj carta tiene tag marked y existe carta ejecutar code
                {
                    Debug.Log("Teleported to card");
                    StartCoroutine(TeleportCard());
                }
            }
            
            else if (enemy.gameObject.tag == "EnemyMarked")//si obj tiene tag enemymarked ejecutar code
            {
                StartCoroutine(TeleportEnemy());
                enemy.gameObject.tag = "Enemy";
            }
            
        }
    }

    IEnumerator TeleportCard()
    {
        player.playerDisabled = true;
        CharacterController p = GetComponent<CharacterController>();
        p.enabled = false;
        yield return new WaitForSeconds(0.1f);

        Vector3 pos = new Vector3(cSpawn.card.transform.position.x, 0f, cSpawn.card.transform.position.z);

        cSpawn.DestroyCard();
        gameObject.transform.position = pos;
        p.enabled = true;
        
        yield return new WaitForSeconds(0.1f);
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
