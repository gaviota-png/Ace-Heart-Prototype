using UnityEngine;

public class HitboxController : MonoBehaviour
{
    private void Start()
    {
        //gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyMarked"))
        {
            if (other!= null)
            {
                EnemyController enemy = other.GetComponent<EnemyController>();
                Debug.Log("ENEMY CURRENT LIFE : " + enemy.enemyLife);

                enemy.TakeDamage();
                if (enemy.enemyLife <= 0)
                {
                    Debug.Log("ENEMY DEAD");

                    Destroy(enemy.gameObject);
                }


            }
            
        }
    }

}
