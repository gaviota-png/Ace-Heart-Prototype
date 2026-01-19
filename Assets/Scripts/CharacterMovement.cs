using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Components")]
    private CharacterController controller;

    [Header("Variables")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private bool isGrounded;

    [Header("Player Input")]
    private float moveInput;
    private float turnInput;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void PlayerMovement()
    {
        GroundMovement();
    }

    private void GroundMovement() {

        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move.y = 0;

        move *= walkSpeed;

        controller.Move(move * Time.deltaTime);
    
    }
    private void InputDetection()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
    // Update is called once per frame
    void Update()
    {
        InputDetection();
        PlayerMovement();
    }
}
