using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Components")]
    private CharacterController controller;
    private Animator animator;

    [Header("Variables")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    private float vertVeloc;

    [Header("Player Input")]
    private float moveInput;
    private float turnInput;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void PlayerMovement()
    {
        GroundMovement();
    }

    private void GroundMovement() {

        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move.y = GravityCalc();

        move *= walkSpeed;

        controller.Move(move * Time.deltaTime);
        
    }

    private float GravityCalc()
    {
        if (controller.isGrounded)
        {
            vertVeloc = -1f;
        }
        else { vertVeloc -= gravity * Time.deltaTime;}
        return vertVeloc;
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
