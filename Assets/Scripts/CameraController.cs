using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    [SerializeField] float cameraSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] float distance;
    [SerializeField] Quaternion rotation;

    private void Update()
    {
        Vector3 pos = Vector3.Lerp(transform.position,player.position + offset -transform.forward * distance,cameraSpeed *Time.deltaTime);
        transform.position = pos;

        transform.rotation = rotation;
    }

}
