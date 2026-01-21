using System.Collections;
using UnityEngine;

public class PlayerTP : MonoBehaviour
{
    private bool isTP = false;//cooldown(?)
    CardSpawner cSpawn;
    CharacterMovement player;
    //Enemy enemy;

    private void Start()
    {
        cSpawn = GetComponent<CardSpawner>();
        player = GetComponent<CharacterMovement>();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {

            //if (card with tag Marked){
            //
            Debug.Log("Teleporting");
            StartCoroutine(TeleportCard());
            //
            //}



            //if (enemy w/tag Marked){
            //
            //
            //TeleportEnemy();
            //
            //}
        }
    }

    IEnumerator TeleportCard()
    {
        player.playerDisabled = true;
        yield return new WaitForSeconds(0.1f);
        //Vector3 pos = cSpawn.card.transform.position;
        Vector3 pos = new Vector3(0f, 0f, 0f);
        Debug.Log(pos);
        Destroy(cSpawn.card.gameObject);
        gameObject.transform.position = pos;
        Debug.Log("Teleported");
        yield return new WaitForSeconds(0.1f);
        player.playerDisabled = false;
        //destroy card dsp de swap
    }

    void TeleportEnemy()
    {

    }


}
