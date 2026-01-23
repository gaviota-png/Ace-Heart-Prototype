using UnityEngine;

public class HitboxController : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Detected");
        }
    }

}
