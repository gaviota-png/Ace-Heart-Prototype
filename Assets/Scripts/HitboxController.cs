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
                Debug.Log("ENEMY LIFE : " + enemy.enemyLife);
                enemy.TakeDamage();
                
                Debug.Log("ENEMY CURRENT LIFE : " + enemy.enemyLife);

                if (enemy.enemyLife <= 0)
                {
                    Debug.Log("ENEMY DEFEATED");
                    Destroy(enemy.gameObject);
                }
            }
            
        }
    }

}
